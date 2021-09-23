using Autofac;
using Autofac.Integration.Mvc;
using GFCA.APT.DAL.Implements;
using GFCA.APT.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using GFCA.APT.BAL.Implements;
using GFCA.APT.BAL.Interfaces;

namespace GFCA.APT.WEB
{
    public class AutofacConfig
    {
        public IConfiguration Configuration { get; set; }
        public static void Register()
        {
            var builder = new Autofac.ContainerBuilder();

            // Register your MVC controllers. (MvcApplication is the name of the class in Global.asax.)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);//.PropertiesAutowired();

            // OPTIONAL: Register model binders that require DI.
            //builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterFilterProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // OPTIONAL: Enable action method parameter injection (RARE).
            //builder.InjectActionInvoker();

            ////Register AutoMapper here using AutoFacModule class (Both methods works)
            ////builder.RegisterModule(new AutoMapperModule());
            //builder.RegisterModule<BusinessLayer>();
            //builder.RegisterModule<DataAccessLayer>();


            builder.RegisterType<BusinessProvider>().As<IBusinessProvider>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            /*
            builder.RegisterAssemblyTypes(Assembly.Load("GFCA.APT.BAL"))
                .Where(t => t.Namespace.Contains("Interfaces") && t.Name.EndsWith("Service"))
                //.As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == $"I{t.Name}"))
                .AsImplementedInterfaces()
                .InstancePerRequest();
            builder.RegisterAssemblyTypes(Assembly.Load("GFCA.APT.DAL"))
                .Where(t => t.Namespace.Contains("Interfaces") && t.Name.EndsWith("Repository"))
                //.As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == $"I{t.Name}"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            */

            #region Set the MVC dependency resolver to use Autofac
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            #endregion
        }
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.Load("GFCA.APT.BAL"))
                .Where(t => t.Namespace.Contains("Implements"))
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == $"I{t.Name}"))
                .InstancePerRequest();

            builder.RegisterAssemblyTypes(Assembly.Load("GFCA.APT.DAL"))
                .Where(t => t.Namespace.Contains("Implements"))
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == $"I{t.Name}"))
                .InstancePerLifetimeScope();

            return builder.Build();

        }
        private class DataAccessLayer : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                //builder.RegisterType(typeof(APTDbContext)).As(typeof(DbContext)).InstancePerLifetimeScope();
                //builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();
                //builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerLifetimeScope();
                //builder.Register(c => new UnitOfWork("APTDbConnectionString")).As<IUnitOfWork>().InstancePerRequest();

                builder.RegisterAssemblyTypes(Assembly.Load("GFCA.APT.DAL"))
                    .Where(t => t.Name.EndsWith("Repository"))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
            }
        }

        private class BusinessLayer : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterAssemblyTypes(Assembly.Load("GFCA.APT.BAL"))
                    .Where(t => t.Name.EndsWith("Service"))
                    .AsImplementedInterfaces()
                    .InstancePerRequest();
            }
        }

        /*
        private class AutoMapModule : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.Register(context => new MapperConfiguration(cfg =>
                {
                    //Register Mapper Profile
                    cfg.AddProfile<AutoMapperProfile>();
                }
            )).AsSelf().SingleInstance();

                builder.Register(c =>
                {
                    //This resolves a new context that can be used later.
                    var context = c.Resolve<IComponentContext>();
                    var config = context.Resolve<MapperConfiguration>();
                    return config.CreateMapper(context.Resolve);
                })
                .As<IMapper>()
                .InstancePerLifetimeScope();
            }
        }
        */

    }
}