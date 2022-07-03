namespace AvatarService.Models;

public class AnalysisModel
{
	public long Id { get; set; }
	public string Scenario { get; set; }
	public string? Algorithm { get; set; }
	public string? Description { get; set; }
	public virtual ICollection<FileModel> Files { get; set; }

	public AnalysisDto Convert()
	{
		var dto = new AnalysisDto
		{
			Id = Id,
			Scenario = Scenario,
			Algorithm = Algorithm,
			Description = Description
		};

		List<FileDto>? files = new();
		if (Files != null)
			foreach (var item in Files)
			{
				files.Add(item.Convert());
			}

		dto.Files = files;
		return dto;
	}
}