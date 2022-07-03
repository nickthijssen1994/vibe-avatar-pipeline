namespace FileStorage.Models;

public class ContainerDescription
{
	public string Name { get; set; }
	public List<FileDescription> Files { get; set; }

	public ContainerDescription()
	{
		Files = new List<FileDescription>();
	}
}