using FilesManagerWithAzure.Core.DTOs;
using FilesManagerWithAzure.Core.Persistence.Entities;

namespace FilesManagerWithAzure.Core.Services.Interfaces;

public interface IFileDetailsService
{
    Task<List<FileDetail>> GetAll();
    Task<bool> CreateFileDetail(FileDetailDTO info);
}