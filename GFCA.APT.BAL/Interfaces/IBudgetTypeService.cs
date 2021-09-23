using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Interfaces
{
    public interface IBudgetTypeService
    {
        IEnumerable<BudgetTypeDto> GetAll();
        BudgetTypeDto GetByCode(string code);
        BusinessResponse Create(BudgetTypeDto model);
        BusinessResponse Edit(BudgetTypeDto model);
        BusinessResponse Remove(BudgetTypeDto model);
    }
}
