using ReportService.Models;

namespace ReportService.Repositories
{
	public interface IReportRepository
	{
		Task<ReportDto> GetReportFromFile(AnalysisInfo info);
	}
}