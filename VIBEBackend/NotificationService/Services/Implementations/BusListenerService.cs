using MessagingNetwork;
using MessagingNetwork.Messages;

namespace NotificationService.Services.Implementations
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

			await bus.ReceiveAsync<AnalysisCompleteNotificationData>("analysis_complete", async analysisCompleteData =>
			{
				// Create a new scope.
				// Using the outerscope object can cause issues with IDispoable objects like DbContext
				using var scope = _serviceProvider.CreateScope();
				ILogger<BusListenerService> logger =
					scope.ServiceProvider.GetRequiredService<ILogger<BusListenerService>>();
				if (analysisCompleteData == null)
				{
					logger.LogError("No data received in Analysis Complete message");
					return;
				}

				INotificationService notificationService =
					scope.ServiceProvider.GetRequiredService<INotificationService>();

				logger.LogInformation("Message Received: analyis complete");
				await notificationService.NotifyAnalysisCompleteAsync(analysisCompleteData.UserID,
					analysisCompleteData.AvatarName);
			});
		}
	}
}