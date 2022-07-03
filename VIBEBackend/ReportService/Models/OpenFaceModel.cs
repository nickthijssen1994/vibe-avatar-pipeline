using CsvHelper.Configuration.Attributes;

namespace ReportService.Models
{
	public class OpenFaceModel
	{
		[Name("confidence")] public float Confidence { get; set; }
		[Name("AU01_r")] public double ActionUnit1 { get; set; }
		[Name("AU02_r")] public double ActionUnit2 { get; set; }
		[Name("AU04_r")] public double ActionUnit4 { get; set; }
		[Name("AU05_r")] public double ActionUnit5 { get; set; }
		[Name("AU06_r")] public double ActionUnit6 { get; set; }
		[Name("AU07_r")] public double ActionUnit7 { get; set; }
		[Name("AU09_r")] public double ActionUnit9 { get; set; }
		[Name("AU10_r")] public double ActionUnit10 { get; set; }
		[Name("AU12_r")] public double ActionUnit12 { get; set; }
		[Name("AU14_r")] public double ActionUnit14 { get; set; }
		[Name("AU15_r")] public double ActionUnit15 { get; set; }
		[Name("AU17_r")] public double ActionUnit17 { get; set; }
		[Name("AU20_r")] public double ActionUnit20 { get; set; }
		[Name("AU23_r")] public double ActionUnit23 { get; set; }
		[Name("AU25_r")] public double ActionUnit25 { get; set; }
		[Name("AU26_r")] public double ActionUnit26 { get; set; }
		[Name("AU45_r")] public double ActionUnit45 { get; set; }
	}
}