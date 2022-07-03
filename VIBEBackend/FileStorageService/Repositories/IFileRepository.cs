using FileStorageService.Models;

namespace FileStorageService.Repositories;

public interface IFileRepository
{
	Task<List<FileDto>> GetAllFiles();
	Task<List<FileDto>?> GetFilesFromAvatarAnalysis(string avatarName, string analysisId);
	Task<List<FileUploadedResult>> UploadFile(AvatarAnalysisUpload avatarAnalysisUpload);
}