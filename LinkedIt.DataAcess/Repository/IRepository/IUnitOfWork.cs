using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.DataAcess.Repository.IRepository
{
	public interface IUnitOfWork
	{
		IUserRepository User { get; }
		ILinkUserRepository LinkUser { get; }
		IPhantomSignalRepository PhantomSignal { get; }
		IPhantomSignalUpRepository PhantomSignalUp { get; }
		IPhantomSignalDownRepository PhantomSignalDown { get; }
		IPhantomSignalCommentRepository PhantomSignalComment { get; }
		IPhantomResignalRepository PhantomResignal { get; }
		IWhisperRepository Whisper { get; }
		Task<bool> SaveAsync();
	}
}
