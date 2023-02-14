using FilesManagerWithAzure.Core.DTOs;
using FilesManagerWithAzure.Core.Persistence;
using FilesManagerWithAzure.Core.Persistence.Entities;
using FilesManagerWithAzure.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FilesManagerWithAzure.Core.Services.Implementations;

public class BlobInfoService: IBlobInfoService
{
    private readonly FileManagerContext _context;
	public BlobInfoService(FileManagerContext context)
    {
        _context= context;
    }

    public async Task<bool> CreateBlobInfo(FileDTO info)
    {
        _context.BlobInfos.Add(new BlobContainer
        {
            Id = Guid.NewGuid().ToString(),
            ContentType = info.Extension,
            CreationDate = info.CreationDate,
            Description = info.Description,
            FileName = info.FileName,
            LastAccessDate = info.LastAccessDate,
            LastModificationDate = info.LastModificationDate,
        });
        var result = await _context.SaveChangesAsync();
        return  result > 0;
    }

    public async Task<List<BlobContainer>> GetAllBlobInfo()
    {
        return await _context.BlobInfos.ToListAsync();
    }
}