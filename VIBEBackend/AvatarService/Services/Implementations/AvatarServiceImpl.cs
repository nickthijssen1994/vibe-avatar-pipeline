using AvatarService.Repository;
using MessagingNetwork;
using MessagingNetwork.Messages;

namespace AvatarService.Services.Implementations;

public class AvatarServiceImpl : IAvatarService
{
	private readonly IAvatarRepository _repository;
	private readonly ILogger<AvatarServiceImpl> _logger;
	private readonly IBus _bus;

	public AvatarServiceImpl(IAvatarRepository repository, ILogger<AvatarServiceImpl> logger, IBus bus)
	{
		_repository = repository;
		_logger = logger;
		_bus = bus;
	}

	public async Task StartHandleFileUploadedAsync(List<FileUploadedNotification> uploadedFiles)
	{
		_logger.LogInformation("Received Files : AvatarService");
		foreach (var uploadedFile in uploadedFiles)
		{
			var addedFile = await _repository.AddFileToAnalysis(uploadedFile);

			var analysisRequest = new AnalysisRequest
			{
				FileName = uploadedFile.FileName,
				FileDirectory = uploadedFile.FileDirectory,
				FileType = uploadedFile.FileType,
				ContainerId = uploadedFile.ContainerId,
				FileUri = uploadedFile.FileUri,
				AvatarName = uploadedFile.AvatarName,
				Scenario = uploadedFile.Scenario,
				Algorithm = uploadedFile.Algorithm
			};
			_logger.LogInformation("Sending Message");
			await _bus.SendAsync("start_analysis", analysisRequest);
			_logger.LogInformation("Send Message");
		}
	}
}