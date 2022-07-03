using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using OpenFaceService.Models;
using File = OpenFaceService.Models.File;

namespace OpenFaceService.AzureBlobStorage;

public class AzureBlobServiceClient : IAzureBlobServiceClient
{
	private readonly ILogger<AzureBlobServiceClient> _logger;
	private readonly BlobServiceClient _blobServiceClient;
	private readonly IConfiguration _configuration;

	public AzureBlobServiceClient(IConfiguration configuration, ILogger<AzureBlobServiceClient> logger)
	{
		_configuration = configuration;
		_logger = logger;
		string connectionString = _configuration["AzureStorageConnectionString"];
		_blobServiceClient = new BlobServiceClient(connectionString);
	}

	public async Task<List<ContainerDescription>> GetAllContainers()
	{
		List<ContainerDescription> containers = new();

		var blobContainers = _blobServiceClient.GetBlobContainers();

		_logger.LogInformation(blobContainers.ToString());

		await foreach (var blobContainer in _blobServiceClient.GetBlobContainersAsync())
		{
			containers.Add(new ContainerDescription
			{
				Name = blobContainer.Name,
				Files = await GetAllFilesFromContainer(blobContainer.Name)
			});
		}

		return containers;
	}

	public async Task<List<FileDescription>> GetAllFilesFromContainer(string containerName)
	{
		List<FileDescription> files = new();

		if (!CheckIfContainerExists(containerName))
		{
			return files;
		}

		var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

		await foreach (var blobItem in containerClient.GetBlobsAsync())
		{
			var file = new FileDescription()
			{
				Name = blobItem.Name,
				DownloadLink = containerClient.Uri + "/" + blobItem.Name
			};
			files.Add(file);
		}

		return files;
	}

	public async Task<FileDescription?> UploadFileToContainer(string containerName, File file)
	{
		// Create the container and return a container client object
		BlobContainerClient containerClient;

		if (CheckIfContainerExists(containerName))
		{
			containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
		}
		else
		{
			containerClient =
				await _blobServiceClient.CreateBlobContainerAsync(containerName, PublicAccessType.BlobContainer);
		}

		// Get a reference to a blob
		if (CheckIfFileInContainerExists(containerName, file.Name))
		{
			_logger.LogInformation("File already exists");
			return null;
		}

		var blobClient = containerClient.GetBlobClient(file.Name);

		_logger.LogInformation("Uploading file to blob storage as blob: {address}", blobClient.Uri);

		// Upload data from the local file
		Stream stream = new MemoryStream(file.Content);
		await blobClient.UploadAsync(stream);

		var fileUpload = new FileDescription()
		{
			Name = file.Name,
			DownloadLink = blobClient.Uri.ToString()
		};

		return fileUpload;
	}

	public async Task<File?> GetFileFromContainer(string containerName, string fileName)
	{
		if (!CheckIfFileInContainerExists(containerName, fileName)) return null;

		var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
		var blobClient = containerClient.GetBlobClient(fileName);
		var file = new File()
		{
			Name = fileName
		};

		var ms = new MemoryStream();
		await (await blobClient.OpenReadAsync()).CopyToAsync(ms);
		file.Content = ms.ToArray();

		return file;
	}

	private bool CheckIfContainerExists(string containerName)
	{
		return _blobServiceClient.GetBlobContainers().Any(container => container.Name.Equals(containerName));
	}

	private bool CheckIfFileInContainerExists(string containerName, string fileName)
	{
		if (!CheckIfContainerExists(containerName)) return false;

		var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

		return containerClient.GetBlobs().Any(blobItem => blobItem.Name.Equals(fileName));
	}
}