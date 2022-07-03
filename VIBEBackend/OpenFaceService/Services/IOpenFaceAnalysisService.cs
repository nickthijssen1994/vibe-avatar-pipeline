using FileInfo = OpenFaceService.Models.FileInfo;

namespace OpenFaceService.Services;

public interface IOpenFaceAnalysisService
{
	public Task StartAnalysisAsync(FileInfo fileInfo);
}