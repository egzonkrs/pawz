using Microsoft.AspNetCore.Http;
using Pawz.Application.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Services;

/// <summary>
/// Service for handling file uploads.
/// </summary>
public class FileUploaderService : IFileUploaderService
{ /// <summary>
  /// Uploads a file to the specified directory and returns the relative path of the uploaded file.
  /// </summary>
  /// <param name="file">The file to be uploaded.</param>
  /// <param name="directory">The directory where the file will be saved.</param>
  /// <returns>The relative path of the uploaded file.</returns>
  /// <exception cref="ArgumentException">Thrown when the file is null or empty.</exception>
    public async Task<string> UploadFileAsync(IFormFile file, string directory)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File cannot be null or empty", nameof(file));

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var fullPath = Path.Combine(directory, fileName);

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Path.Combine(directory, fileName).Replace("\\", "/");
    }
}


