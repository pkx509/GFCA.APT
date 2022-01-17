using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GFCA.APT.WEB.CustomAttributes
{
    using Extensions;
    public class AuthorizerAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            var p = httpContext.User.Identity as MyIdentity;
            string username = httpContext.User.Identity.Name;
            var appRoles = this.Roles.Split(',');
            var userRoles = p.User.Roles;

            bool isPermit = false;
            if (appRoles.Length == 1 && appRoles[0] == string.Empty)
                return true;

            var query = appRoles.Concat(userRoles)
                        .GroupBy(x => x)
                        .Where(g => g.Count() > 1)
                        .Select(x => new { roleItem = x.Key, duplicateCount = x.Count() })
                        .FirstOrDefault();

            isPermit = (query.duplicateCount > 1);

            return isPermit;

        }

    }
}