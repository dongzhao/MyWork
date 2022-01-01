using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;
using Newtonsoft.Json;

namespace MyWork.Model.UnitTest
{
    [TestClass]
    public class UserPersistentTest
    {
        [TestMethod]
        public void TestPersistUser()
        {
            //JsonConverter.
            using (var ctx = new MyWorkDbContext())
            {
                var expected = new User()
                {
                    Id = 1,
                    UserName = "Admin",
                    Password = "Admin123",
                    EmailAddress = "abc@test.com",
                };
                ctx.UserSet.Add(expected);
                ctx.SaveChanges();

                var actural = ctx.UserSet.SingleOrDefault(e => e.Id == expected.Id);
                Assert.IsNotNull(actural);
            }
        }
    }
}
