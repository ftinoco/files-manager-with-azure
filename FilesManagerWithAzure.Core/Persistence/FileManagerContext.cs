using FilesManagerWithAzure.Core.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilesManagerWithAzure.Core.Persistence;

public class FileManagerContext: DbContext
{
    public DbSet<FileDetail> BlobInfos { get; set; }

    public FileManagerContext()
    {

    }
    public FileManagerContext(DbContextOptions<FileManagerContext> options)
       : base(options)  { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder); 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FileDetail>()
            .ToContainer("FileDetails")
            .HasPartitionKey(x => x.Id);
    }
}