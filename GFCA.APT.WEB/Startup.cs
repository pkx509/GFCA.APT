using Microsoft.Owin;
using Owin;
using Autofac;
using System.Web.Mvc;
/*
[assembly: OwinStartupAttribute(typeof(GFCA.APT.WEB.Startup))]
namespace GFCA.APT.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ModelBinders.Binders.Add(typeof(Domain.Enums.COMMAND_TYPE), new CommandTypeModelBinder());
            ModelBinders.Binders.Add(typeof(Domain.Enums.ROW_TYPE), new RowTypeModelBinder());
            ConfigureAuth(app);
            //app.UseAutofacMiddleware(container);
            //app.UseAutofacMvc();
        }
    }
}
*/
