using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDto> GetAll();
        CustomerDto GetByCode(string code);
        BusinessResponse Create(CustomerDto model);
        BusinessResponse Edit(CustomerDto model);
        BusinessResponse Remove(CustomerDto model);
    }
}
