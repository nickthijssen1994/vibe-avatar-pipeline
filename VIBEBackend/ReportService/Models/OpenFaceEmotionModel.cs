namespace ReportService.Models
{
	public class OpenFaceEmotionModel
	{
		public int Happiness { get; set; }
		public int Sadness { get; set; }
		public int Surprise { get; set; }
		public int Fear { get; set; }
		public int Anger { get; set; }

		public OpenFaceEmotionModel(double actionUnit1, double actionUnit2, double actionUnit4, double actionUnit5,
			double actionUnit6, double actionUnit7, double actionUnit12, double actionUnit15, double actionUnit20,
			double actionUnit23, double actionUnit26)
		{
			Happiness = Convert.ToInt32((actionUnit6 + actionUnit12) / 2 * 20);
			Sadness = Convert.ToInt32((actionUnit1 + actionUnit4 + actionUnit15) / 3 * 20);
			Surprise = Convert.ToInt32((actionUnit1 + actionUnit2 + actionUnit5 + actionUnit26) / 4 * 20);
			Fear = Convert.ToInt32((actionUnit1 + actionUnit2 + actionUnit4 + actionUnit5 + actionUnit7 + actionUnit20 +
			                        actionUnit26) / 7 * 20);
			Anger = Convert.ToInt32((actionUnit4 + actionUnit5 + actionUnit7 + actionUnit23) / 4 * 20);
		}
	}
}