using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GFCA.APT.DAL
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int primaryKey);
        void Insert(T entity);
        void Update(T entity);
		void Update(IEnumerable<T> entities);
		void Delete(T entity);
		void Delete(IEnumerable<T> entities);

		IQueryable<T> Table { get; }
		IQueryable<T> TableNoTracking { get; }
		IQueryable<T> Where(Expression<Func<T, bool>> expression);
	}
}
