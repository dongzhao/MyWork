using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MyWork.CommonService.UnitTest
{
    [TestClass]
    public class EmailConfigTest : BaseServiceTest
    {
        private IEmailConfig cfg;

        [TestInitialize]
        public void Init()
        {
            this.cfg = container.Resolve<IEmailConfig>();
        }

        [TestMethod]
        public void TestGetSettings()
        {
            Assert.IsNotNull(cfg);
            Assert.AreEqual(true, cfg.IsActive);
            Assert.AreEqual("smtp.gmail.com", cfg.Host);
            Assert.AreEqual(25, cfg.Port);
            Assert.AreEqual("test", cfg.Username);
            Assert.AreEqual("test123", cfg.Password);
            Assert.AreEqual("2022Test", cfg.Key);

        }
    }
}
