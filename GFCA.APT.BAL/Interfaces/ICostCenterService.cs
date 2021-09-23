using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Interfaces
{
    public interface ICostCenterService
    {
        IEnumerable<CostCenterDto> GetAll();
        CostCenterDto GetByCode(string code);
        BusinessResponse Create(CostCenterDto model);
        BusinessResponse Edit(CostCenterDto model);
        BusinessResponse Remove(CostCenterDto model);
    }
}
