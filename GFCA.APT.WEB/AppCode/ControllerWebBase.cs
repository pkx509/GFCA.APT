using GFCA.APT.BAL.Log;
using System.Web;
using System.Web.Mvc;

namespace GFCA.APT.WEB
{
    public abstract class ControllerWebBase : Controller
    {
        protected readonly ILogService _logger;
        protected ControllerWebBase(ILogService log)
        {
            _logger = log;
        }
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
            _logger.Info(text);
            base.OnActionExecuting(filterContext);
        }
    }
}