//using Microsoft.Owin.Logging;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace GFCA.APT.WEB.CustomAttributes
{
    public class CustomAuthenticationAttribute : AuthorizationFilterAttribute, System.Web.Mvc.Filters.IAuthenticationFilter
    //public class CustomAuthenticationAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        /*
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                string tk = actionContext.Request.Headers.Authorization.Parameter;
                string decodedTk = Encoding.UTF8.GetString(Convert.FromBase64String(tk));
                string[] usernamePasswordArray = decodedTk.Split(':');
                string username = usernamePasswordArray[0];
                string password = usernamePasswordArray[1];

                string[] roles = null;
                bool isAuten = true;
                if (isAuten)
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), roles);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
        */
        //public ILogger Logger { get; set; }
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            
            var a = filterContext.HttpContext.Profile;
            var attribute = ((ReflectedActionDescriptor)filterContext.ActionDescriptor).MethodInfo.GetCustomAttributes(true).OfType<AuthorizationFilterAttribute>().FirstOrDefault();
            /*
            //string username = Convert.ToString(System.Web.HttpContext.Current.Session["Username"]);
            //string role = Convert.ToString(System.Web.HttpContext.Current.Session["Role"]);
            var p = filterContext.HttpContext.User as MyPrincipal;
            //var id = System.Web.HttpContext.Current.User.Identity as MyIdentity;
            
            //string username = System.Web.HttpContext.Current.User.Identity.Name;
            //var p = System.Threading.Thread.CurrentPrincipal as MyPrincipal;
            var id = filterContext.HttpContext.User.Identity as MyIdentity;
            if (p == null)
                return;

            
            string username = id.Name;
            var userData = id.User;
            string[] roles = userData.Roles;
            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string tag = controllerName + actionName;

            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }
            
            //if (System.Web.HttpContext.Current.Session["Username"] == null)
            if (username == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            //if (!string.IsNullOrEmpty(username))
            if (username != null && username != string.Empty)
            {
                bool isPermitted = false;
                //var viewPermission = dbRolePermission.Where(x => x.Role == role && x.Tag == tag).SingleOrDefault();
                var viewPermission = default(int);

                if (viewPermission != null)
                {
                    isPermitted = true;
                }
                if (isPermitted == false)
                {
                    filterContext.Result = new RedirectToRouteResult
                        (
                            new System.Web.Routing.RouteValueDictionary()
                            {
                                {"controller", "Home"},
                                {"action", "Contact" }
                            }
                        );
                }

            }
            */
        }
        //Runs after the OnAuthentication method
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            //TODO: Additional tasks on the request
        }
    }
}