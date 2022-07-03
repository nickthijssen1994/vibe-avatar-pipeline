namespace AvatarService.Models;

public class AvatarDto
{
	public long Id { get; set; }
	public string Name { get; set; }
	public List<AnalysisDto>? Analysis { get; set; }
}