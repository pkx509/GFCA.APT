﻿using Microsoft.Owin;
using Owin;
using Autofac;

[assembly: OwinStartupAttribute(typeof(GFCA.APT.WEB.Startup))]
namespace GFCA.APT.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //app.UseAutofacMiddleware(container);
            //app.UseAutofacMvc();
        }
    }
}
