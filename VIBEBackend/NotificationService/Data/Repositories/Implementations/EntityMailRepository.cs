using Microsoft.EntityFrameworkCore;
using Models.Exceptions;
using NotificationService.Data.Contexts;

namespace NotificationService.Data.Repositories.Implementations
{
	public class EntityMailRepository : IMailRepository
	{
		private readonly NotificationServiceContext _context;

		public EntityMailRepository(NotificationServiceContext context)
		{
			_context = context;
		}

		public async Task<Contact> GetContactByIdAsync(int id)
		{
			var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);
			if (contact is null)
			{
				throw new ResourceNotFoundException(nameof(_context.Contacts));
			}

			return contact;
		}
	}
}