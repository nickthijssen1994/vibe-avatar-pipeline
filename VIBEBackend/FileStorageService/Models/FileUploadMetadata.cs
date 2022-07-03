using System.ComponentModel.DataAnnotations;

namespace FileStorageService.Models;

public class FileUploadMetadata
{
	[Required] public string AvatarName { get; set; }
	[Required] public string Scenario { get; set; }
	[Required] public string Algorithm { get; set; }
	public string? Description { get; set; }
	[Required] public List<IFormFile> Files { get; set; }
}