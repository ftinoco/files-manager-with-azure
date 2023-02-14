namespace FilesManagerWithAzure.Core.DTOs;

public class FileDetailDTO
{
    public string FileName { get; set; }
    public string Description { get; set; }
    public string Extension { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastAccessDate { get; set; }
    public DateTime LastModificationDate { get; set; }
}