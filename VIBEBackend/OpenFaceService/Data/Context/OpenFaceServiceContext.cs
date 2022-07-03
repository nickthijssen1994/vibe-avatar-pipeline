using System.Diagnostics;
using System.Runtime.InteropServices;
using OpenFaceService.AzureBlobStorage;
using OpenFaceService.Models;
using File = System.IO.File;
using FileInfo = OpenFaceService.Models.FileInfo;

namespace OpenFaceService.Data.Context
{
	public class OpenFaceServiceContext : IOpenFaceServiceContext
	{
		private readonly string? _openFacePath;
		private readonly IAzureBlobServiceClient _azureBlobServiceClient;
		private readonly ILogger<OpenFaceServiceContext> _logger;

		public OpenFaceServiceContext(IAzureBlobServiceClient azureBlobServiceClient,
			ILogger<OpenFaceServiceContext> logger, IConfiguration configuration)
		{
			_azureBlobServiceClient = azureBlobServiceClient;
			_logger = logger;

			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				_openFacePath = "/home/openface-build/build/bin/";
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				_openFacePath = configuration["OpenFaceExecutablePath"];
			}
		}

		/// <inheritdoc />
		public async Task<List<FileModel>> GetAllFiles()
		{
			List<FileModel> list = new();

			foreach (ContainerDescription container in await _azureBlobServiceClient.GetAllContainers())
			{
				if (container.Name.StartsWith("avatar"))
					foreach (var fileDescription in container.Files)
					{
						list.Add(new FileModel(fileDescription.Name)
						{
							ContainerId = container.Name,
							DownloadLink = fileDescription.DownloadLink
						});
					}
			}

			return list;
		}

		public async Task AnalyseFile(FileInfo fileInfo)
		{
			_logger.LogInformation("File Info {0}-{1}-{2}", fileInfo.ContainerName, fileInfo.FileName,
				fileInfo.FilePath);
			_logger.LogInformation("Retrieving File From BlobStorage");
			var rawReportData = await _azureBlobServiceClient.GetFileFromContainer(fileInfo.ContainerName,
				fileInfo.FilePath + "/" + fileInfo.FileName);

			string filename = Path.GetFileName(rawReportData.Name);
			_logger.LogInformation("Filename {0}", filename);

			string path = "";
			string filePath = "";

			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				_logger.LogInformation("Using Linux Directory Structure");
				path = "/home/openface-build/awaiting-processing";
				_logger.LogInformation("File Directory {0}", path);
				filePath = Path.Combine(path, filename);
				_logger.LogInformation("File Path {0}", filePath);
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				_logger.LogInformation("Using Windows Directory Structure");
				path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "awaiting-processing");
				_logger.LogInformation("File Directory {0}", path);
				filePath = Path.Combine(path, filename);
				_logger.LogInformation("File Path {0}", filePath);
			}

			try
			{
				if (File.Exists(filePath))
				{
					_logger.LogInformation("File Already Exists. Deleting Existing File...");
					File.Delete(filePath);
					_logger.LogInformation("Existing File Deleted.");
				}

				var info = new DirectoryInfo(path);
				if (!info.Exists)
				{
					_logger.LogInformation("Creating Temp Folder.");
					info.Create();
				}

				using var memoryStream = new MemoryStream(rawReportData.Content);
				await using var outputFileStream = new FileStream(filePath, FileMode.Create);
				await memoryStream.CopyToAsync(outputFileStream);
				_logger.LogInformation("Downloaded File And Saved To: {0}", filePath);
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, "Could Not Save File");
			}

			var analysisCompleted = StartOpenFaceProcess(filePath, " -wild");

			var resultsDirectory = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "processed"));

			_logger.LogInformation("Result Files:");

			_logger.LogInformation("Uploading results to Blob Storage");

			foreach (System.IO.FileInfo resultFile in resultsDirectory.GetFiles())
			{
				if (!resultFile.Name.EndsWith(".csv")) continue;

				byte[] streamedFileContent = Array.Empty<byte>();
				using var ms = new MemoryStream();
				await using var fileStream = new FileStream(resultFile.FullName, FileMode.Open, FileAccess.Read);
				await fileStream.CopyToAsync(ms);

				streamedFileContent = ms.ToArray();

				var file = new Models.File()
				{
					Name = fileInfo.FilePath + "/" + resultFile.Name,
					Content = streamedFileContent
				};

				await _azureBlobServiceClient.UploadFileToContainer(fileInfo.ContainerName, file);
				_logger.LogInformation("CSV File Uploaded To Blob Storage");
				break;
			}
		}

		private bool StartOpenFaceProcess(string filePath, string arguments)
		{
			string[] imageTypes = { ".png", ".jpg", ".jpeg" };
			string[] videoTypes = { ".mp4", ".mkv" };
			string fileExtension = Path.GetExtension(filePath);

			string executable;

			if (imageTypes.Contains(fileExtension))
			{
				executable = _openFacePath + "FaceLandmarkImg";
				_logger.LogInformation("Starting OpenFace Image Analysis.");
			}
			else if (videoTypes.Contains(fileExtension))
			{
				executable = _openFacePath + "FaceLandmarkVid";
				_logger.LogInformation("Starting OpenFace Video Analysis.");
			}
			else
			{
				_logger.LogInformation("Can not analyse file. File extension {0} is not supported by OpenFace",
					fileExtension);
				return false;
			}

			_logger.LogInformation(
				"Starting Analysis. File: [{filePath}] Executable: [{executable}] Arguments: [{arguments}]", filePath,
				executable, arguments);
			var fullCommand = executable + " -f " + filePath + " " + arguments;
			_logger.LogInformation("Full Command: {command}", fullCommand);

			bool analysisFinishedSuccessfully;
			try
			{
				using (Process openFaceProcess = new Process())
				{
					var processStartInfo = new ProcessStartInfo
					{
						UseShellExecute = false,
						FileName = executable,
						CreateNoWindow = true,
						RedirectStandardOutput = true,
						Arguments = " -f " + filePath + " " + arguments
					};

					openFaceProcess.StartInfo = processStartInfo;
					openFaceProcess.Start();

					string output = openFaceProcess.StandardOutput.ReadToEnd();
					openFaceProcess.WaitForExit();
					_logger.LogInformation(output);
					_logger.LogInformation("Analysis Finished");
					analysisFinishedSuccessfully = true;
				}
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
				analysisFinishedSuccessfully = false;
			}

			return analysisFinishedSuccessfully;
		}
	}
}