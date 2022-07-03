using FileStorage.AzureBlobStorage;
using FileStorage.Models;
using FileStorageService.Models;
using File = FileStorage.Models.File;

namespace FileStorageService.Repositories;

public class FileRepository : IFileRepository
{
	private readonly IAzureBlobServiceClient _azureBlobServiceClient;
	private readonly ILogger<FileRepository> _logger;

	public FileRepository(IAzureBlobServiceClient azureBlobServiceClient, ILogger<FileRepository> logger)
	{
		_azureBlobServiceClient = azureBlobServiceClient;
		_logger = logger;
	}

	/// <inheritdoc />
	public async Task<List<FileDto>> GetAllFiles()
	{
		List<FileDto> list = new();

		foreach (ContainerDescription container in await _azureBlobServiceClient.GetAllContainers())
		{
			if (container.Name.StartsWith("avatar"))
				foreach (var fileDescription in container.Files)
				{
					list.Add(new FileDto(fileDescription.Name)
					{
						ContainerId = container.Name,
						DownloadLink = fileDescription.DownloadLink
					});
				}
		}

		return list;
	}

	/// <inheritdoc />
	public async Task<List<FileDto>?> GetFilesFromAvatarAnalysis(string avatarName, string analysisId)
	{
		var fileDescriptions = await _azureBlobServiceClient.GetAllFilesFromContainer("avatar-" + avatarName.ToLower());

		if (fileDescriptions == null)
		{
			return null;
		}

		List<FileDto>? fileDtos = new();
		foreach (var fileDescription in fileDescriptions)
		{
			fileDtos.Add(new FileDto(fileDescription.Name)
			{
				ContainerId = avatarName,
				DownloadLink = fileDescription.DownloadLink
			});
		}

		return fileDtos;
	}

	/// <inheritdoc />
	public async Task<List<FileUploadedResult>> UploadFile(AvatarAnalysisUpload avatarAnalysisUpload)
	{
		_logger.LogInformation("Uploading New File");

		List<FileUploadedResult> fileUploadedResults = new();

		var containerName = "avatar-" + avatarAnalysisUpload.AvatarName.ToLower();

		foreach (var fileUpload in avatarAnalysisUpload.Files)
		{
			var file = new File
			{
				Name = avatarAnalysisUpload.AvatarName + "/" + avatarAnalysisUpload.AvatarVersion + "/" +
				       avatarAnalysisUpload.Algorithm + "/" + fileUpload.FileName,
				Content = fileUpload.Content
			};

			var fileDescription = await _azureBlobServiceClient.UploadFileToContainer(containerName, file);

			if (fileDescription != null)
			{
				fileUploadedResults.Add(new FileUploadedResult()
				{
					FileDirectory = avatarAnalysisUpload.AvatarName + "/" + avatarAnalysisUpload.AvatarVersion + "/" +
					                avatarAnalysisUpload.Algorithm,
					FileName = fileUpload.FileName,
					AvatarName = avatarAnalysisUpload.AvatarName,
					ContainerId = containerName,
					FileType = fileUpload.FileType,
					FileUri = fileDescription.DownloadLink,
					Scenario = avatarAnalysisUpload.AvatarVersion,
					Algorithm = avatarAnalysisUpload.Algorithm
				});
			}
		}

		return fileUploadedResults;
	}
}