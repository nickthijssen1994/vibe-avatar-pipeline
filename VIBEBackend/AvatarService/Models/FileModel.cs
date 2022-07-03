namespace AvatarService.Models
{
	public class FileModel
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string? FileType { get; set; }
		public string? DownloadLink { get; set; }

		public FileDto Convert()
		{
			var dto = new FileDto
			{
				Name = Name,
				FileType = FileType,
				DownloadLink = DownloadLink
			};

			return dto;
		}
	}
}