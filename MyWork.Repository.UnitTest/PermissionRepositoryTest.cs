using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Autofac;
using MyWork.Model;
using System.Globalization;
using System.Linq;

namespace MyWork.Repository.UnitTest
{
    [TestClass]
    public class PermissionRepositoryTest : BaseRepositoryTest
    {
        private IPermissionRepository repository;

        [TestInitialize]
        public void Init()
        {
            this.repository = container.Resolve<IPermissionRepository>();

        }

        [TestMethod]
        public void TestCreate()
        {
            var obj = new Permission()
            {
                ShortName = "Home-Index",
                Description = "test",
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
            var oldObj = new Permission()
            {
                ShortName = obj.ShortName,
                Description = obj.Description,
            };
            obj.ShortName = obj.ShortName + "_upd";
            obj.Description = obj.Description + "_upd";
            repository.Update(obj);

            obj = repository.GetById(obj.Id);
            Assert.IsNotNull(obj);
            Assert.AreNotEqual(obj.ShortName, oldObj.ShortName);
            Assert.AreNotEqual(obj.Description, oldObj.Description);
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
