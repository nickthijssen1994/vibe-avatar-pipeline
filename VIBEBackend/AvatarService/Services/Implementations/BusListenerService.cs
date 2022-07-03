using MessagingNetwork;
using MessagingNetwork.Messages;

namespace AvatarService.Services.Implementations;

public class BusListenerService : BackgroundService
{
	private readonly IServiceProvider _serviceProvider;

	public BusListenerService(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		if (stoppingToken.IsCancellationRequested)
		{
			return;
		}

		using var outerScope = _serviceProvider.CreateScope();
		IBus bus = outerScope.ServiceProvider.GetRequiredService<IBus>();

		await bus.ReceiveAsync<List<FileUploadedNotification>>("file_uploaded", async fileUploadData =>
		{
			// Create a new scope.
			// Using the outerscope object can cause issues with IDispoable objects like DbContext
			using var scope = _serviceProvider.CreateScope();
			ILogger<BusListenerService>
				logger = scope.ServiceProvider.GetRequiredService<ILogger<BusListenerService>>();

			logger.LogInformation("Message Received: File Upload");
			if (fileUploadData == null)
			{
				logger.LogError("No file upload data received");
				return;
			}

			IAvatarService avatarService = scope.ServiceProvider.GetRequiredService<IAvatarService>();

			logger.LogInformation("Adding Files Info To Avatar: {0}", fileUploadData.Count);
			await avatarService.StartHandleFileUploadedAsync(fileUploadData);
		});
	}
}