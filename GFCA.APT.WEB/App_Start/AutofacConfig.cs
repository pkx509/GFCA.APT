using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using GFCA.APT.DAL;
using GFCA.APT.DAL.Implements;
using GFCA.APT.DAL.Interfaces;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace GFCA.APT.WEB
{
    public class AutofacConfig
    {

        public static void Register()
        {
            var builder = new Autofac.ContainerBuilder();

            // var config = GlobalConfiguration.Configuration;

            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();

            builder.RegisterModule(new DataAccessLayer());
            builder.RegisterModule(new BusinessLayer());

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private class DataAccessLayer : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                //builder.RegisterType(typeof(APTDbContext)).As(typeof(DbContext)).InstancePerLifetimeScope();
                //builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();
                builder.Register(c => new UnitOfWork("APTDbConnectionString")).As<IUnitOfWork>().InstancePerLifetimeScope();
            }
        }

        private class BusinessLayer : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterAssemblyTypes(Assembly.Load("GFCA.APT.BAL"))
                    .Where(t => t.Name.EndsWith("Service"))
                    .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            }
        }

    }
}