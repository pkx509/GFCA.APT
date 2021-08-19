using System.Collections.Generic;

namespace GFCA.APT.DAL.Interfaces
{
    public interface IRepositories<T>
	{
		IEnumerable<T> All();
		T GetById(int id);
		void Insert(T entity);
		void Update(T entity);
		void Delete(int id);

	}
}
