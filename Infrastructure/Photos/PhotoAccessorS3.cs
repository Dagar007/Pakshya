using System;
using Application.Photos;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Infrastructure.Security;
using System.IO;

namespace Infrastructure.Photos
{
    public class PhotoAccessorS3 : IPhotoS3Accessor
    {
        private readonly IAmazonS3 _client;
        private const string BucketName = "pakshya.bucket";
        private readonly string _key;
        private readonly string _secret;
        private readonly string _region;

        public PhotoAccessorS3(IAmazonS3 client, IOptions<AwsSettings> settings)
        {
            _client = client;
            _key = settings.Value.AccessKey;
            _region = settings.Value.Region;
            _secret = settings.Value.Secret;

        }
    
        public async Task GetObjectFromS3(string filename)
        {
            using var client = new AmazonS3Client(_key, _secret);
            var downloadRequest = new GetObjectRequest
            {
                BucketName = "pdf-files-deepak-v1",
                Key = filename
            };
            string responseBody;

            using (var response = await client.GetObjectAsync(downloadRequest))
            {
                await using (var responseStream = response.ResponseStream)
                {
                    using var reader = new StreamReader(responseStream);

                    responseBody = await reader.ReadToEndAsync();
                }
            }
            var pathAndFileName = $"C:\\Users\\deepakkumar11\\Downloads\\{filename}";
            var createText = responseBody;
            await File.WriteAllTextAsync(pathAndFileName, createText);
        }

        public async Task<S3Response> UploadFileAsync(string bucketName, IFormFile file)
        {
            var key = Guid.NewGuid().ToString();
            using var client = new AmazonS3Client(_key, _secret);
            await using var newMemoryStream = new MemoryStream();
            await file.CopyToAsync(newMemoryStream);

            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = newMemoryStream,
                Key = key,
                StorageClass = S3StorageClass.Standard,
                BucketName = BucketName,
                CannedACL = S3CannedACL.PublicRead
            };

            var fileTransferUtility = new TransferUtility(client);
            await fileTransferUtility.UploadAsync(uploadRequest);

            return new S3Response
            {
                Key = key,
                Url = $"https://{bucketName}.s3.{_region}.amazonaws.com/{key}"
            };

        }

        public async Task DeletePhoto(string key, string bucket)
        {
             try
             {
                 var deleteObjectRequest = new DeleteObjectRequest
                 {
                     BucketName = bucket,
                     Key = key
                 };

                 Console.WriteLine("Deleting an object");
                 await _client.DeleteObjectAsync(deleteObjectRequest);
             }
             catch (AmazonS3Exception e)
             {
                 Console.WriteLine("Error encountered on server. Message:'{0}' when deleting an object", e.Message);
             }
             catch (Exception e)
             {
                 Console.WriteLine("Unknown encountered on server. Message:'{0}' when deleting an object", e.Message);
             }
        }
    }
}