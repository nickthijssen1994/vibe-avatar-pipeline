namespace NotificationService.Services
{
	public interface INotificationService
	{
		Task NotifyAnalysisCompleteAsync(int userId, string avatarName);
	}
}