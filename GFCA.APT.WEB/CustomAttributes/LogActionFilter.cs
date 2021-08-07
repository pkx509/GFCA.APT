using System;
using System.Web;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;
using GFCA.APT.BAL.Log;

namespace GFCA.APT.WEB.CustomAttributes
{
    public class LogActionFilter : ActionFilterAttribute
    {
        protected ILogService _logger { get; }
        public LogActionFilter(ILogService logger)
        {
            _logger = logger;
        }

        // 1
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            string actionName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string userName = HttpContext.Current.Session["User"] as string;
            userName = !string.IsNullOrWhiteSpace(userName) ? userName : "anonymous";

            string text = $"User : {userName},  HttpMethod : {HttpContext.Current.Request.HttpMethod}, Controller : {controllerName}, Action : {actionName} ";

            //Log("", filterContext.RouteData);

        }
        /*
        // 2
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }
        */
        // 3
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        // 4
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }

        private void Log(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = String.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);
            //Debug.WriteLine(message, "Action Filter Log");
        }
    }
}