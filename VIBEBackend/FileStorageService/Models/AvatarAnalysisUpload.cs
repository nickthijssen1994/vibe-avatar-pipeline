namespace FileStorageService.Models;

public class AvatarAnalysisUpload
{
	public int Id { get; set; }
	public string AvatarName { get; set; }
	public string AvatarVersion { get; set; }
	public string Algorithm { get; set; }
	public List<FileUpload> Files { get; set; }
	public string Description { get; set; }
	public DateTime UploadDT { get; set; }
}