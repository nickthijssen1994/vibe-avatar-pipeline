namespace AvatarService.Models
{
	public class AvatarModel
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public virtual ICollection<AnalysisModel> Analysis { get; set; }

		public AvatarDto Convert()
		{
			var dto = new AvatarDto
			{
				Id = Id,
				Name = Name
			};

			List<AnalysisDto>? analysisList = new();
			if (Analysis != null)
				foreach (var item in Analysis)
				{
					analysisList.Add(item.Convert());
				}

			dto.Analysis = analysisList;
			return dto;
		}
	}
}