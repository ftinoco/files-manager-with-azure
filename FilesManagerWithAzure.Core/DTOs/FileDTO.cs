namespace FilesManagerWithAzure.Core.DTOs;

public class FileDTO
{
    public byte[] Blob { get; set; }
    public string ContentType { get; set; }
}