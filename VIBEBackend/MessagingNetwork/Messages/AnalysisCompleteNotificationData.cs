namespace MessagingNetwork.Messages
{
	public class AnalysisCompleteNotificationData
	{
		public int UserID { get; set; }
		public string AvatarName { get; set; }
		public string FileName { get; set; }
		public AnalysisCompleteNotificationData() { }

		public AnalysisCompleteNotificationData(int userID, string avatarName)
		{
			UserID = userID;
			AvatarName = avatarName;
		}
	}
}