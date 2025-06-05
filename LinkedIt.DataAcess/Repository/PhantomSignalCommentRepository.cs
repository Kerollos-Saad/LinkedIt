using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.DTOs.SignalComment;
using LinkedIt.Core.Models.Phantom_Signal;
using LinkedIt.DataAcess.Context;
using LinkedIt.DataAcess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LinkedIt.DataAcess.Repository
{
	public class PhantomSignalCommentRepository : GenericRepository<PhantomSignalComment>, IPhantomSignalCommentRepository
	{
		private readonly ApplicationDbContext _db;
		public PhantomSignalCommentRepository(ApplicationDbContext db) : base(db)
		{
			this._db = db;
		}

		public async Task<int> AddCommentPhantomSignalAsync(string userId, Guid phantomSignalId,
			PhantomSignalCommentDTO phantomSignalCommentDto)
		{
			var signal = await _db.PhantomSignals.FirstOrDefaultAsync(s => s.Id == phantomSignalId);
			var comment = new PhantomSignalComment
			{
				SignalCommentDate = DateTime.Now,
				Comment = phantomSignalCommentDto.Comment,
				ApplicationUserId = userId,
				PhantomSignalId = phantomSignalId
			};

			await _db.PhantomSignalsComments.AddAsync(comment);
			signal.CommentCount++;

			var success = await _db.SaveChangesAsync();
			if (success > 0)
				return comment.Id;

			return 0;
		}

		public async Task<int> UpdateCommentPhantomSignalAsync(string userId, int commentId,
			PhantomSignalCommentDTO phantomSignalCommentDto)
		{
			var existComment = await _db.PhantomSignalsComments.FirstOrDefaultAsync(c=>c.Id == commentId);

			existComment.SignalCommentDate = DateTime.Now;
			existComment.Comment = phantomSignalCommentDto.Comment;

			var success = await _db.SaveChangesAsync();
			if (success > 0)
				return existComment.Id;

			return 0;
		}

		public async Task<bool> IsCommentHisPropertyAsync(string userId, int commentId)
		{
			var exist = await _db.PhantomSignalsComments.CountAsync(c =>
				c.Id == commentId && c.ApplicationUserId == userId);

			return exist == 1;
		}
	}
}
