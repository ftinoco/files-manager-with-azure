namespace FilesManagerWithAzure.Core.DTOs;

public class BlobInfoDTO
{
    public byte[] Blob { get; set; }
    public string ContentType { get; set; }
}