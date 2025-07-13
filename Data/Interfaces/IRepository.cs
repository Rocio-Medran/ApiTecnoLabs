using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
	public interface IRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
		Task<T?> GetByIdAsync(int id);
		Task AddAsync(T entity);
		void Update(T entity);
		void Delete(T entity);
		Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>>? filter = null, 
			Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
			params Expression<Func<T, object>>[] includes);
		Task<T?> FirstOrDefaultAsync(
			Expression<Func<T, bool>> filter,
			params Expression<Func<T, object>>[] includes);
		Task<T?> GetByIdWithIncludesAsync(
			int id,
			params Expression<Func<T, object>>[] includes);
	}
}
