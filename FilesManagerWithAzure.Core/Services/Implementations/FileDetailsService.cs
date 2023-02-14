using FilesManagerWithAzure.Core.DTOs;
using FilesManagerWithAzure.Core.Persistence;
using FilesManagerWithAzure.Core.Persistence.Entities;
using FilesManagerWithAzure.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FilesManagerWithAzure.Core.Services.Implementations;

public class FileDetailsService: IFileDetailsService
{
    private readonly FileManagerContext _context;

	public FileDetailsService(FileManagerContext context)
    {
        _context= context;
    }

    public async Task<bool> CreateFileDetail(FileDetailDTO info)
    {
        _context.BlobInfos.Add(new FileDetail
        {
            Id = Guid.NewGuid().ToString(),
            ContentType = info.Extension,
            CreationDate = info.CreationDate,
            Description = info.Description,
            FileName = info.FileName,
            LastModificationDate = info.LastModificationDate,
        });
        var result = await _context.SaveChangesAsync();
        return  result > 0;
    }

    public IEnumerable<FileDetailDTO> GetAll()
    {
        var result =  _context.BlobInfos.ToListAsync();
        foreach (var file in result.Result)
        {
            yield return new FileDetailDTO
            {
                CreationDate = file.CreationDate,
                Description = file.Description,
                Extension = file.ContentType,
                FileName = file.FileName,
                LastModificationDate = file.LastModificationDate
            };
        }
    }
}