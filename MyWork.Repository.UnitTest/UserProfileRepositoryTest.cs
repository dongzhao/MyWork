using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Autofac;
using MyWork.Model;
using System.Globalization;
using System.Linq;

namespace MyWork.Repository.UnitTest
{
    [TestClass]
    public class UserProfileRepositoryTest : BaseRepositoryTest
    {
        private IUserProfileRepository repository;

        [TestInitialize]
        public void Init()
        {
            this.repository = container.Resolve<IUserProfileRepository>();

        }

        [TestMethod]
        public void TestCreate()
        {
            var obj = new UserProfile()
            {
                FirstName = "Tony",
                LastName = "Zhao",
                Gender = true,
                BirthDate = DateTime.ParseExact("1980-12-01","yyyy-MM-dd", CultureInfo.InvariantCulture),
                Address = "2 Test AVE",
                Mobile = "0401999999",
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
            var oldObj = new UserProfile()
            {
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                Gender = obj.Gender,
                BirthDate = obj.BirthDate,
                Address = obj.Address,
                Mobile = obj.Mobile,
            };
            obj.FirstName = obj.FirstName + "_upd";
            obj.LastName = obj.LastName + "_upd";
            obj.Gender = obj.Gender ? false : true ;
            obj.Mobile = obj.Mobile + "9";
            obj.Address = obj.Address + "_upd";
            obj.BirthDate = obj.BirthDate.AddDays(1);
            repository.Update(obj);

            obj = repository.GetById(obj.Id);
            Assert.IsNotNull(obj);
            Assert.AreNotEqual(obj.FirstName, oldObj.FirstName);
            Assert.AreNotEqual(obj.LastName, oldObj.LastName);
            Assert.AreNotEqual(obj.Gender, oldObj.Gender);
            Assert.AreNotEqual(obj.Mobile, oldObj.Mobile);
            Assert.AreNotEqual(obj.BirthDate, oldObj.BirthDate);
            Assert.AreNotEqual(obj.Address, oldObj.Address);
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
