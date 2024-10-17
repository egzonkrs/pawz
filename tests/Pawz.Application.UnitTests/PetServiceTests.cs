using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Pawz.Application.Interfaces;
using Pawz.Application.Models;
using Pawz.Application.Services;
using Pawz.Application.UnitTests.Helpers;
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

    private void VerifyNoOtherCalls()
    {
        _mockPetRepository.VerifyNoOtherCalls();
        _mockUnitOfWork.VerifyNoOtherCalls();
        _mockFileUploaderService.VerifyNoOtherCalls();
        _mockLocationService.VerifyNoOtherCalls();
        _mockUserAccessor.VerifyNoOtherCalls();
        _mockMapper.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task CreatePetAsync_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var validRequest = PetServiceDataHelper.GenerateValidPetCreateRequest();
        var randomLocation = PetServiceDataHelper.GenerateValidLocation();
        var randomPet = PetServiceDataHelper.GenerateValidPet();

        _mockUserAccessor.Setup(x => x.GetUserId()).Returns("user-123");

        _mockLocationService.Setup(x => x.CreateLocationAsync(It.IsAny<Location>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Location>.Success(randomLocation));

        _mockMapper.Setup(x => x.Map<Pet>(It.IsAny<PetCreateRequest>()))
            .Returns(randomPet);

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
        Assert.Equal(randomPet.Id, result.Value);

        _mockLocationService.Verify(x => x.CreateLocationAsync(It.IsAny<Location>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockPetRepository.Verify(x => x.InsertAsync(It.IsAny<Pet>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
        _mockFileUploaderService.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Exactly(validRequest.ImageFiles.Count()));
        _mockUserAccessor.Verify(x => x.GetUserId(), Times.Exactly(2));
        _mockMapper.Verify(x => x.Map<Pet>(It.IsAny<PetCreateRequest>()), Times.Once);

        VerifyNoOtherCalls();
    }

    [Fact]
    public async Task CreatePetAsync_WithInvalidData_ReturnsFailure()
    {
        // Arrange
        var invalidRequest = PetServiceDataHelper.GenerateValidPetCreateRequest();

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
        _mockUserAccessor.Verify(x => x.GetUserId(), Times.Once);

        VerifyNoOtherCalls();
    }

    [Fact]
    public async Task CreatePetAsync_WhenLocationInsertFails_ReturnsFailure()
    {
        // Arrange
        var petCreateRequest = PetServiceDataHelper.GenerateValidPetCreateRequest();

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
        _mockUserAccessor.Verify(x => x.GetUserId(), Times.Once);

        VerifyNoOtherCalls();
    }

    [Fact]
    public async Task CreatePetAsync_WhenUnitOfWorkFails_ThrowsException()
    {
        // Arrange
        var validRequest = new PetCreateRequest
        {
            Name = "Test Pet",
            BreedId = 0,
            AgeYears = "2",
            About = "A friendly test pet",
            Price = -100,
            CityId = 1,
            Address = "123 Test St.",
            PostalCode = "54321",
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
            .ReturnsAsync(0); // Simulate a failure (e.g., no rows affected)

        // Act
        var result = await _petService.CreatePetAsync(validRequest, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(PetErrors.CreationFailed.Code, result.Errors.First().Code);
        Assert.Equal(PetErrors.CreationFailed.Description, result.Errors.First().Description);

        _mockLocationService.Verify(x => x.CreateLocationAsync(It.IsAny<Location>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockPetRepository.Verify(x => x.InsertAsync(It.IsAny<Pet>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _mockUserAccessor.Verify(x => x.GetUserId(), Times.Exactly(2));
        _mockMapper.Verify(x => x.Map<Pet>(It.IsAny<PetCreateRequest>()), Times.Once);

        VerifyNoOtherCalls();
    }

}
