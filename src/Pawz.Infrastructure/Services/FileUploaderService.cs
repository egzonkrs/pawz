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

    private readonly string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
    private const int maxFileSize = 2 * 1024 * 1024; // 2MB

    /// <summary>
    /// Uploads a file to the specified directory after validating it.
    /// </summary>
    /// <param name="file">The file to be uploaded.</param>
    /// <param name="directory">The directory where the file will be saved.</param>
    /// <returns>The relative path of the uploaded file.</returns>
    /// <exception cref="ArgumentException">Thrown when the file is null, empty, too large, or has an invalid type.</exception>
    public async Task<Result<string>> UploadFileAsync(IFormFile file, string directory)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                _logger?.LogWarning("File upload failed: file is null or empty.");
                return Result<string>.Failure("File cannot be null or empty.");
            }

            if (file.Length > maxFileSize)
            {
                _logger?.LogWarning("File upload failed: file size {FileSize} exceeds the maximum allowed size of {MaxFileSize}.", file.Length, maxFileSize);
                return Result<string>.Failure($"File size exceeds the maximum allowed size of {maxFileSize / (1024 * 1024)} MB.");
            }

            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                _logger?.LogWarning("File upload failed: file extension {FileExtension} is not allowed.", fileExtension);
                return Result<string>.Failure("File type is not allowed. Allowed types are: " + string.Join(", ", allowedExtensions));
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(directory, fileName);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
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
            return Result<string>.Failure("An unexpected error occurred while uploading the file.");
        }
    }
}


