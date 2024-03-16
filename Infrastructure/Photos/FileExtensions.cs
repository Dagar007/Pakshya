using Microsoft.AspNetCore.StaticFiles;

namespace Infrastructure.Photos;

public static class FileExtensions
{
    private static readonly FileExtensionContentTypeProvider Provider = new FileExtensionContentTypeProvider();

    public static string GetContentType(this string filename)
    {
        if (!Provider.TryGetContentType(filename, out var contentType))
        {
            contentType = "application/octet-stream";
        }
        return contentType;
    }
}