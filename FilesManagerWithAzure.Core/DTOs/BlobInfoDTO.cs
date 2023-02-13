namespace FilesManagerWithAzure.Core.DTOs;

public class BlobInfoDTO
{
    public BinaryData Blob { get; set; }
    public string ContentType { get; set; }
}