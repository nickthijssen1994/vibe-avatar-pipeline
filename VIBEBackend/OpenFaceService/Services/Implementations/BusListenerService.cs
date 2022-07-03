using MessagingNetwork;
using MessagingNetwork.Messages;
using FileInfo = OpenFaceService.Models.FileInfo;

namespace OpenFaceService.Services.Implementations;

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

		await bus.ReceiveAsync<AnalysisRequest>("start_openface_analysis", async analysisRequest =>
		{
			// Create a new scope.
			// Using the outerscope object can cause issues with IDispoable objects like DbContext
			using var scope = _serviceProvider.CreateScope();
			ILogger<BusListenerService>
				logger = scope.ServiceProvider.GetRequiredService<ILogger<BusListenerService>>();

			logger.LogInformation("Message Received: Start OpenFace Analysis Request");
			if (analysisRequest == null)
			{
				logger.LogError("No analysis parameters received");
				return;
			}

			IOpenFaceAnalysisService analysisService =
				scope.ServiceProvider.GetRequiredService<IOpenFaceAnalysisService>();

			logger.LogInformation("Starting OpenFace Analysis...");

			var fileInfo = new FileInfo
			{
				ContainerName = analysisRequest.ContainerId,
				FileName = analysisRequest.FileName,
				FilePath = analysisRequest.FileDirectory
			};

			await analysisService.StartAnalysisAsync(fileInfo);
		});
	}
}