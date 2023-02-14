using FilesManagerWithAzure.Core.DTOs;
using FilesManagerWithAzure.Core.Persistence.Entities;

namespace FilesManagerWithAzure.Core.Services.Interfaces;

public interface IBlobInfoService
{
    Task<List<BlobContainer>> GetAllBlobInfo();
    Task<bool> CreateBlobInfo(FileDTO info);
}