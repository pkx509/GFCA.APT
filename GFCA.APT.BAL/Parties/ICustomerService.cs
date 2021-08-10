using GFCA.APT.DAL;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;

namespace GFCA.APT.BAL.Parties
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDto> GetAll();
        CustomerDto GetByID(int Id);
        BusinessResponse Create(CustomerDto model);
        BusinessResponse Edit(CustomerDto model);
        BusinessResponse Delete(CustomerDto model);
    }
}
