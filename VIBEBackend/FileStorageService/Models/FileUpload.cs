namespace FileStorageService.Models;

public class FileUpload
{
	public long Id { get; set; }
	public string FileName { get; set; }
	public string FileType { get; set; }
	public byte[] Content { get; set; }
}