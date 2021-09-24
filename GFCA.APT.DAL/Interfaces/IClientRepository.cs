using GFCA.APT.Domain.Dto;

namespace GFCA.APT.DAL.Interfaces
{
    public interface IClientRepository : IRepositories<ClientDto>
    {
        ClientDto GetByCode(string Id);
        void DeleteByCode(string id);
    }
}
