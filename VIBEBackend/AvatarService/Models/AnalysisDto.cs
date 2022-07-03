namespace AvatarService.Models;

public class AnalysisDto
{
	public long Id { get; set; }
	public string Scenario { get; set; }
	public string? Algorithm { get; set; }
	public string? Description { get; set; }
	public List<FileDto>? Files { get; set; }
}