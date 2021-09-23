using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Interfaces
{
    public interface IPackService
    {
        IEnumerable<PackDto> GetAll();
        PackDto GetById(int Id);
        BusinessResponse Create(PackDto model);
        BusinessResponse Edit(PackDto model);
        BusinessResponse Remove(PackDto model);
    }
}
