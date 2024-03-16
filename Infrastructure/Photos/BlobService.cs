using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Application.Interfaces;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Domain;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Photos;

public class BlobService : IBlobService
{
    private readonly BlobServiceClient _blobServiceClient;
    public BlobService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public string GetBlobInfoAsync(string name)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient("posts-images");
        var blobClient = containerClient.GetBlobClient(name);
        var blobUri =  blobClient.Uri;
        return blobUri.ToString();
        //blobDownloadInfo.Value.Details.
        //return new BlobInfo(blobDownloadInfo.Value.Content, blobDownloadInfo.Value.ContentType);
    }

    public async Task<IEnumerable<string>> ListBlobsAsync()
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient("posts-images");
        var items = new List<string>();
        await foreach (var blobItem in containerClient.GetBlobsAsync())
        {
            items.Add(blobItem.Name);
        }
        return items;
    }

    public async Task UploadFileBlobAsync(string filePath, string filename)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient("posts-images");
        var blobClient = containerClient.GetBlobClient(filename);
        await blobClient.UploadAsync(filePath, new BlobHttpHeaders { ContentType = filePath.GetContentType() });

    }

    public async Task<Photo> UploadContentBlobAsync(IFormFile file, string filename)
    {
        if (file.Length > 0)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var containerClient = _blobServiceClient.GetBlobContainerClient("posts-images");
            var blobClient = containerClient.GetBlobClient(filename);
            ms.Position = 0;
            await blobClient.UploadAsync(ms, new BlobHttpHeaders { ContentType = filename.GetContentType() });

            return new Photo
            {
                Id = Guid.NewGuid().ToString(),
                Url = blobClient.Uri.ToString()
            };
        }

        throw new FileNotFoundException();

    }

    public async Task DeleteBlobAsync(string name)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient("posts-images");
        var blobClient = containerClient.GetBlobClient(name);
        await blobClient.DeleteIfExistsAsync();
    }
}