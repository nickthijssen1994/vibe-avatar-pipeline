using MessagingNetwork;
using MessagingNetwork.Messages;

namespace AnalysisService.Services.Implementations
{
	public class AnalysisServiceImpl : IAnalysisService
	{
		private readonly ILogger<AnalysisServiceImpl> _logger;
		private readonly IBus _bus;

		public AnalysisServiceImpl(ILogger<AnalysisServiceImpl> logger, IBus bus)
		{
			_logger = logger;
			_bus = bus;
		}

		public async Task StartAnalysisAsync(AnalysisRequest analysisRequest)
		{
			var avatarName = analysisRequest.AvatarName;

			_logger.LogInformation("Starting {0} Analysis For {1}. File:[{2}]", analysisRequest.Algorithm,
				analysisRequest.AvatarName, analysisRequest.FileName);

			//TODO choose messagename based on algorithm
			await _bus.SendAsync("start_openface_analysis", analysisRequest);
		}
	}
}