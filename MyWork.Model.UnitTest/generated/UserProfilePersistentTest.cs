
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
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace MyWork.Model.PersistentTest
{
    [TestClass]
    public class UserProfilePersistTest
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
        public void TestPersistuserprofile()
        {
            var expected = CreateNew();
            using (var ctx = new MyWorkDbContext())
            {
                ctx.UserProfileSet.Add(expected);
                ctx.SaveChanges();

                var actural = ctx.UserProfileSet.SingleOrDefault(e => e.Id == expected.Id);
                Assert.IsNotNull(actural);

                Assert.AreEqual(expected.FirstName, actural.FirstName);

                Assert.AreEqual(expected.LastName, actural.LastName);

                Assert.AreEqual(expected.Gender, actural.Gender);

                Assert.AreEqual(expected.BirthDate, actural.BirthDate);

                Assert.AreEqual(expected.Mobile, actural.Mobile);

                Assert.AreEqual(expected.Address, actural.Address);

            }
        }

        private UserProfile CreateNew()
        {
            var jsonStr = File.ReadAllText(@"json\" + "UserProfile.json");
            return JsonConvert.DeserializeObject<UserProfile>(jsonStr);
        }
    }
}

