using GFCA.APT.Domain.Dto;

namespace GFCA.APT.BAL.Interfaces
{
    public interface IAuthenticationService
    {
        void SignIn(UserLoginDto user, bool createPersistentCookie);
        void SignOut();

        UserInfoDto GetAuthenticatedUser();

    }
}
