using Microsoft.EntityFrameworkCore;
using ReportService.Models;

namespace ReportService.Data
{
	public class ReportContext : DbContext
	{
		public ReportContext(DbContextOptions<ReportContext> options) : base(options) { }
		public DbSet<ReportModel> Avatars { get; set; } = null!;
	}
}