using log4net;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GFCA.APT.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDgxNTYyQDMxMzkyZTMyMmUzMFNmZmkxOXZEMUpuV2tUemNzYVN4eDJNWTNjK0pWQm4yTDhLdHFhYUJNTDg9;NDgxNTYzQDMxMzkyZTMyMmUzMFQvU0tGM0hYbVFvQ00wMUQ2VThFcjFjMHk0TWNVYkZ4djBydGk4QmIySDA9;NDgxNTY0QDMxMzkyZTMyMmUzME1jaHJDZ1RZdzh1blNrREpPcnR0WERNajZZMmxlanpsWmNDN000aGx4bTA9;NDgxNTY1QDMxMzkyZTMyMmUzMEdpWWUvUXVrc0pyNS9BaEp2SEF5bkY0TGd6UHRMZk9yalZUemxWVVFRaU09;NDgxNTY2QDMxMzkyZTMyMmUzMGhVK2dOTkFMaEVxTjVxV2tTRlFXOU5mS3FnWHdUU2JTNXpNLzAvM2E5Vjg9;NDgxNTY3QDMxMzkyZTMyMmUzMEVYRzV2Slp0M0RPVjBvWlJHZk1qd2dMK2V5WlRQVy9XWTdIOFMzMjRBc1E9;NDgxNTY4QDMxMzkyZTMyMmUzMFBic1kxSHZldFlZbU52aFdBdmlEdERYRlhJVVNRVDNRZVArcmhvTUdjRmc9;NDgxNTY5QDMxMzkyZTMyMmUzMGxTWmkwdjZtQnJrWlcwQWJzMDJrMU9KZkVxNUYrK1VVZWsyd1NHOWh3NEk9;NDgxNTcwQDMxMzkyZTMyMmUzMEdDY2JWQWdud1MydVRYb3JLN29vQVcxTW03SnZyRkNHVkhmbzdHdExHN289;NDgxNTcxQDMxMzkyZTMyMmUzMEdhUG0vLzFTbXlTemYzRjlXSHB6bXF5OEZadFMwZFd0d1ZncnBlMDVEYWc9");
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacConfig.Register();
            log4net.Config.XmlConfigurator.Configure();

            //ASP.NET MVC version disclosure
            MvcHandler.DisableMvcResponseHeader = true;
            
        }

        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Set("Server", "My httpd server");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-AspNetMvc-Version");
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var application = sender as HttpApplication;
            if (application != null && application.Context != null)
            {
                application.Context.Response.Headers.Remove("Server");
            }

            CultureInfo culture = new CultureInfo("en-US");
            //culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            //culture.DateTimeFormat.DateSeparator = "/";
            Thread.CurrentThread.CurrentCulture = culture;

            var c = GlobalConfiguration.Configuration;
            c.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //c.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
            
            //c.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;

            //c.Formatters.JsonFormatter.SerializerSettings.ContractResolver = Newtonsoft.Json.Serialization.IContractResolver;

        }

        public static void Register(HttpConfiguration config)
        {
            
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                 name: "DefaultApi",
                 routeTemplate: "api/{controller}/{action}/{id}",
                 defaults: new { id = RouteParameter.Optional }
             );
        }
    }
}
