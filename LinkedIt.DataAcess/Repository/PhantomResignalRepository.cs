using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LinkedIt.Core.DTOs.PhantomResignal;
using LinkedIt.Core.Models.Phantom_Signal;
using LinkedIt.DataAcess.Context;
using LinkedIt.DataAcess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LinkedIt.DataAcess.Repository
{
	public class PhantomResignalRepository : GenericRepository<PhantomResignal>, IPhantomResignalRepository
	{
		private readonly ApplicationDbContext _db;
		private readonly IMapper _mapper;
		public PhantomResignalRepository(ApplicationDbContext db, IMapper mapper) : base(db)
		{
			this._db = db;
			this._mapper = mapper;
		}

		public async Task<ResignalDetailsDTO> GetPhantomReSignalInDetailsAsync(int reSignalId)
		{
			var resignalDetailsDto = await _db.PhantomResignals
				.Where(r => r.Id == reSignalId)
				.ProjectTo<ResignalDetailsDTO>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync();

			return resignalDetailsDto;
		}
	}
}
