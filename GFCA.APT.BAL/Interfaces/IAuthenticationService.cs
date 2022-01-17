using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;

namespace GFCA.APT.BAL.Interfaces
{
    public interface IAuthenticationService
    {

        AuthenticationResponse ValidateUser(string userName, string password);

    }
}
