using MessagingNetwork;

namespace FileStorageService.Services.Implementations;

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

		using var scope = _serviceProvider.CreateScope();
		IBus bus = scope.ServiceProvider.GetRequiredService<IBus>();
		IFileStorageService fileStorageService = scope.ServiceProvider.GetRequiredService<IFileStorageService>();

		var logger = scope.ServiceProvider.GetRequiredService<ILogger<BusListenerService>>();
	}
}