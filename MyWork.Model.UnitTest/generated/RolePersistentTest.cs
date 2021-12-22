
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
    public class RolePersistTest
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
        public void TestPersistrole()
        {
            var expected = CreateNew();
            using (var ctx = new MyWorkDbContext())
            {
                ctx.RoleSet.Add(expected);
                ctx.SaveChanges();

                var actural = ctx.RoleSet.SingleOrDefault(e => e.Id == expected.Id);
                Assert.IsNotNull(actural);

                Assert.AreEqual(expected.ShortName, actural.ShortName);

                Assert.AreEqual(expected.Description, actural.Description);

            }
        }

        private Role CreateNew()
        {
            var jsonStr = File.ReadAllText(@"json\" + "Role.json");
            return JsonConvert.DeserializeObject<Role>(jsonStr);
        }
    }
}

