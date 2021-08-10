using System.Collections.Generic;

namespace GFCA.APT.DAL.Interfaces
{
    public interface IRepository<T>
	{
		IEnumerable<T> All();
		T GetById(int id);
		void Add(T entity);
		void Update(T entity);
		void Delete(int id);
		
	}
}
