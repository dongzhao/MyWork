using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MyWork.CommonService.UnitTest
{
    [TestClass]
    public class RestClientConfigTest : BaseServiceTest
    {
        private IRestClientConfig cfg;

        [TestInitialize]
        public void Init()
        {
            this.cfg = container.Resolve<IRestClientConfig>();
        }

        [TestMethod]
        public void TestGetSettings()
        {
            Assert.IsNotNull(cfg);
            Assert.AreEqual(@"http://test/api/v1/", cfg.BaseUrl);
            Assert.AreEqual(@"userprofile", cfg.UserProfileUrl);

        }
    }
}
