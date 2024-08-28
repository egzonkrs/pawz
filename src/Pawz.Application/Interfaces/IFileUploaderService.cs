using Microsoft.AspNetCore.Http;
using Pawz.Domain.Common;
using System.Threading.Tasks;

namespace Pawz.Application.Interfaces;

/// <summary>
/// Interface for a service that handles file uploads.
/// </summary>
public interface IFileUploaderService
{
    /// <summary>
    /// Uploads a file to a specified directory and returns the relative path of the uploaded file.
    /// </summary>
    /// <param name="file">The file to be uploaded.</param>
    /// <param name="directory">The directory where the file will be saved.</param>
    /// <returns>The relative path of the uploaded file.</returns>
    Task<Result<string>> UploadFileAsync(IFormFile file, string directory);
}
