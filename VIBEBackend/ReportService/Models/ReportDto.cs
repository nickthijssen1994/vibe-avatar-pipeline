namespace ReportService.Models
{
	public class ReportDto
	{
		public string AnalysisName { get; set; }
		public string ValueName { get; set; }
		public string FileName { get; set; }
		public int Confidence { get; set; }
		public List<string> Notes { get; set; }
		public List<ResultModel> Results { get; set; }

		public ReportDto(string fileName, string analysisName, string valueName, int confidence)
		{
			FileName = fileName;
			AnalysisName = analysisName;
			ValueName = valueName;
			Confidence = confidence;
			Notes = new List<string>();
			Results = new List<ResultModel>();
		}
	}
}