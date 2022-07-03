using FileStorage.AzureBlobStorage;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileStorage;

public static class DependencyInjectionExtensions
{
	public static IServiceCollection AddAzureBlobStorageServices(this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddAzureClients(clientBuilder =>
		{
			clientBuilder.AddBlobServiceClient(configuration["AzureStorageConnectionString:blob"], true);
			clientBuilder.AddQueueServiceClient(configuration["AzureStorageConnectionString:queue"], true);
		});
		services.AddScoped<IAzureBlobServiceClient, AzureBlobServiceClient>();
		return services;
	}
}