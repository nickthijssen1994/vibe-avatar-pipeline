namespace ReportService.Models
{
	public class ResultModel
	{
		public string ValueName { get; set; }
		public double Content { get; set; }

		public ResultModel(string valueName, double content)
		{
			ValueName = valueName;
			Content = content;
		}
	}
}