using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Interfaces
{
    public interface IClientService
    {
        IEnumerable<ClientDto> GetAll();
        ClientDto GetById(int Id);
        BusinessResponse Create(ClientDto model);
        BusinessResponse Edit(ClientDto model);
        BusinessResponse Remove(ClientDto model);
    }
}
