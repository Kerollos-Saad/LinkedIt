using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
	}
}
