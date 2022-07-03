namespace FileStorageService.Models;

public class FileDto
{
	public FileDto(string name)
	{
		Name = name;
	}

	public string Name { get; set; }
	public string? ContainerId { get; set; }
	public string DownloadLink { get; set; }
}