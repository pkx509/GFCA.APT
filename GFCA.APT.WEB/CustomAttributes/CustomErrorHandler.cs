using log4net;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace GFCA.APT.WEB.CustomAttributes
{
    public class CustomErrorHandler : HandleErrorAttribute, IExceptionFilter, IActionFilter
    {
        private readonly ILog logger = LogManager.GetLogger("GFCA.APT.WEB MONITOR");
        private const string StopwatchKey = "DebugLoggingStopWatch";

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (logger.IsDebugEnabled)
            {
                var loggingWatch = Stopwatch.StartNew();
                filterContext.HttpContext.Items.Add(StopwatchKey, loggingWatch);

                var message = new StringBuilder();

                string actionName = filterContext.ActionDescriptor.ActionName;
                string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                var area = filterContext.RouteData.DataTokens["area"] ?? string.Empty;
                string areaName = area.ToString();

                string msg = $"Executing controller {controllerName}, action {actionName}";
                if (!string.IsNullOrEmpty(areaName))
                    msg = $"Executing area {areaName}, controller {controllerName}, action {actionName}";

                message.Append(msg);

                logger.Debug(message);
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (logger.IsDebugEnabled)
            {
                if (filterContext.HttpContext.Items[StopwatchKey] != null)
                {
                    var loggingWatch = (Stopwatch)filterContext.HttpContext.Items[StopwatchKey];
                    loggingWatch.Stop();

                    long timeSpent = loggingWatch.ElapsedMilliseconds;

                    string actionName = filterContext.ActionDescriptor.ActionName;
                    string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                    var area = filterContext.RouteData.DataTokens["area"] ?? string.Empty;
                    string areaName = area.ToString();

                    var message = new StringBuilder();

                    string msg = $"Finished executing controller {controllerName}, action {actionName} - time spent {timeSpent}";
                    if (!string.IsNullOrEmpty(areaName))
                        msg = $"Finished executing area {areaName}, controller {controllerName}, action {actionName} - time spent {timeSpent}";

                    message.Append(msg);

                    logger.Debug(message);
                    filterContext.HttpContext.Items.Remove(StopwatchKey);
                }
            }
        }

        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                //Logger.Log("OnException");
                return;
            }

            if (new HttpException(null, filterContext.Exception).GetHttpCode() != 500)
                return;


            if (!ExceptionType.IsInstanceOfType(filterContext.Exception))
                return;

            /*
            if (filterContext.ExceptionHandled && filterContext.Exception is NullReferenceException)
            {
                filterContext.Result = new RedirectResult("500.html");
                filterContext.ExceptionHandled = true;
            }

            if (filterContext.Exception is NotImplementedException)
            {
                
            }
            */

            var area = filterContext.RouteData.DataTokens[RouteDataTokenKeys.Area] ?? string.Empty;
            var controller = filterContext.RouteData.Values[RouteDataTokenKeys.Controller] ?? string.Empty;
            var action = filterContext.RouteData.Values[RouteDataTokenKeys.Action] ?? string.Empty;
            //var id = filterContext.RouteData.Values["id"]; parameters
            //var model = filterContext.Controller.ViewData.Model;

            string actionName = action.ToString();
            string controllerName = filterContext.Controller.ToString();
            string areaName = area.ToString();

            // if the request is AJAX return JSON else view.
            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        error = true,
                        message = filterContext.Exception.Message
                    }
                };
            }
            else
            {
                /*
                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
                filterContext.Result = new ViewResult
                {
                    ViewName = View,
                    MasterName = Master,
                    ViewData = new ViewDataDictionary(model),
                    TempData = filterContext.Controller.TempData
                };
                */

            }

            logger.Error("ERROR INFO", filterContext.Exception);

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;

            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;


        }
    }
}