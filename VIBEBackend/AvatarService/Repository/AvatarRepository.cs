using AvatarService.Data;
using AvatarService.Models;
using MessagingNetwork.Messages;
using Microsoft.EntityFrameworkCore;

namespace AvatarService.Repository
{
	public class AvatarRepository : IAvatarRepository
	{
		private readonly AvatarContext _context;

		public AvatarRepository(AvatarContext context)
		{
			_context = context;
		}

		/// <inheritdoc />
		public async Task<AvatarModel> GetAvatarByName(string name)
		{
			var avatar = await _context.Avatars.Include("Analysis.Files")
				.Where(a => a.Name == name)
				.FirstOrDefaultAsync();
			return avatar;
		}

		/// <inheritdoc />
		public async Task<List<AvatarDto>> GetAvatars()
		{
			var avatars = await _context.Avatars.Include("Analysis.Files").ToListAsync();
			List<AvatarDto> list = new();
			foreach (var m in avatars)
			{
				list.Add(m.Convert());
			}

			return list;
		}

		/// <inheritdoc />
		public async Task<AvatarDto> GetAvatar(long id)
		{
			var avatar = await _context.Avatars.Include("Analysis.Files").Where(a => a.Id == id).FirstOrDefaultAsync();
			var avatarDto = avatar.Convert();
			return avatarDto;
		}

		/// <inheritdoc />
		public async Task<AvatarDto> AddAvatar(AvatarModel avatar)
		{
			var existingAvatar = await _context.Avatars.Include("Analysis.Files")
				.FirstOrDefaultAsync(a => a.Name.Equals(avatar.Name));

			if (existingAvatar == null)
			{
				await _context.Avatars.AddAsync(new AvatarModel() { Name = avatar.Name });
				await _context.SaveChangesAsync();
				existingAvatar = await _context.Avatars.Include("Analysis.Files")
					.FirstOrDefaultAsync(a => a.Name.Equals(avatar.Name));
			}

			return await GetAvatar(existingAvatar.Id);
		}

		/// <inheritdoc />
		public async Task<AnalysisDto> AddAnalysis(AnalysisModel avatar)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public async Task<AvatarDto> AddFileToAnalysis(FileUploadedNotification file)
		{
			var avatar = await _context.Avatars.Include("Analysis.Files")
				.Where(a => a.Name == file.AvatarName)
				.FirstOrDefaultAsync();

			var analysis = avatar.Analysis.SingleOrDefault(a =>
				a.Scenario.Equals(file.Scenario) && a.Algorithm.Equals(file.Algorithm));

			if (analysis != null)
			{
				analysis.Files.Add(new FileModel()
					{ Name = file.FileName, FileType = file.FileType, DownloadLink = file.FileUri });
			}
			else
			{
				analysis = new AnalysisModel()
				{
					Scenario = file.Scenario,
					Algorithm = file.Algorithm,
					Files = new List<FileModel>()
				};
				analysis.Files.Add(new FileModel()
				{
					Name = file.FileName,
					FileType = file.FileType,
					DownloadLink = file.FileUri,
				});
				avatar.Analysis.Add(analysis);
			}

			_context.Entry(avatar).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException) when (!AvatarExists(avatar.Id))
			{
				return null;
			}

			return await GetAvatar(avatar.Id);
		}

		/// <inheritdoc />
		public async Task<AvatarDto> UpdateAvatar(long id, AvatarModel avatar)
		{
			if (id != avatar.Id) return null;

			var existingAvatar = await _context.Avatars.Include("Analysis.Files")
				.Where(a => a.Id == id)
				.FirstOrDefaultAsync();

			var addedAnalysis = avatar.Analysis.Last();
			var newAnalysis = new AnalysisModel();
			newAnalysis.Scenario = addedAnalysis.Scenario;
			newAnalysis.Algorithm = addedAnalysis.Algorithm;
			newAnalysis.Description = addedAnalysis.Description;
			newAnalysis.Files = new List<FileModel>();
			foreach (var file in addedAnalysis.Files)
			{
				newAnalysis.Files.Add(new FileModel()
				{
					DownloadLink = file.DownloadLink,
					FileType = file.FileType,
					Name = file.Name
				});
			}

			existingAvatar.Analysis.Add(newAnalysis);

			_context.Entry(existingAvatar).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException) when (!AvatarExists(id))
			{
				return null;
			}

			return await GetAvatar(id);
		}

		/// <inheritdoc />
		public async Task<AvatarDto> DeleteAvatar(long id)
		{
			var finishedGame = await _context.Avatars.FindAsync(id);
			if (finishedGame == null) return null;

			_context.Avatars.Remove(finishedGame);
			await _context.SaveChangesAsync();

			return null;
		}

		private bool AvatarExists(long id)
		{
			return _context.Avatars.Any(e => e.Id == id);
		}
	}
}