using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.Constants;

namespace LinkedIt.DataAcess.Repository.IRepository
{
	public interface IGenericRepository<T> where T : class
	{
		// ---------------------------------------------------------------------
		// ---------------------------------------------------------------------

		T Add(T entity);
		Task<T> AddAsync(T entity);

		// ---------------------------------------------------------------------
		// ---------------------------------------------------------------------

		T Remove(T entity);

		// ---------------------------------------------------------------------
		// ---------------------------------------------------------------------

		IEnumerable<T> AddRange(IEnumerable<T> entities);
		Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

		// ---------------------------------------------------------------------
		// ---------------------------------------------------------------------

		IEnumerable<T> RemoveRange (IEnumerable<T> entities);

		// ---------------------------------------------------------------------
		// ---------------------------------------------------------------------

		T? Find(Expression<Func<T, bool>>? filter, string[]? includeProperties = null);
		Task<T?> FindAsync(Expression<Func<T, bool>>? filter, string[]? includeProperties = null);

		// ---------------------------------------------------------------------
		// ---------------------------------------------------------------------

		Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>>? filter = null, string[]? includeProperties = null,
			int? skip = null, int? take = null, Expression<Func<T, bool>>? orderBy = null, string orderByDirection = OrderBy.Ascending);
	}
}
