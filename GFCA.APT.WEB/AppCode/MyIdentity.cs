using GFCA.APT.Domain.Dto;
using System.Security.Principal;

namespace GFCA.APT.WEB
{
    public class MyIdentity : IIdentity
    {
        public IIdentity Identity { get; set; }
        public UserInfoDto User { get; set; }
        public MyIdentity(UserInfoDto user)
        {
            Identity = new GenericIdentity(user.UserName, "Forms");
            User = user;
        }
        public string Name => Identity.Name;
        public string AuthenticationType => Identity.AuthenticationType;
        public bool IsAuthenticated => Identity.IsAuthenticated;
    }

}