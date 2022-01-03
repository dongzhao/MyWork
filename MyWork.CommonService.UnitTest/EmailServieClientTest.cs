using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MyWork.CommonService.UnitTest
{
    [TestClass]
    public class EmailServieClientTest : BaseServiceTest
    {
        private IEmailConfig cfg;
        private IEmailServiceClient service;

        [TestInitialize]
        public void Init()
        {
            this.cfg = container.Resolve<IEmailConfig>();
            this.service = container.Resolve<IEmailServiceClient>();
        }

        [TestMethod]
        public void TestSendEmailWithoutAttachment()
        {
            var dto = new MailDto()
            {
                Subject = "Hello",
                Body = "Hello",        
            };
            dto.ToList.Add("test@abc.com");
            service.Send(dto);
        }
    }
}
