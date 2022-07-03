namespace OpenFaceService.Models;

public class FileModel
{
	public FileModel(string name)
	{
		Name = name;
	}

	public string Name { get; set; }
	public string? ContainerId { get; set; }
	public string DownloadLink { get; set; }
}