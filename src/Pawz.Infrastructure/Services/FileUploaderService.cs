using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Pawz.Application.Interfaces;
using Pawz.Application.Services;
using Pawz.Domain.Common;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Services;

/// <summary>
/// Service for handling file uploads.
/// </summary>
public class FileUploaderService : IFileUploaderService
{
    private readonly ILogger<PetService> _logger;

    public FileUploaderService(ILogger<PetService> logger)
    {
        _logger = logger;
    }

    public static readonly string PetImagesDirectory = "wwwroot/images/pets";
    private readonly string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
    private const int maxFileSize = 2 * 1024 * 1024; // 2MB

    /// <summary>
    /// Uploads a file to the specified directory after validating it.
    /// </summary>
    /// <param name="file">The file to be uploaded.</param>
    /// <param name="directory">The directory where the file will be saved.</param>
    /// <returns>The relative path of the uploaded file.</returns>
    /// <exception cref="ArgumentException">Thrown when the file is null, empty, too large, or has an invalid type.</exception>
    public async Task<Result<string>> UploadFileAsync(IFormFile file)
    {
        try
        {
            if (file is null || file.Length is 0)
            {
                _logger.LogError("File upload failed: no file was provided.");
                return Result<string>.Failure(FileUploadErrors.InvalidFile);
            }

            if (file.Length > maxFileSize)
            {
                _logger.LogError("File upload failed: file size {FileSize} exceeds the maximum allowed size of {MaxFileSize}.", file.Length, maxFileSize);
                return Result<string>.Failure(new Error(FileUploadErrors.MaxFileSize.Code, $"{FileUploadErrors.MaxFileSize.Description}: {maxFileSize / (1024 * 1024)} MB."));
            }

            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                var allowedTypes = string.Join(", ", allowedExtensions);
                _logger.LogError("File upload failed: file extension {FileExtension} is not allowed.", fileExtension);
                return Result<string>.Failure(new Error(FileUploadErrors.UnsupportedFileFormat.Code, $"{FileUploadErrors.UnsupportedFileFormat.Description} Allowed types are: {allowedTypes}"));
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(PetImagesDirectory, fileName);

            if (!Directory.Exists(PetImagesDirectory))
            {
                Directory.CreateDirectory(PetImagesDirectory);
            }

            _logger.LogInformation("Starting file upload: {FileName}", fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            _logger.LogInformation("File uploaded successfully: {FilePath}", fileName);

            return Result<string>.Success(fileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while uploading the file.");
            return Result<string>.Failure(FileUploadErrors.UnexpectedError);
        }
    }
}


