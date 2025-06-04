namespace Noo.Api.Media;

public static class MediaConfig
{
    /// <summary>
    /// Maximum file size in bytes
    /// </summary>
    public const int MaxFileSize = 150 * 1024 * 1024; // 10 MB

    /// <summary>
    /// Allowed file types (MIME types)
    /// </summary>
    public static readonly string[] AllowedFileTypes = [
        "image/jpeg",
        "image/png",
        "image/gif",
        "application/pdf"
    ];
}
