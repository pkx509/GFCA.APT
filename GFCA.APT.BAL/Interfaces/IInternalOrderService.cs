using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Interfaces
{
    public interface IInternalOrderService
    {
        IEnumerable<InternalOrderDto> GetAll();
        InternalOrderDto GetByCode(string code);
        BusinessResponse Create(InternalOrderDto model);
        BusinessResponse Edit(InternalOrderDto model);
        BusinessResponse Remove(InternalOrderDto model);
    }
}
