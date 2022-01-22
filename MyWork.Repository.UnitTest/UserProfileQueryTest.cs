using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Autofac;
using MyWork.Model;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;

namespace MyWork.Repository.UnitTest
{
    [TestClass]
    public class UserProfileQueryTest : BaseRepositoryTest
    {
        private IUserProfileQuery query;

        [TestInitialize]
        public void Init()
        {
            this.query = container.Resolve<IUserProfileQuery>();
        }

        //[TestMethod]
        //public void TestQuery()
        //{
        //    var filter = query.Filter();
        //    filter = query.WithFirstName(filter, "Tony");
        //    var results = query.Execute(filter);
        //    Assert.IsNotNull(results);
        //    Assert.IsTrue(results.Count() > 0);
        //}

        [TestMethod]
        public void TestExecuteWithItems()
        {
            var items = new List<QueryItem>()
            {
                new QueryItem()
                {
                    ItemName = UserProfileQueryField.LastName,
                    ItemValue = "Zhao"
                },
                new QueryItem()
                {
                    ItemName = UserProfileQueryField.Gender,
                    ItemValue = "true"
                },
                new QueryItem()
                {
                    ItemName = UserProfileQueryField.BirthDate,
                    ItemValue = "2010-12-01"
                },
            };
            var results = query.Execute(items);
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count() > 0);
        }
    }
}
