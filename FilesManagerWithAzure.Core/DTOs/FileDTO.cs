namespace FilesManagerWithAzure.Core.DTOs;

public class FileDTO
{
    public string FileName { get; set; }
    public string Extension { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastAccessDate { get; set; }
}