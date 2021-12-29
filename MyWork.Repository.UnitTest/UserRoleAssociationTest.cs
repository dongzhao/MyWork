using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Autofac;
using MyWork.Model;
using System.Globalization;
using System.Linq;

namespace MyWork.Repository.UnitTest
{
    [TestClass]
    public class UserRoleAssociationTest : BaseRepositoryTest
    {
        private IUserRepository userRepository;
        private IRoleRepository roleRepository;
        private IPermissionRepository permissionRepository;

        [TestInitialize]
        public void Init()
        {
            this.userRepository = container.Resolve<IUserRepository>();
            this.roleRepository = container.Resolve<IRoleRepository>();
            this.permissionRepository = container.Resolve<IPermissionRepository>();
        }

        [TestMethod]
        public void TestClearUserAndRoleAssociation()
        {
            var users = userRepository.GetAll();
            foreach(var user in users)
            {
                user.Roles.Clear();
                userRepository.Update(user);
            }

            var found = userRepository.GetAll().Where(c => c.Roles.Count > 0).FirstOrDefault();
            Assert.IsNull(found);

        }

        [TestMethod]
        public void TestCreateUserAndRoleAssociation()
        {
            TestClearUserAndRoleAssociation();
            var user = userRepository.SearchByUserName("user1").FirstOrDefault();
            if(user == null )
            {
                user = new User()
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

                userRepository.Create(user);
            }

            var role1 = roleRepository.SearchByShortName("Role1").FirstOrDefault();
            if(role1 == null)
            {
                role1 = new Role()
                {
                    ShortName = "Role1",
                    Description = "test Role1",
                };
                roleRepository.Create(role1);
            }

            // add a new association
            user.Roles.Add(role1);
            userRepository.Update(user);

            var found = userRepository.GetAll().Where(c => c.Roles.Any(r => r.ShortName == "Role1")).FirstOrDefault();
            Assert.IsNotNull(found);

        }

        [TestMethod]
        public void TestDeleteUserRoleAssociation()
        {
            TestCreateUserAndRoleAssociation();

            var user = userRepository.SearchByUserName("user1").FirstOrDefault();
            Assert.IsNotNull(user);

            var role1 = roleRepository.SearchByShortName("Role1").FirstOrDefault();
            Assert.IsNotNull(role1);

            user.Roles.Remove(role1);
            userRepository.Update(user);

            var found = userRepository.GetAll().Where(c => c.Roles.Any(r => r.ShortName == "Role1")).FirstOrDefault();
            Assert.IsNull(found);
        }
    }
}
