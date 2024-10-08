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
    private readonly ILogger<FileUploaderService> _logger;

    public FileUploaderService(ILogger<FileUploaderService> logger)
    {
        _logger = logger;
    }

    private const string PetImagesDirectory = "wwwroot/images/pets";
    private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
    private const int MaxFileSize = 10 * 1024 * 1024;

    /// <summary>
    /// Uploads a file to the specified directory after validating it.
    /// </summary>
    /// <param name="file">The file to be uploaded.</param>
    public async Task<Result<string>> UploadFileAsync(IFormFile file)
    {
        try
        {
            if (file is null || file.Length is 0)
            {
                _logger.LogError("File upload failed: no file was provided.");
                return Result<string>.Failure(FileUploadErrors.InvalidFile);
            }

            if (file.Length > MaxFileSize)
            {
                _logger.LogError("File upload failed: file size {FileSize} exceeds the maximum allowed size of {MaxFileSize}.", file.Length, MaxFileSize);
                return Result<string>.Failure(new Error(FileUploadErrors.MaxFileSize.Code, $"{FileUploadErrors.MaxFileSize.Description}: {MaxFileSize / (1024 * 1024)} MB."));
            }

            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!_allowedExtensions.Contains(fileExtension))
            {
                var allowedTypes = string.Join(", ", _allowedExtensions);
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


