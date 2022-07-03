using MailKit.Net.Smtp;
using MimeKit;
using NotificationService.Data.Repositories;

namespace NotificationService.Services.Implementations
{
	public class MailNotificationService : INotificationService
	{
		private readonly IMailRepository _mailRepository;
		private readonly string _host;
		private readonly int _port;

		public MailNotificationService(IMailRepository mailRepository, string host, int port)
		{
			_mailRepository = mailRepository;
			_host = host;
			_port = port;
		}

		public async Task NotifyAnalysisCompleteAsync(int userId, string avatarName)
		{
			var contact = await _mailRepository.GetContactByIdAsync(userId);

			var message = new MimeMessage();
			message.From.Add(new MailboxAddress("Avatar Validation System", "validator@ViBE.net"));
			message.To.Add(new MailboxAddress(contact.Name, contact.Email));
			message.Subject = "Validation klaar";

			var body = $"{contact.Name}, {Environment.NewLine}";
			body += $"De validatie van {avatarName} is compleet";

			message.Body = new TextPart("plain")
			{
				Text = body
			};

			using var client = new SmtpClient();
			client.Connect(_host, _port, false);

			client.Send(message);
			client.Disconnect(true);
		}
	}
}