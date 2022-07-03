using Microsoft.AspNetCore.Mvc;

namespace AnalysisService.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AnalysisController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private readonly ILogger<AnalysisController> _logger;

		public AnalysisController(ILogger<AnalysisController> logger, IConfiguration configuration)
		{
			_configuration = configuration;
			_logger = logger;
		}

		[HttpGet("{avatar}/{scenario}/{algorithm}")]
		public async Task<IActionResult> Get(string avatar, string scenario, string algorithm)
		{
			HttpClient client = new HttpClient();

			_logger.LogInformation("Analysis Run Request: [{0}][{1}][{2}]", avatar, scenario, algorithm);

			string openFaceRestEndpoint = _configuration.GetValue<string>("OpenFaceRestEndpoint");
			var response = await client.GetAsync(openFaceRestEndpoint + "/analyse/image/iw/avatar-" + avatar.ToLower());

			return Ok("Analysis Running...");
		}
	}
}