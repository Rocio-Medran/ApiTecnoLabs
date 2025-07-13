using Data.Data;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
	public class Repository<T>: IRepository<T> where T : class
	{
		private readonly AppDbContext _context;
		private readonly DbSet<T> _dbSet;

		public Repository(AppDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}

		public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
		{
			IQueryable<T> query = _dbSet.AsNoTracking();

			foreach (var include in includes)
			{
				query = query.Include(include);
			}

			return await query.ToListAsync();
		}

		public async Task<T?> GetByIdAsync(int id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task<T?> GetByIdWithIncludesAsync(
			int id,
			params Expression<Func<T, object>>[] includes)
		{
			IQueryable<T> query = _dbSet;

			foreach (var include in includes)
				query = query.Include(include);

			return await query.FirstOrDefaultAsync(e =>
				EF.Property<int>(e, "Id") == id);
		}


		public async Task AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public void Update(T entity)
		{
			_dbSet.Update(entity);
		}

		public void Delete(T entity)
		{
			_dbSet.Remove(entity);
		}

		public async Task<IEnumerable<T>> FindAsync(
			Expression<Func<T, bool>>? filter = null,
			Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
			params Expression<Func<T, object>>[] includes)
		{
					IQueryable<T> query = _dbSet;

					if (filter != null)
						query = query.Where(filter);

					foreach (var include in includes)
						query = query.Include(include);

					if (orderBy != null)
						query = orderBy(query);

					return await query.AsNoTracking().ToListAsync();
		}

		public async Task<T?> FirstOrDefaultAsync(
			Expression<Func<T, bool>> filter,
			params Expression<Func<T, object>>[] includes)
		{
			IQueryable<T> query = _dbSet;

			foreach (var include in includes)
				query = query.Include(include);

			return await query.FirstOrDefaultAsync(filter);
		}

	}
}
