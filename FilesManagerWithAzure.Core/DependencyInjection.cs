using FilesManagerWithAzure.Core.Persistence;
using FilesManagerWithAzure.Core.Services.Implementations;
using FilesManagerWithAzure.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FilesManagerWithAzure.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreDependencies(this IServiceCollection services,
            IConfiguration configuration)
    {
        services.AddDbContext<FileManagerContext>(opt => opt.UseCosmos(
            configuration.GetSection("AzureCosmosDbSettings:AccountUri").Value,
            configuration.GetSection("AzureCosmosDbSettings:AccountKey").Value,
            configuration.GetSection("AzureCosmosDbSettings:DatabaseName").Value
            ));
        services.AddScoped<IBlobManageService, BlobManageService>();
        services.AddScoped<IFileDetailsService, FileDetailsService>();
        return services;
    }
}