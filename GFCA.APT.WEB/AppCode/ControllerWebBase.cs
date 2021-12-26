using GFCA.APT.Domain.Dto;
using log4net;
using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace GFCA.APT.WEB
{
    public abstract class ControllerWebBase : Controller
    {
        private readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /*
        protected new JsonResult Json(object data, JsonRequestBehavior behavior)
        {
            return Json(data, "application/json", System.Text.Encoding.UTF8, behavior);
        }
        protected new JsonResult Json(object data)
        {
            return Json(data, "application/json", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return Json(data, contentType, contentEncoding, behavior);
        }
        */
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
            //logger.Info(text);

            var assambly = System.Reflection.Assembly.GetExecutingAssembly();
            var versionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(assambly.Location);

            string env = Environment.GetEnvironmentVariable("ENV_APT");
            if (env == null)
                env = "DEV";

            ViewBag.InfomationSystem = new InfomationSystem 
            {
                Environment = env,
                SystemVersion = versionInfo.FileVersion,
                DatabaseVersion = "0.0.2"
            };

            base.OnActionExecuting(filterContext);
        }
        
    }
}