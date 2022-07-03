using Microsoft.AspNetCore.Mvc;
using ReportService.Models;
using ReportService.Repositories;

namespace ReportService.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ReportController : Controller
	{
		private readonly IReportRepository _reportRepository;
		private readonly ILogger<ReportController> _logger;

		public ReportController(IReportRepository reportRepository, ILogger<ReportController> logger)
		{
			_reportRepository = reportRepository;
			_logger = logger;
		}

		[HttpGet("{avatarName}/{scenario}/{algorithm}")]
		public async Task<IActionResult> GetAnalysisResults(string avatarName, string scenario, string algorithm)
		{
			if (string.IsNullOrEmpty(avatarName) || string.IsNullOrEmpty(scenario) || string.IsNullOrEmpty(algorithm))
			{
				return BadRequest();
			}

			AnalysisInfo info = new AnalysisInfo(avatarName, scenario, algorithm);
			var analysis = await _reportRepository.GetReportFromFile(info);
			if (analysis == null)
			{
				return NotFound();
			}

			return Ok(analysis);
		}
	}
}