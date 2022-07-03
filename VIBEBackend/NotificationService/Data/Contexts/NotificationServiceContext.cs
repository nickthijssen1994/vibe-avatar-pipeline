using Microsoft.EntityFrameworkCore;

namespace NotificationService.Data.Contexts
{
	public class NotificationServiceContext : DbContext
	{
		public NotificationServiceContext(DbContextOptions options) : base(options)
		{
			_ = options ?? throw new ArgumentNullException(nameof(options));
			Contacts = Set<Contact>();
		}

		public DbSet<Contact> Contacts { get; set; }

		public async Task Seed()
		{
			if (!await Contacts.AnyAsync())
			{
				await Contacts.AddRangeAsync(new Contact() { Name = "Bob", Email = "Bob@example.net" },
					new Contact() { Name = "Guus", Email = "Guus@example.net" },
					new Contact() { Name = "Eric", Email = "Eric@example.net" },
					new Contact() { Name = "Kees", Email = "Kees@example.net" },
					new Contact() { Name = "Piet", Email = "Piet@example.net" });
				await SaveChangesAsync();
			}
		}
	}
}