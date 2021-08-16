using log4net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace GFCA.APT.WEB
{
    public abstract class ControllerWebBase : Controller
    {
        private readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var request = HttpContext.Request;
            var baseUrl = string.Format("{0}://{1}{2}"
                , request.Url.Scheme
                , request.Url.Authority
                , HttpRuntime.AppDomainAppVirtualPath == "/" ? string.Empty : HttpRuntime.AppDomainAppVirtualPath);
            ViewBag.BasePath = baseUrl;

            string actionName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string userName = Session["User"] as string;
            userName = !string.IsNullOrWhiteSpace(userName) ? userName : "anonymous";

            string text = $"User : {userName},  HttpMethod : {Request.HttpMethod}, Controller : {controllerName}, Action : {actionName} ";
            logger.Info(text);
            base.OnActionExecuting(filterContext);
        }
    }
}