using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FilesManagerWithAzure.Core.DTOs;
using FilesManagerWithAzure.Core.Services.Interfaces;

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

    public async Task<BlobInfoDTO> GetBlobByName(string blobName)
    {
        var blobClient = _blobContainerClient.GetBlobClient(blobName);
        Response<BlobDownloadResult> blobInfo = await blobClient.DownloadContentAsync();
        return new BlobInfoDTO()
        {
            Blob = blobInfo.Value.Content,
            ContentType = blobInfo.Value.Details.ContentType
        };
    }
}