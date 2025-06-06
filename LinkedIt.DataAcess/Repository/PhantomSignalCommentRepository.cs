using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LinkedIt.Core.DTOs.AppUsers;
using LinkedIt.Core.DTOs.PhantomSignal;
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
		private readonly IMapper _mapper;

		public PhantomSignalCommentRepository(ApplicationDbContext db, IMapper mapper) : base(db)
		{
			this._db = db;
			this._mapper = mapper;
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

		public async Task<SignalCommentDetailsDTO> GetCommentPhantomSignalV1Async(int commentId)
		{
			var commentWithDetailsDto = await _db.PhantomSignalsComments
				.Where(c=>c.Id == commentId)
				.ProjectTo<SignalCommentDetailsDTO>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync();

			return commentWithDetailsDto;
		}

		public async Task<SignalCommentDetailsDTO> GetCommentPhantomSignalV2Async(int commentId)
		{
			var commentWithDetailsDto = await _db.PhantomSignalsComments
				.Where(c=>c.Id == commentId)
				.Select(c => new SignalCommentDetailsDTO()
				{
					Id = c.Id,
					Comment = c.Comment,
					SignalCommentDate = c.SignalCommentDate,

					ApplicationUser = new ApplicationUserDTO
					{
						UserName = c.ApplicationUser.UserName,
						FirstName = c.ApplicationUser.FirstName,
						LastName = c.ApplicationUser.LastName,
						Bio = c.ApplicationUser.Bio,
						location = c.ApplicationUser.Location,
						PhoneNumber = c.ApplicationUser.PhoneNumber,
						UserCreated = c.ApplicationUser.UserCreated,
						Gender = c.ApplicationUser.Gender
					},

					PhantomSignal = new PhantomSignalDTO
					{
						Id = c.PhantomSignal.Id,
						SignalContent = c.PhantomSignal.SignalContent,
						SignalDate = c.PhantomSignal.SignalDate,
						PhantomFlag = c.PhantomSignal.PhantomFlag,
						UpCount = c.PhantomSignal.UpCount,
						DownCount = c.PhantomSignal.DownCount,
						CommentCount = c.PhantomSignal.CommentCount,
						ResignalCount = c.PhantomSignal.ResignalCount
					}
				})
				.FirstOrDefaultAsync();

			return commentWithDetailsDto;
		}

		public async Task<bool> DeleteAsync(int commentId)
		{
			var comment = await _db.PhantomSignalsComments
				.FirstOrDefaultAsync(c => c.Id == commentId);
			var signal = await _db.PhantomSignals
				.FirstOrDefaultAsync(s => s.Id == comment.PhantomSignalId);

			if (signal == null)
				return false;

			await using var transaction = await _db.Database.BeginTransactionAsync();
			try
			{
				_db.PhantomSignalsComments.Remove(comment);
				signal.CommentCount--;

				var success = await _db.SaveChangesAsync();
				await transaction.CommitAsync();

				return success > 0;
			}
			catch
			{
				await transaction.RollbackAsync();
				throw;
			}
		}
	}
}
