using NotificationService.Services.Implementations;
using NotificationService.Data.Repositories;
using Models.Exceptions;

namespace NotificationService.Services.Factories
{
	public class NotificationServiceFactory
	{
		private readonly IMailRepository _mailRepository;

		public NotificationServiceFactory(IMailRepository mailRepository)
		{
			_mailRepository = mailRepository;
		}

		public MailNotificationService CreateNotificationService()
		{
			return new MailNotificationService(_mailRepository,
				Environment.GetEnvironmentVariable("VIBE_MAILHOST") ??
				throw new EnvironmentMisconfigurationException("The variable VIBE_MAILHOST is missing"),
				int.Parse(Environment.GetEnvironmentVariable("VIBE_MAILPORT") ??
				          throw new EnvironmentMisconfigurationException("The variable VIBE_MAILPORT is missing")));
		}
	}
}