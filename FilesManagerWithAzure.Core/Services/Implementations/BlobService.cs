using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FilesManagerWithAzure.Core.DTOs;
using FilesManagerWithAzure.Core.Services.Interfaces;
using Microsoft.AspNetCore.StaticFiles;

namespace FilesManagerWithAzure.Core.Services.Implementations;

public class BlobService : IBlobService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly BlobContainerClient _blobContainerClient;

    public BlobService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient("filemanagercontainer");
    }

    public async Task<IEnumerable<string>> GetAllBlobs()
    {
        List<string> items = new();
        await foreach (var blob in _blobContainerClient.GetBlobsAsync())
        {
            items.Add(blob.Name);
        }
        return items;
    }

    public async Task<BlobInfoDTO> GetBlobByName(string blobName)
    {
        var blobClient = _blobContainerClient.GetBlobClient(blobName);
        Response<BlobDownloadResult> blobInfo = await blobClient.DownloadContentAsync();
        return new BlobInfoDTO()
        {
            Blob = blobInfo.Value.Content.ToArray(),
            ContentType = blobInfo.Value.Details.ContentType
        };
    }

    public async Task<Response<BlobContentInfo>> UploadFileBlob(Stream file, string fileName)
    {
        var blobClient = _blobContainerClient.GetBlobClient(fileName);
        return await blobClient.UploadAsync(file, new BlobHttpHeaders()
        {
            ContentType = GetContentType(fileName)
        }) ;
    }

   private static string GetContentType(string fileName)
    {
        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        FileExtensionContentTypeProvider provider = new();
        if (!provider.TryGetContentType(Path.Combine(path, fileName), out string contentType))
            contentType = "application/actet-stream";
        return contentType;
    }
}