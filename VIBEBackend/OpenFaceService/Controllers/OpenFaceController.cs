using Microsoft.AspNetCore.Mvc;
using OpenFaceService.Data.Context;
using OpenFaceService.Models;
using FileInfo = OpenFaceService.Models.FileInfo;

namespace OpenFaceService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OpenFaceController : ControllerBase
	{
		private readonly ILogger<OpenFaceController> _logger;
		private readonly IOpenFaceServiceContext _context;

		public OpenFaceController(IOpenFaceServiceContext context, ILogger<OpenFaceController> logger)
		{
			_context = context;
			_logger = logger;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<FileModel>>> GetAll()
		{
			List<FileModel> files;
			try
			{
				files = await _context.GetAllFiles();
			}
			catch (Exception e)
			{
				return NotFound(e);
			}

			return files;
		}

		[HttpPost]
		public async Task<IActionResult> Post(FileInfo fileInfo)
		{
			_logger.LogInformation("Run Analysis Request");
			await _context.AnalyseFile(fileInfo);
			return Ok();
		}
	}
}