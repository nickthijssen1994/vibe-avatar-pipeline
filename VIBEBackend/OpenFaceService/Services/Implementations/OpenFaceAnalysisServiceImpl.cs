using MessagingNetwork;
using MessagingNetwork.Messages;
using OpenFaceService.Data.Context;
using FileInfo = OpenFaceService.Models.FileInfo;

namespace OpenFaceService.Services.Implementations;

public class OpenFaceAnalysisServiceImpl : IOpenFaceAnalysisService
{
	private readonly IOpenFaceServiceContext _context;
	private readonly ILogger<OpenFaceAnalysisServiceImpl> _logger;
	private readonly IBus _bus;

	public OpenFaceAnalysisServiceImpl(ILogger<OpenFaceAnalysisServiceImpl> logger, IOpenFaceServiceContext context,
		IBus bus)
	{
		_context = context;
		_logger = logger;
		_bus = bus;
	}

	public async Task StartAnalysisAsync(FileInfo fileInfo)
	{
		await _context.AnalyseFile(fileInfo);
		_logger.LogInformation("OpenFace Analysis Completed");
		await _bus.SendAsync("analysis_complete", new AnalysisCompleteNotificationData(1, fileInfo.FileName));
	}
}