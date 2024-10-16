using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Pawz.Application.Interfaces;
using Pawz.Application.Models;
using Pawz.Application.Services;
using Pawz.Domain.Common;
using Pawz.Domain.Entities;
using Pawz.Domain.Enums;
using Pawz.Domain.Interfaces;
using Xunit;

namespace Pawz.Application.UnitTests;

public class PetServiceTests
{
    private readonly Mock<IPetRepository> _mockPetRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<ILogger<PetService>> _mockLogger;
    private readonly Mock<IFileUploaderService> _mockFileUploaderService;
    private readonly Mock<IPetImageRepository> _mockPetImageRepository;
    private readonly Mock<IUserAccessor> _mockUserAccessor;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILocationService> _mockLocationService;

    private readonly PetService _petService;

    public PetServiceTests()
    {
        _mockPetRepository = new Mock<IPetRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockLogger = new Mock<ILogger<PetService>>();
        _mockFileUploaderService = new Mock<IFileUploaderService>();
        _mockPetImageRepository = new Mock<IPetImageRepository>();
        _mockUserAccessor = new Mock<IUserAccessor>();
        _mockMapper = new Mock<IMapper>();
        _mockLocationService = new Mock<ILocationService>();

        _petService = new PetService(
            _mockPetRepository.Object,
            _mockUnitOfWork.Object,
            _mockLogger.Object,
            _mockFileUploaderService.Object,
            _mockPetImageRepository.Object,
            _mockUserAccessor.Object,
            _mockMapper.Object,
            _mockLocationService.Object
        );
    }

    [Fact]
    public async Task CreatePetAsync__WithValidData__ReturnsSuccess()
    {
        // Arrange
        var validRequest = new PetCreateRequest
        {
            Name = "Buddy",
            BreedId = 1,
            AgeYears = "2",
            About = "Friendly dog",
            Price = 100,
            CityId = 1,
            Address = "123 Pet St.",
            PostalCode = "12345",
            ImageFiles = new List<IFormFile>(),
            Status = PetStatus.Available
        };

        var location = new Location
        {
            Id = 1,
            CityId = validRequest.CityId,
            Address = validRequest.Address,
            PostalCode = validRequest.PostalCode
        };

        var pet = new Pet
        {
            Id = 1,
            Name = validRequest.Name,
            BreedId = validRequest.BreedId,
            AgeYears = validRequest.AgeYears,
            About = validRequest.About,
            Price = validRequest.Price,
            Location = location,
            PostedByUserId = "user-123",
            Status = PetStatus.Available
        };

        _mockUserAccessor.Setup(x => x.GetUserId()).Returns("user-123");

        _mockLocationService.Setup(x => x.CreateLocationAsync(It.IsAny<Location>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Location>.Success(location));

        _mockMapper.Setup(x => x.Map<Pet>(It.IsAny<PetCreateRequest>()))
            .Returns(pet);

        _mockPetRepository.Setup(x => x.InsertAsync(It.IsAny<Pet>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        _mockUnitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        _mockFileUploaderService.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>()))
            .ReturnsAsync(Result<string>.Success("file-url.jpg"));

        // Act
        var result = await _petService.CreatePetAsync(validRequest, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.Value);

        _mockLocationService.Verify(x => x.CreateLocationAsync(It.IsAny<Location>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockPetRepository.Verify(x => x.InsertAsync(It.IsAny<Pet>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
        _mockFileUploaderService.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Exactly(validRequest.ImageFiles.Count()));
    }

    [Fact]
    public async Task CreatePetAsync__WithInvalidData__ReturnsFailure()
    {
        // Arrange
        var invalidRequest = new PetCreateRequest
        {
            Name = "",
            BreedId = 0,
            AgeYears = null,
            About = "",
            Price = -100,
            CityId = 999, 
            Address = "", 
            PostalCode = "",
            ImageFiles = new List<IFormFile>(),
            Status = PetStatus.Available
        };

        _mockLocationService.Setup(x => x.CreateLocationAsync(It.IsAny<Location>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Location>.Failure(LocationErrors.CreationFailed));

        // Act
        var result = await _petService.CreatePetAsync(invalidRequest, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);

        Assert.NotEmpty(result.Errors);
        Assert.Equal(LocationErrors.CreationFailed.Code, result.Errors.First().Code);
        Assert.Equal(LocationErrors.CreationFailed.Description, result.Errors.First().Description);

        _mockPetRepository.Verify(x => x.InsertAsync(It.IsAny<Pet>(), It.IsAny<CancellationToken>()), Times.Never);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        _mockFileUploaderService.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Never);
        _mockLocationService.Verify(x => x.CreateLocationAsync(It.IsAny<Location>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CreatePetAsync__WhenLocationInsertFails__ReturnsFailure()
    {
        // Arrange
        var petCreateRequest = new PetCreateRequest
        {
            Name = "Test Pet",
            BreedId = 1,
            AgeYears = "2",
            About = "Test pet description",
            Price = 200,
            CityId = 999,
            Address = "123 Pet Street",
            PostalCode = "12345",
            ImageFiles = new List<IFormFile>(),
            Status = PetStatus.Available
        };

        var locationFailure = Result<Location>.Failure(LocationErrors.CreationFailed);

        _mockLocationService.Setup(x => x.CreateLocationAsync(It.IsAny<Location>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(locationFailure);

        // Act
        var result = await _petService.CreatePetAsync(petCreateRequest, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(LocationErrors.CreationFailed.Description, result.Errors.First().Description);

        _mockPetRepository.Verify(x => x.InsertAsync(It.IsAny<Pet>(), It.IsAny<CancellationToken>()), Times.Never);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        _mockFileUploaderService.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Never);
        _mockLocationService.Verify(x => x.CreateLocationAsync(It.IsAny<Location>(), It.IsAny<CancellationToken>()), Times.Once);
    }

}
