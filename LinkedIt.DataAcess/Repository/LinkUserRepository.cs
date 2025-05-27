using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.DTOs.Linker;
using LinkedIt.Core.Models.User;
using LinkedIt.DataAcess.Context;
using LinkedIt.DataAcess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LinkedIt.DataAcess.Repository
{
	public class LinkUserRepository : GenericRepository<UserLink>, ILinkUserRepository
	{
		private readonly ApplicationDbContext _db;
		public LinkUserRepository(ApplicationDbContext db) : base(db)
		{
			this._db = db;
		}

		public async Task<bool?> IsAlreadyLinking(string linkerId, string linkedId)
		{
			var follow = await _db.UserLinks.AnyAsync(uf =>
				uf.LinkerUserId == linkerId && uf.LinkedUserId == linkedId);
			return follow;
		}

		public async Task<bool> LinkUser(string linkerId, string linkedId)
		{
			UserLink userLink = new UserLink
			{
				LinkerUserId = linkerId,
				LinkedUserId = linkedId,
				FollowDate = DateTime.Now,
			};

			await _db.UserLinks.AddAsync(userLink);
			var result = await _db.SaveChangesAsync();

			return result > 0;
		}

		public async Task<bool> UnLinkUser(string linkerId, string linkedId)
		{
			var userLink =
				await _db.UserLinks.FirstOrDefaultAsync(
					uf => uf.LinkerUserId == linkerId && uf.LinkedUserId == linkedId);

			if (userLink == null)
				return false;
			
			_db.UserLinks.Remove(userLink);
			var result = await _db.SaveChangesAsync();

			return result > 0;
		}

		public async Task<IEnumerable<ApplicationUser>> GetMutualLinkersAsync(string userId, string targetUserId)
		{
			var mutualLinkers = await (
				from u in _db.Users
				join ul1 in _db.UserLinks on u.Id equals ul1.LinkedUserId
				join ul2 in _db.UserLinks on u.Id equals ul2.LinkedUserId
				where
					ul1.LinkerUserId == userId
					&&
					ul2.LinkerUserId == targetUserId
				select u
			).ToListAsync();

			return mutualLinkers;
		}

		public async Task<List<LinkerDTO>> GetLinkersDtoAsync(string userId)
		{
			var linkers = await _db.UserLinks
				.AsNoTracking()
				.Where(ul => ul.LinkedUserId == userId)
				.Select(ul => new LinkerDTO
				{
					UserId = ul.Linker.Id,
					UserName = ul.Linker.UserName
				}).ToListAsync();

			return linkers;
		}
	}
}
