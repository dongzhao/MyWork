using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Autofac;
using MyWork.Model;
using System.Globalization;
using System.Linq;

namespace MyWork.Repository.UnitTest
{
    [TestClass]
    public class UserProfileReportRepositoryTest : BaseRepositoryTest
    {
        private IUserProfileReportUsp repository;

        [TestInitialize]
        public void Init()
        {
            this.repository = container.Resolve<IUserProfileReportUsp>();
        }

        [TestMethod]
        public void TestSearch()
        {
            var results = repository.Search("1900-01-01", "9999-12-31").ToList();
            Assert.IsNotNull(results);
            Assert.IsNotNull(results.Count > 0);
        }
    }
}
