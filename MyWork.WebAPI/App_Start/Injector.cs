using Autofac;
using Autofac.Integration.WebApi;
using MyWork.Model;
using MyWork.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace MyWork.WebAPI.App_Start
{
    public class Injector
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<DbContext>().InstancePerRequest();
            builder.RegisterType<MyWorkDbContext>().InstancePerRequest();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<RoleRepository>().As<IUserRepository>();
            builder.RegisterType<PermissionRepository>().As<IPermissionRepository>();
            builder.RegisterType<UserProfileRepository>().As<IUserProfileRepository>();
            //builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IRepository<>));

            IContainer container = builder.Build();
            // set WebApi controller
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }

    }
}