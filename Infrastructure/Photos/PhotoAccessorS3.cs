using System;
using Application.Photos;
using System.Net;
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
         private const string BUCKET_NAME = "pakshya.bucket";
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
            using (var client = new AmazonS3Client(_key, _secret))
            {
                var downloadRequest = new GetObjectRequest
                {
                    BucketName = "pdf-files-deepak-v1",
                    Key = filename
                };
                string responseBody;

                using (var response = await client.GetObjectAsync(downloadRequest))
                {
                    using (var responseStream = response.ResponseStream)
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            var title = response.Metadata["x-amz-meta-title"];
                            var contentType = response.Headers["Content-Type"];

                            responseBody = reader.ReadToEnd();
                        }
                    }
                }
                var pathAndFileName = $"C:\\Users\\deepakkumar11\\Downloads\\{filename}";
                var createText = responseBody;
                File.WriteAllText(pathAndFileName, createText);
                //   var fileTransferUtility = new TransferUtility(client);
                //    fileTransferUtility.DownloadAsync(downloadRequest);
            }
        }

        public async Task<S3Response> UploadFileAsync(string bucketName, IFormFile file)
        {
            var key = Guid.NewGuid().ToString();
            using (var client = new AmazonS3Client(_key, _secret))
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    file.CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = key,
                        StorageClass = S3StorageClass.Standard,
                        BucketName = BUCKET_NAME,
                        CannedACL = S3CannedACL.PublicRead
                    };

                    var fileTransferUtility = new TransferUtility(client);
                    await fileTransferUtility.UploadAsync(uploadRequest);

                }    

            }
            return new S3Response
            {
                Key = key,
                Url = $"https://{bucketName}.s3.{_region}.amazonaws.com/{key}"
            };

        }

        public async Task DeletePhoto(string Key, string bucket)
        {
             try
            {
                var deleteObjectRequest = new DeleteObjectRequest
                {
                    BucketName = bucket,
                    Key = Key
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