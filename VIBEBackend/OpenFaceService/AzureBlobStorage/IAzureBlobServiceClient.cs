using OpenFaceService.Models;
using File = OpenFaceService.Models.File;

namespace OpenFaceService.AzureBlobStorage;

public interface IAzureBlobServiceClient
{
	Task<List<ContainerDescription>> GetAllContainers();
	Task<List<FileDescription>> GetAllFilesFromContainer(string containerName);
	Task<FileDescription?> UploadFileToContainer(string containerName, File file);
	Task<File?> GetFileFromContainer(string containerName, string fileName);
}