
/**
Generated class by codegen 
*/
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyWork.Model;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using SYstem.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace MyWork.Model.PersistentTest
{
    [TestClass]
    public class UserPersistTest
    {
        [TestInitialize]
        public void Setup()
        {
        }

        [TestCleanup]
        public void TearDown()
        {
        }

        [TestMethod]
        public void TestPersistuser()
        {
            var expected = CreateNew();
            using (var ctx = new MyWorkDbContext())
            {
                ctx.UserSet.Add(expected);
                ctx.SaveChanges();

                var actural = ctx.UserSet.SingleOrDefault(e => e.Id == expected.Id);
                Assert.IsNotNull(actural);

                Assert.AreEqual(expected.UserName, actural.UserName);

                Assert.AreEqual(expected.Password, actural.Password);

                Assert.AreEqual(expected.EmailAddress, actural.EmailAddress);

            }
        }

        private User CreateNew()
        {
            var jsonStr = File.ReadAllText(@"json\" + "User.json");
            return JsonConvert.DeserializeObject<User>(jsonStr);
        }
    }
}

