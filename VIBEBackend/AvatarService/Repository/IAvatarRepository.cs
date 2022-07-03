using AvatarService.Models;
using MessagingNetwork.Messages;

namespace AvatarService.Repository;

public interface IAvatarRepository
{
	Task<AvatarModel> GetAvatarByName(string name);
	Task<List<AvatarDto>> GetAvatars();
	Task<AvatarDto> GetAvatar(long id);
	Task<AvatarDto> AddAvatar(AvatarModel avatar);
	Task<AnalysisDto> AddAnalysis(AnalysisModel avatar);
	Task<AvatarDto> AddFileToAnalysis(FileUploadedNotification file);
	Task<AvatarDto> UpdateAvatar(long id, AvatarModel avatar);
	Task<AvatarDto> DeleteAvatar(long id);
}