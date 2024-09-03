namespace Pawz.Domain.Common;

/// <summary>
/// Provides a collection of standardized error messages related to file uploads.
/// This class defines errors for scenarios such as invalid files, unsupported file types, exceeding maximum file size, and unexpected errors during file upload operations.
/// </summary>
public class FileUploadErrors
{
    /// <summary>
    /// Error indicating that the uploaded file is invalid, either because it is null or empty.
    /// </summary>
    public static readonly Error InvalidFile = new Error("FileUpload.InvalidFile", "Please upload a valid file.");

    /// <summary>
    /// Error indicating that the uploaded file has an unsupported or invalid file extension.
    /// </summary>
    public static readonly Error UnsupportedFileFormat = new Error("FileUpload.UnsupportedFileFormat ", "File extension type is not allowed.");

    /// <summary>
    /// Error indicating that the uploaded file exceeds the maximum allowed file size.
    /// </summary>
    public static readonly Error MaxFileSize = new Error("FileUpload.MaxFileSize", "File size exceeds the maximum allowed size");

    /// <summary>
    /// Error indicating that an unexpected error occurred during the file upload process.
    /// </summary>
    public static readonly Error UnexpectedError = new Error("FileUpload.UnexpectedError", "An unexpected error occurred while uploading the file.");

}
