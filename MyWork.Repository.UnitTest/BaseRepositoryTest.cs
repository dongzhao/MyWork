using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyWork.Model;
using System;
using System.ComponentModel;
using System.Data.Entity;

namespace MyWork.Repository.UnitTest
{
    [TestClass]
    public abstract class BaseRepositoryTest
    {
        protected Autofac.IContainer container;

        [TestInitialize]
        public void Setup()
        {
            var builder = new ContainerBuilder();

            //builder.RegisterType<DbContext>().InstancePerRequest();
            builder.RegisterType<MyWorkDbContext>().SingleInstance();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>();
            builder.RegisterType<PermissionRepository>().As<IPermissionRepository>();
            builder.RegisterType<UserProfileRepository>().As<IUserProfileRepository>();
            builder.RegisterType<UserProfileReportUsp>().As<IUserProfileReportUsp>();

            builder.RegisterType<UserProfileQuery>().As<IUserProfileQuery>();

            builder.RegisterGeneric(typeof(GenericRepository<,>)).As(typeof(IRepository<,>));

            container = builder.Build();
        }


    }
}
