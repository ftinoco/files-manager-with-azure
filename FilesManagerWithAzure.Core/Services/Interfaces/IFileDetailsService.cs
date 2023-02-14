using FilesManagerWithAzure.Core.DTOs;

namespace FilesManagerWithAzure.Core.Services.Interfaces;

public interface IFileDetailsService
{
    IEnumerable<FileDetailDTO> GetAll();
    Task<bool> CreateFileDetail(FileDetailDTO info);
}