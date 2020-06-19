using Application.Interfaces;
using Application.Photos;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Photos
{
    public class PhotoAccessorS3 : IPhotoAccessor
    {
        public PhotoUploadResult AddPhoto(IFormFile file, bool transform = false)
        {
            throw new System.NotImplementedException();
        }

        public string DeletePhoto(string publicId)
        {
            throw new System.NotImplementedException();
        }
    }
}