using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Application.Interfaces;

public interface IBlobService
{
    public string GetBlobInfoAsync(string name);
    public Task<IEnumerable<string>> ListBlobsAsync();
    public Task UploadFileBlobAsync(string filePath, string filename);

    public Task<Photo> UploadContentBlobAsync(IFormFile file, string filename);
    public Task DeleteBlobAsync(string name);
}