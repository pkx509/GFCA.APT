using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Interfaces
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDto> GetAll();
        CompanyDto GetByCode(string code);
        BusinessResponse Create(CompanyDto model);
        BusinessResponse Edit(CompanyDto model);
        BusinessResponse Remove(CompanyDto model);
    }
}
