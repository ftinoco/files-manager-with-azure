using Microsoft.AspNetCore.Http;

namespace FilesManagerWithAzure.Core.DTOs;

public class FileUploadDTO
{
    public string Description { get; set; }
    public IFormFile File { get; set; }
}