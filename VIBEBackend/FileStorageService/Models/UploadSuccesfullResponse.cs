namespace FileStorageService.Models;

public class UploadSuccesfullResponse
{
	public string Message { get; set; }
	public List<FileUploadedResult> Result { get; set; }
}