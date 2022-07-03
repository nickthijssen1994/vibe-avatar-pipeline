using MessagingNetwork;
using MessagingNetwork.Messages;

namespace AnalysisService.Services.Implementations
{
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

			await bus.ReceiveAsync<AnalysisRequest>("start_analysis", async analysisRequest =>
			{
				// Create a new scope.
				// Using the outerscope object can cause issues with IDispoable objects like DbContext
				using var scope = _serviceProvider.CreateScope();
				ILogger<BusListenerService> logger =
					scope.ServiceProvider.GetRequiredService<ILogger<BusListenerService>>();

				logger.LogInformation("Message Received: Analysis Requested");
				if (analysisRequest == null)
				{
					logger.LogError("No analysis request data received");
					return;
				}

				IAnalysisService analysisService = scope.ServiceProvider.GetRequiredService<IAnalysisService>();

				logger.LogInformation("Starting Analysis...");
				await analysisService.StartAnalysisAsync(analysisRequest);
			});
		}
	}
}