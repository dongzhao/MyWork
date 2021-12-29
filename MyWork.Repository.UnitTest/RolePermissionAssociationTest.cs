using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Autofac;
using MyWork.Model;
using System.Globalization;
using System.Linq;

namespace MyWork.Repository.UnitTest
{
    [TestClass]
    public class RolePermissionAssociationTest : BaseRepositoryTest
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
        public void TestClearRolePermissionAssociation()
        {
            var roles = roleRepository.GetAll();
            foreach(var role in roles)
            {
                role.Permissions.Clear();
                roleRepository.Update(role);
            }

            var found = roleRepository.GetAll().Where(c => c.Permissions.Count > 0).FirstOrDefault();
            Assert.IsNull(found);

        }

        [TestMethod]
        public void TestCreateRoleAndPermissionAssociation()
        {
            TestClearRolePermissionAssociation();
            var role = roleRepository.SearchByShortName("Role1").FirstOrDefault();
            if(role == null )
            {
                role = new Role()
                {
                    ShortName = "Role1",
                    Description = "Test Role1",
                };

                roleRepository.Create(role);
            }

            var permission1 = permissionRepository.SearchByShortName("Test-Index").FirstOrDefault();
            if(permission1 == null)
            {
                permission1 = new Permission()
                {
                    ShortName = "Test-Index",
                    Description = "Test-Index",
                };
                permissionRepository.Create(permission1);
            }

            // add a new association
            role.Permissions.Add(permission1);
            roleRepository.Update(role);

            var found = roleRepository.GetAll().Where(c => c.Permissions.Any(r => r.ShortName == "Test-Index")).FirstOrDefault();
            Assert.IsNotNull(found);

        }

        [TestMethod]
        public void TestDeleteUserRoleAssociation()
        {
            TestCreateRoleAndPermissionAssociation();

            var role1 = roleRepository.SearchByShortName("Role1").FirstOrDefault();
            Assert.IsNotNull(role1);

            var permission1 = permissionRepository.SearchByShortName("Test-Index").FirstOrDefault();
            Assert.IsNotNull(permission1);

            role1.Permissions.Remove(permission1);
            roleRepository.Update(role1);

            var found = roleRepository.GetAll().Where(c => c.Permissions.Any(r => r.ShortName == "Test-Index")).FirstOrDefault();
            Assert.IsNull(found);
        }
    }
}
