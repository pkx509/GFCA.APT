using System.Web.Mvc.Filters;

namespace GFCA.APT.WEB.CustomAttributes
{
    public interface IAuthenticationFilter
    {
        void OnAuthentication(AuthenticationContext filterContext);
        void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext);
    }
}
