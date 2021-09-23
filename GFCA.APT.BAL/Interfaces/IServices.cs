using GFCA.APT.Domain.Models;
using System.Collections.Generic;

namespace GFCA.APT.BAL.Interfaces
{
    public interface IServices<T>
    {
        IEnumerable<T> GetAll();
        T GetByCode(string code);
        BusinessResponse Create(T model);
        BusinessResponse Edit(T model);
        BusinessResponse Remove(T model);
    }
}
