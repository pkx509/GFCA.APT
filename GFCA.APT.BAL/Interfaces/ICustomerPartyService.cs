using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;

namespace GFCA.APT.BAL.Interfaces
{
    public interface ICustomerPartyService
    {
        IEnumerable<CustomerPartyDto> GetAll();
        CustomerPartyDto GetById(int Id);
        BusinessResponse Create(CustomerPartyDto model);
        BusinessResponse Edit(CustomerPartyDto model);
        BusinessResponse Remove(CustomerPartyDto model);
    }
}
