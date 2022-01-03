using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyWork.CommonService.UnitTest
{
    [TestClass]
    public abstract class BaseServiceTest
    {
        protected Autofac.IContainer container;

        [TestInitialize]
        public void Setup()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<RestClientConfig>().As<IRestClientConfig>();
            builder.RegisterType<EmailConfig>().As<IEmailConfig>();
            builder.RegisterType<RestServiceClient>().As<IRestServiceClient>();
            builder.RegisterType<EmailServiceClient>().As<IEmailServiceClient>();

            container = builder.Build();
        }

    }
}
