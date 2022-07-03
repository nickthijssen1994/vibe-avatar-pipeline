using Microsoft.AspNetCore.Mvc;
using NotificationService.Services;
using Models.Exceptions;
using System.Net.Mime;
using MessagingNetwork.Messages;

namespace NotificationService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationController : ControllerBase
	{
		private readonly ILogger<NotificationController> _logger;
		private readonly INotificationService _notificationService;

		public NotificationController(INotificationService notificationService, ILogger<NotificationController> logger)
		{
			_notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
			_logger = logger;
		}

		[HttpPost]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> NotifyAnalysisComplete([FromBody] AnalysisCompleteNotificationData requestData)
		{
			if ((requestData?.UserID ?? 0) == 0 || requestData?.AvatarName == null)
				return BadRequest("Requestdata is incomplete");

			try
			{
				await _notificationService.NotifyAnalysisCompleteAsync(requestData.UserID, requestData.AvatarName);
			}
			catch (ResourceNotFoundException e)
			{
				_logger.LogWarning(e.Message);
				return NotFound();
			}

			return Ok();
		}
	}
}