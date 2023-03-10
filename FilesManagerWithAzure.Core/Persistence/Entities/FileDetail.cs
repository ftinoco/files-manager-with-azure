namespace FilesManagerWithAzure.Core.Persistence.Entities;

public class FileDetail
{ 
    public string? Id { get; set; }
    public string? FileName { get; set; }
    public string? Description { get; set; }
    public string? ContentType { get; set; }
    public DateTime CreationDate { get; set; } 
    public DateTime LastModificationDate { get; set; }
}