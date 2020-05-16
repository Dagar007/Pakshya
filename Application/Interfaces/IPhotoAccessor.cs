using Application.Photos;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IPhotoAccessor
    {
         PhotoUploadResult AddPhoto(IFormFile file, bool transform =false);
         string DeletePhoto (string publicId);
    }
}