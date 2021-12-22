

































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
    public class PermissionPersistTest
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
        public void TestPersistpermission()
        {
            var expected = CreateNew();
            using (var ctx = new MyWorkDbContext())
            {
                ctx.PermissionSet.Add(expected);
                ctx.SaveChanges();

                var actural = ctx.PermissionSet.SingleOrDefault(e => e.Id == expected.Id);
                Assert.IsNotNull(actural);

                Assert.AreEqual(expected.ShortName, actural.ShortName);

                Assert.AreEqual(expected.Description, actural.Description);

            }
        }

        private Permission CreateNew()
        {
            var jsonStr = File.ReadAllText(@"json\" + "Permission.json");
            return JsonConvert.DeserializeObject<Permission>(jsonStr);
        }
    }
}

