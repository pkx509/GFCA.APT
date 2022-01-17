using System.Security.Principal;
using System.Web.Security;

namespace GFCA.APT.WEB
{
    public class MyPrincipal : IPrincipal
    {

        private readonly MyIdentity _myIdentity;
        private readonly IPrincipal _myprincipal;

        public MyPrincipal(MyIdentity myIdentity) : this(myIdentity, new string[] { "Root" })
        {
            
        }
        public MyPrincipal(MyIdentity myIdentity, string[] roles)
        {
            _myIdentity = myIdentity;
            _myprincipal = new GenericPrincipal(myIdentity, roles);
        }
        
        public IIdentity Identity => _myIdentity;

        public bool IsInRole(string role)
        {
            return Roles.IsUserInRole(role);
        }
    }
}