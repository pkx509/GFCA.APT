using GFCA.APT.BAL.Interfaces;
using GFCA.APT.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace GFCA.APT.BAL.Implements
{
    public class FormsAuthenticationService : IAuthenticationService
    {

        private readonly HttpContext _httpContext = HttpContext.Current;
        //private readonly UserService _userService = new UserService();
        //private readonly RoleService _roleService = new RoleService();

        public UserInfoDto GetAuthenticatedUser()
        {
            throw new NotImplementedException();
        }

        public void SignIn(UserLoginDto user, bool createPersistentCookie)
        {

        }

        public void SignOut()
        {
            
        }
    }
}
