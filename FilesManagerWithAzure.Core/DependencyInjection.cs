using FilesManagerWithAzure.Core.Services.Implementations;
using FilesManagerWithAzure.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FilesManagerWithAzure.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
    {
        services.AddScoped<IBlobService, BlobService>();
        return services;
    }
}