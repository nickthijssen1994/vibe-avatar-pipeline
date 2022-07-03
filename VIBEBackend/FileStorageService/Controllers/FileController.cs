using System.Net;
using FileStorageService.Models;
using FileStorageService.Repositories;
using FileStorageService.Utilities;
using MessagingNetwork;
using Microsoft.AspNetCore.Mvc;

namespace FileStorageService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
	private readonly IFileRepository _fileRepository;
	private readonly ILogger<FileController> _logger;
	private readonly IBus _busController;

	public FileController(IFileRepository fileRepository, ILogger<FileController> logger, IBus busController)
	{
		_fileRepository = fileRepository;
		_logger = logger;
		_busController = busController;
	}

	[HttpGet]
	public async Task<IActionResult> GetAllFiles()
	{
		List<FileDto> files;
		try
		{
			files = await _fileRepository.GetAllFiles();
		}
		catch (Exception e)
		{
			return NotFound(e);
		}

		return Ok(files);
	}

	[HttpGet("{avatarName}/{analysisId}")]
	public async Task<IActionResult> GetFilesFromAvatarAnalysis(string avatarName, string analysisId)
	{
		List<FileDto>? files;
		try
		{
			files = await _fileRepository.GetFilesFromAvatarAnalysis(avatarName, analysisId);
		}
		catch (Exception e)
		{
			return NotFound(e);
		}

		return Ok(files);
	}

	[HttpPost]
	[Route(nameof(Upload))]
	public async Task<ActionResult<UploadSuccesfullResponse>> Upload([FromForm] FileUploadMetadata fileUploadMetadata)
	{
		_logger.LogInformation("Received File Upload");

		if (Request.ContentType is null || !MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
		{
			ModelState.AddModelError("File", "The request couldn't be processed (Error 1).");
			return BadRequest(HttpStatusCode.UnsupportedMediaType);
		}

		var avatarAnalysisUpload = new AvatarAnalysisUpload
		{
			AvatarName = fileUploadMetadata.AvatarName,
			AvatarVersion = fileUploadMetadata.Scenario,
			Algorithm = fileUploadMetadata.Algorithm,
			UploadDT = DateTime.UtcNow,
			Files = new List<FileUpload>()
		};

		foreach (var file in fileUploadMetadata.Files)
		{
			var fileUpload = await ReadFile(file);
			avatarAnalysisUpload.Files.Add(fileUpload);
		}

		_logger.LogInformation("Uploading file for analysis: ");

		List<FileUploadedResult> result;
		try
		{
			result = await _fileRepository.UploadFile(avatarAnalysisUpload);
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}

		if (result.Count == 0)
		{
			return BadRequest("Could Not Upload File.");
		}

		await _busController.SendAsync("file_uploaded", result);
		_logger.LogInformation("Send File Uploaded Message To Bus");

		// foreach (var fileUploadedResult in result)
		// {
		// 	await _busController.SendAsync("file_uploaded", fileUploadedResult);
		// 	_logger.LogInformation("Send File Uploaded Message To Bus");
		// }

		var response = new UploadSuccesfullResponse()
		{
			Result = result,
			Message = "Files Uploaded."
		};

		return Ok(response);
	}

	private async Task<FileUpload> ReadFile(IFormFile formFile)
	{
		byte[] streamedFileContent = Array.Empty<byte>();
		await using var memoryStream = new MemoryStream();
		await formFile.CopyToAsync(memoryStream);
		streamedFileContent = memoryStream.ToArray();

		var file = new FileUpload
		{
			FileName = formFile.FileName,
			FileType = formFile.ContentType,
			Content = streamedFileContent
		};

		return file;
	}
}