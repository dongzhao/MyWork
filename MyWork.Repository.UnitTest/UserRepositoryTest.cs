using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Autofac;
using MyWork.Model;
using System.Globalization;
using System.Linq;

namespace MyWork.Repository.UnitTest
{
    [TestClass]
    public class UserRepositoryTest : BaseRepositoryTest
    {
        private IUserRepository repository;

        [TestInitialize]
        public void Init()
        {
            this.repository = container.Resolve<IUserRepository>();

        }

        [TestMethod]
        public void TestCreate()
        {
            var obj = new User()
            {
                UserName = "user1",
                Password = "user123",
                EmailAddress = "abc@test.com",
                UserProfile = new UserProfile()
                {
                    FirstName = "Tony",
                    LastName = "Zhao",
                    Gender = true,
                    BirthDate = DateTime.ParseExact("1980-12-01", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Address = "2 Test AVE",
                    Mobile = "0401999999",
                },
            };
            var id = repository.Create(obj);

            Assert.IsTrue(id > 0);

        }

        [TestMethod]
        public void TestUpdate()
        {
            var obj = repository.GetAll().FirstOrDefault();
            Assert.IsNotNull(obj);
            var id = obj.Id;
            var oldObj = new User()
            {
                UserName = obj.UserName,
                Password = obj.Password,
                EmailAddress = obj.EmailAddress,
            };
            obj.UserName = obj.UserName + "_upd";
            obj.Password = obj.Password + "_upd";
            obj.EmailAddress = obj.EmailAddress + "_upd";
            repository.Update(obj);

            obj = repository.GetById(obj.Id);
            Assert.IsNotNull(obj);
            Assert.AreNotEqual(obj.UserName, oldObj.UserName);
            Assert.AreNotEqual(obj.Password, oldObj.Password);
            Assert.AreNotEqual(obj.EmailAddress, oldObj.EmailAddress);
        }

        [TestMethod]
        public void TestDelete()
        {
            var obj = repository.GetAll().FirstOrDefault();
            Assert.IsNotNull(obj);
            var id = obj.Id;
            repository.Delete(obj.Id);
            obj = repository.GetById(id);
            Assert.IsNull(obj);
        }
    }
}
