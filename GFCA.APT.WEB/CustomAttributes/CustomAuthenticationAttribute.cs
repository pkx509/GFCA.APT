using Microsoft.Owin.Logging;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace GFCA.APT.WEB.CustomAttributes
{
    public class CustomAuthenticationAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        //public ILogger Logger { get; set; }
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            //Logic for authenticating a user
        }
        //Runs after the OnAuthentication method
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            //TODO: Additional tasks on the request
        }
    }
}