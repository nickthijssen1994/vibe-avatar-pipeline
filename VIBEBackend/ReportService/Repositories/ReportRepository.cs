using CsvHelper;
using CsvHelper.Configuration;
using FileStorage.AzureBlobStorage;
using ReportService.Models;
using System.Globalization;

namespace ReportService.Repositories
{
	public class ReportRepository : IReportRepository
	{
		private readonly IAzureBlobServiceClient _azureBlobServiceClient;
		private readonly ILogger<ReportRepository> _logger;

		public ReportRepository(IAzureBlobServiceClient azureBlobServiceClient, ILogger<ReportRepository> logger)
		{
			_azureBlobServiceClient = azureBlobServiceClient;
			_logger = logger;
		}

		public async Task<ReportDto> GetReportFromFile(AnalysisInfo info)
		{
			string containerName = "avatar-" + info.AvatarName.ToLower();
			var filesInContainer = await _azureBlobServiceClient.GetAllFilesFromContainer(containerName);
			string directory = info.AvatarName + "/" + info.Scenario + "/" + info.Algorithm + "/";
			var Files = filesInContainer.Where(f => f.Name.StartsWith(directory)).ToList();
			;
			var CSVFiles = Files.Where(f => f.Name.EndsWith(".csv")).ToList();
			var CSVFile = CSVFiles.First();
			List<OpenFaceModel> results = new();
			var rawReportData = await _azureBlobServiceClient.GetFileFromContainer(containerName, CSVFile.Name);
			using (var memoryStream = new MemoryStream(rawReportData.Content))
			{
				CsvConfiguration configuration = new CsvConfiguration(CultureInfo.InvariantCulture);
				configuration.Delimiter = ",";
				memoryStream.Position = 0;
				using (var reader = new StreamReader(memoryStream))
				using (var csv = new CsvReader(reader, configuration))
				{
					results = csv.GetRecords<OpenFaceModel>().ToList();
				}
			}

			OpenFaceModel result = results[0];
			OpenFaceEmotionModel resultEmotion = new OpenFaceEmotionModel(result.ActionUnit1, result.ActionUnit2,
				result.ActionUnit4, result.ActionUnit5, result.ActionUnit6, result.ActionUnit7, result.ActionUnit12,
				result.ActionUnit15, result.ActionUnit20, result.ActionUnit23, result.ActionUnit26);
			List<ResultModel> resultList = new List<ResultModel>();
			resultList.Add(new ResultModel("Happiness", Convert.ToDouble(resultEmotion.Happiness)));
			resultList.Add(new ResultModel("Sadness", Convert.ToDouble(resultEmotion.Sadness)));
			resultList.Add(new ResultModel("Surprise", Convert.ToDouble(resultEmotion.Surprise)));
			resultList.Add(new ResultModel("Fear", Convert.ToDouble(resultEmotion.Fear)));
			resultList.Add(new ResultModel("Anger", Convert.ToDouble(resultEmotion.Anger)));
			ReportDto reportDto =
				new ReportDto(CSVFile.Name, "OpenFace", "Emotion", Convert.ToInt32(result.Confidence));
			reportDto.Results = resultList;
			return reportDto;
		}
	}
}