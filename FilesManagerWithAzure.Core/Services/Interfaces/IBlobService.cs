using FilesManagerWithAzure.Core.DTOs;

namespace FilesManagerWithAzure.Core.Services.Interfaces;

public interface IBlobService
{
    Task<BlobInfoDTO> GetBlobByName(string blobName);
    Task<IEnumerable<string>> GetAllBlobs();
    Task UploadFileBlob(Stream file, string fileName);
}
