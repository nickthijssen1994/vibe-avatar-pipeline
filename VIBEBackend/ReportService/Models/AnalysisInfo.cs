namespace ReportService.Models
{
	public class AnalysisInfo
	{
		public string AvatarName { get; set; }
		public string Scenario { get; set; }
		public string Algorithm { get; set; }

		public AnalysisInfo(string avatarName, string scenario, string algorithm)
		{
			AvatarName = avatarName;
			Scenario = scenario;
			Algorithm = algorithm;
		}
	}
}