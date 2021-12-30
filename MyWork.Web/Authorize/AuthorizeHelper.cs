using MyWork.Repository;
using MyWork.Web.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWork.Web.Authorize
{
    public class AuthorizeHelper : IAuthorizeHelper
    {
        private readonly IUserRepository userRepository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public AuthorizeHelper(IUserRepository rs)
        {
            this.userRepository = rs;
        }

        public UserInfo GetUserInfo(string username)
        {
            var result = userRepository.SearchByUserName(username).FirstOrDefault();
            var userInfo = new UserInfo()
            {
                UserId = result.Id,
                UserName = username,
                Email = result.EmailAddress,
                UserProfile = new UserProfileDto()
                {
                    ProfileId = result.UserProfile.Id,
                    FirstName = result.UserProfile.FirstName,
                    LastName = result.UserProfile.LastName,
                },
                Roles = new List<RoleDto>(),
            };

            foreach (var role in result.Roles)
            {
                var roleDto = new RoleDto()
                {
                    RoleId = role.Id,
                    RoleName = role.ShortName,
                    Permissions = new List<PermissionDto>(),
                };

                foreach (var permission in role.Permissions)
                {
                    roleDto.Permissions.Add(new PermissionDto()
                    {
                        PermissionId = permission.Id,
                        PermissionName = permission.ShortName,
                    });

                }

                userInfo.Roles.Add(roleDto);
            }
            return userInfo;
        }

        public bool HasPermission(string permission, UserInfo userInfo)
        {
            return userInfo.Roles.Where(c => c.Permissions.Any(d => d.PermissionName == permission)).Count() > 0;
        }

        public string ToShortUserName(string logonName)
        {
            var list = logonName.Split('\\').ToList();
            if(list.Count > 1)
            {
                return list[1];
            }
            return list[0];
        }
    }
}