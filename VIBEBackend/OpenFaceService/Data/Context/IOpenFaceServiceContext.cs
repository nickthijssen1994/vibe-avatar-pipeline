using OpenFaceService.Models;
using FileInfo = OpenFaceService.Models.FileInfo;

namespace OpenFaceService.Data.Context
{
	public interface IOpenFaceServiceContext
	{
		Task<List<FileModel>> GetAllFiles();
		Task AnalyseFile(FileInfo fileInfo);
	}
}