using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LinkedIt.Core.Models.User;
using LinkedIt.DataAcess.Context;
using LinkedIt.DataAcess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace LinkedIt.DataAcess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _db;

		private readonly IMapper _mapper;
		private readonly IConfiguration _config;

		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public IUserRepository User { get; private set; }
		public ILinkUserRepository LinkUser { get; private set; }
		public IPhantomSignalRepository PhantomSignal { get; private set; }
		public IPhantomSignalUpRepository PhantomSignalUp { get; private set; }
		public IPhantomSignalDownRepository PhantomSignalDown { get; private set; }
		public IPhantomSignalCommentRepository PhantomSignalComment { get; private set; }
		public IPhantomResignalRepository PhantomResignal { get; private set; }
		public IWhisperRepository Whisper { get; private set; }

		public UnitOfWork(ApplicationDbContext db, IMapper mapper, IConfiguration config,
			UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			this._db = db;
			this._mapper = mapper;
			this._config = config;
			this._userManager = userManager;
			this._roleManager = roleManager;

			User = new UserRepository(db, userManager, roleManager, config, mapper);
			LinkUser = new LinkUserRepository(db);
			PhantomSignal = new PhantomSignalRepository(db, mapper);
			PhantomSignalUp = new PhantomSignalUpRepository(db);
			PhantomSignalDown = new PhantomSignalDownRepository(db);
			PhantomSignalComment = new PhantomSignalCommentRepository(db, mapper);
			PhantomResignal = new PhantomResignalRepository(db, mapper);
			Whisper = new WhisperRepository(db, mapper);
		}

		public async Task<bool> SaveAsync()
		{
			var success = await _db.SaveChangesAsync();
			return success > 0;
		}
	}
}
