using AvatarService.Models;
using AvatarService.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AvatarService.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AvatarController : ControllerBase
	{
		private readonly IAvatarRepository _avatarRepository;
		private readonly ILogger<AvatarController> _logger;

		public AvatarController(IAvatarRepository avatarRepository, ILogger<AvatarController> logger)
		{
			_avatarRepository = avatarRepository;
			_logger = logger;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<AvatarDto>>> GetAll()
		{
			_logger.LogInformation("GetAll Request");
			return await _avatarRepository.GetAvatars();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<AvatarDto>> Get(long id)
		{
			_logger.LogInformation("Get Request");
			var avatar = await _avatarRepository.GetAvatar(id);
			if (avatar == null) return NotFound();

			return avatar;
		}

		[HttpPost]
		public async Task<ActionResult<AvatarDto>> Post(AvatarModel avatar)
		{
			_logger.LogInformation("Post Request");
			await _avatarRepository.AddAvatar(avatar);
			return CreatedAtAction(nameof(Get), new { id = avatar.Id }, avatar);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(long id, AvatarModel avatar)
		{
			_logger.LogInformation("Put Request");
			if (id != avatar.Id) return BadRequest();

			await _avatarRepository.UpdateAvatar(id, avatar);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(long id)
		{
			_logger.LogInformation("Delete Request");
			var avatar = await _avatarRepository.DeleteAvatar(id);
			if (avatar == null) return NotFound();

			return NoContent();
		}
	}
}