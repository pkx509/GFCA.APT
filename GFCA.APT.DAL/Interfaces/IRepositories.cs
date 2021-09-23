using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.DAL.Interfaces
{
    public interface IRepositories<T>
    {
        IEnumerable<T> All();
        T GetByCode(string code);
        void Insert(T entity);
        void Update(T entity);
        void Delete(string code);
    }
}
