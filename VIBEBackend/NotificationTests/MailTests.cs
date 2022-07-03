using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using netDumbster.smtp;
using NotificationService.Controllers;
using NotificationService.Data.Contexts;
using NotificationService.Data.Repositories.Implementations;
using NotificationService.Services.Implementations;
using NUnit.Framework;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MessagingNetwork.Messages;

namespace NotificationTests
{
	public class Tests
	{
		private readonly int mailport = 9009;
		private readonly string mailhost = "localhost";
		private SimpleSmtpServer? mailServer;

		private static async Task<NotificationServiceContext> SetupDB(string dbName, bool seed = true)
		{
			var options = new DbContextOptionsBuilder<NotificationServiceContext>()
				.UseInMemoryDatabase(databaseName: "InMemoryNotificationDb_" + dbName)
				.Options;
			var context = new NotificationServiceContext(options);
			if (seed)
			{
				await context.Seed();
			}

			return context;
		}

		private async Task<NotificationController> Initialize(bool seed = true,
			[CallerMemberName] string callerName = "")
		{
			var context = await SetupDB(callerName, seed);

			using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
			var logger = loggerFactory.CreateLogger<NotificationController>();

			var service = new MailNotificationService(new EntityMailRepository(context), mailhost, mailport);

			var controller = new NotificationController(service, logger);
			return controller;
		}

		[SetUp]
		public void SetUp()
		{
			mailServer = SimpleSmtpServer.Start(mailport);
		}

		[Test]
		public async Task MailSent()
		{
			var controller = await Initialize();

			var requestData = new AnalysisCompleteNotificationData(1, "Josie");

			await controller.NotifyAnalysisComplete(requestData);

			Assert.IsNotNull(mailServer);

			Assert.That(mailServer?.ReceivedEmailCount, Is.EqualTo(1));
		}

		[Test]
		public async Task MailContainsAvatarName()
		{
			var controller = await Initialize();

			var avatarName = "Josie";

			var requestData = new AnalysisCompleteNotificationData(1, avatarName);

			await controller.NotifyAnalysisComplete(requestData);

			Assert.IsNotNull(mailServer);

			var mailBody = mailServer?.ReceivedEmail[0].MessageParts[0].BodyData;

			Assert.That(mailBody, Does.Contain("Josie"));
		}

		[Test]
		public async Task WhenContactDoesntExist_ReturnNotFoundError()
		{
			var controller = await Initialize();

			var avatarName = "Josie";

			var requestData = new AnalysisCompleteNotificationData(548, avatarName);

			var response = await controller.NotifyAnalysisComplete(requestData);

			Assert.That(response, Is.TypeOf<NotFoundResult>() | Is.TypeOf<NotFoundObjectResult>());
		}

		[Test]
		public async Task WithoutRequestData_ReturnBadRequestError()
		{
			var controller = await Initialize();

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
			var requestData = new AnalysisCompleteNotificationData(0, null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

			var response = await controller.NotifyAnalysisComplete(requestData);

			Assert.That(response, Is.TypeOf<BadRequestResult>() | Is.TypeOf<BadRequestObjectResult>());
		}

		[TearDown]
		public void TearDown()
		{
			mailServer?.Dispose();
		}
	}
}