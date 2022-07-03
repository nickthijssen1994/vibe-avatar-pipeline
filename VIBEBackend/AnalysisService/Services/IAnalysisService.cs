using MessagingNetwork.Messages;

namespace AnalysisService.Services
{
	public interface IAnalysisService
	{
		public Task StartAnalysisAsync(AnalysisRequest analysisRequest);
	}
}