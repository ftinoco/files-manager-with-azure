using Azure;
using Azure.Storage.Blobs.Models;
using FilesManagerWithAzure.Core.DTOs;

namespace FilesManagerWithAzure.Core.Services.Interfaces;

public interface IBlobManageService
{
    Task<FileDTO> GetBlobByName(string blobName);
    Task<IEnumerable<string>> GetAllBlobs();
    Task<Response<BlobContentInfo>> UploadFileBlob(Stream file, string fileName);
}
