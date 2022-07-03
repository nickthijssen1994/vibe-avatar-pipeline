namespace NotificationService.Data.Repositories
{
	public interface IMailRepository
	{
		Task<Contact> GetContactByIdAsync(int id);
	}
}