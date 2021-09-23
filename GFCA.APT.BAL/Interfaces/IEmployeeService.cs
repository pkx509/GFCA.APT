using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAll();
        EmployeeDto GetByCode(string code);
        BusinessResponse Create(EmployeeDto model);
        BusinessResponse Edit(EmployeeDto model);
        BusinessResponse Remove(EmployeeDto model);
    }
}
