using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using MyWork.Model;
using MyWork.Repository;
using MyWork.Web.Authorize;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MyWork.Web.App_Start
{
    public class Injector
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            // register API controller via autofac WebAPI
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            // register controller via autofac MVC
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            //builder.RegisterType<DbContext>().InstancePerRequest();
            builder.RegisterType<MyWorkDbContext>().InstancePerRequest();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>();
            builder.RegisterType<PermissionRepository>().As<IPermissionRepository>();
            builder.RegisterType<UserProfileRepository>().As<IUserProfileRepository>();
            builder.RegisterType<HierarchyRepository>().As<IHierarchyRepository>();
           
            builder.RegisterType<UserProfileReportUsp>().As<IUserProfileReportUsp>();

            builder.RegisterGeneric(typeof(GenericRepository<,>)).As(typeof(IRepository<,>));

            builder.RegisterType<AuthorizeProvider>().As<IAuthorizeProvider>().InstancePerRequest();

            
            builder.RegisterType<HierarchyEditor>().As<IHierarchyEditor>();

            builder.RegisterModule<AutofacWebTypesModule>();

            IContainer container = builder.Build();
            // set MVC controllers
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            // set WebApi controller
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

    }
}