using System.Threading.Tasks;
using Application.Photos;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IPhotoS3Accessor
    {
         Task<S3Response> UploadFileAsync(string bucketName, IFormFile file);
         Task GetObjectFromS3(string filename);
         Task DeletePhoto(string Key, string bucket);
    }
}