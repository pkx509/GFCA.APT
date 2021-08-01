using System;
using System.Collections.Generic;

namespace GFCA.APT.DAL
{
    public interface IRepository<TModel> : IDisposable
    {
        IEnumerable<TModel> GetAll();
        TModel GetById(int primaryKey);
        void Insert(TModel data);
        void Update(TModel data);
        void Delete(int primaryKey);
        void Save();
    }
}
