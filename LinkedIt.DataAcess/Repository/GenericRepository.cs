using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinkedIt.Core.Constants;
using LinkedIt.DataAcess.Context;
using LinkedIt.DataAcess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LinkedIt.DataAcess.Repository
{
	internal class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly ApplicationDbContext _db;
		internal DbSet<T> DbSet;

		public GenericRepository(ApplicationDbContext db)
		{
			this._db = db;
			this.DbSet = db.Set<T>();
		}

		public T Add(T entity)
		{
			_db.Set<T>().Add(entity);
			return entity;
		}

		public async Task<T> AddAsync(T entity)
		{
			await _db.Set<T>().AddAsync(entity);
			return entity;
		}

		public T Remove(T entity)
		{
			_db.Set<T>().Remove(entity);
			return entity;
		}

		public IEnumerable<T> AddRange(IEnumerable<T> entities)
		{
			IEnumerable<T> range = entities.ToList();
			_db.Set<T>().AddRange(range);
			return range;
		}

		public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
		{
			IEnumerable<T> range = entities.ToList();
			await _db.Set<T>().AddRangeAsync(range);
			return range;
		}

		public IEnumerable<T> RemoveRange(IEnumerable<T> entities)
		{
			IEnumerable<T> range = entities.ToList();
			_db.Set<T>().RemoveRange(range);
			return range;
		}

		public T? Find(Expression<Func<T, bool>>? filter, string[]? includeProperties = null)
		{
			IQueryable<T?> query = DbSet;

			// Apply Filtering
			if(filter != null)
				query = query.Where(filter);

			// Include Properties if provided
			includeProperties ??= Array.Empty<string>();
			foreach (var property in includeProperties)
				query = query.Include(property);

			return query.FirstOrDefault();
		}

		public Task<T?> FindAsync(Expression<Func<T, bool>>? filter, string[]? includeProperties = null)
		{
			IQueryable<T?> query = DbSet;

			// Apply Filtering
			if (filter != null)
				query = query.Where(filter);

			// Include Properties if provided
			includeProperties ??= Array.Empty<string>();
			foreach (var property in includeProperties)
				query = query.Include(property);

			return query.FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>>? filter = null, string[]? includeProperties = null, int? skip = null, int? take = null,
			Expression<Func<T, bool>>? orderBy = null, string orderByDirection = OrderBy.Ascending)
		{
			IQueryable<T?> query = DbSet;

			// Apply Filtering
			if (filter != null)
				query = query.Where(filter);

			// Include Properties if provided
			includeProperties ??= Array.Empty<string>();
			foreach (var property in includeProperties)
				query = query.Include(property);

			// Apply Ordering
			if (orderBy != null)
			{
				query = orderByDirection == OrderBy.Ascending
					? query.OrderBy(orderBy)
					: query.OrderByDescending(orderBy);
			}

			// Apply Pagination
			if(skip.HasValue)
				query = query.Skip(skip.Value);
			if(take.HasValue)
				query = query.Take(take.Value);

			// Just For Avoid warning [ explicitly cast ]
			return (IEnumerable<T>) await query.ToListAsync();
		}
	}
}
