using MessagingNetwork.Messages;

namespace AvatarService.Services;

public interface IAvatarService
{
	public Task StartHandleFileUploadedAsync(List<FileUploadedNotification> uploadedFiles);
}