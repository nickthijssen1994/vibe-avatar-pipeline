namespace FileStorageService.Models;

public class FileUploadedResult
{
	public string FileName { get; set; }
	public string FileDirectory { get; set; }
	public string FileType { get; set; }
	public string ContainerId { get; set; }
	public string FileUri { get; set; }
	public string AvatarName { get; set; }
	public string Scenario { get; set; }
	public string Algorithm { get; set; }
}