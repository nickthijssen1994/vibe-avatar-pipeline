using File = FileStorage.Models.File;
using CsvHelper;
using System.Globalization;

namespace ReportService.Services
{
	public class CsvReader<T>
	{
		public List<T> ReadFile(File file)
		{
			using (var memoryStream = new MemoryStream(file.Content))
			{
				memoryStream.Position = 0;
				using (var reader = new StreamReader(memoryStream))
				using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
				{
					return csv.GetRecords<T>().ToList();
				}
			}
		}
	}
}