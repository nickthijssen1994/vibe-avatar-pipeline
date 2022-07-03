using Microsoft.Extensions.Azure;
using OpenFaceService.AzureBlobStorage;

namespace OpenFaceService;

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
		services.AddTransient<IAzureBlobServiceClient, AzureBlobServiceClient>();
		return services;
	}
}