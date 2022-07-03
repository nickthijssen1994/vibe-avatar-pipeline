using AvatarService.Models;
using Microsoft.EntityFrameworkCore;

namespace AvatarService.Data
{
	public class AvatarContext : DbContext
	{
		public AvatarContext(DbContextOptions<AvatarContext> options) : base(options)
		{
			Avatars = Set<AvatarModel>();
			Analysis = Set<AnalysisModel>();
			Files = Set<FileModel>();
		}

		public DbSet<AvatarModel> Avatars { get; set; }
		public DbSet<AnalysisModel> Analysis { get; set; }
		public DbSet<FileModel> Files { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AvatarModel>().HasMany(b => b.Analysis).WithOne();
			modelBuilder.Entity<AnalysisModel>().HasMany(b => b.Files).WithOne();
		}
	}
}