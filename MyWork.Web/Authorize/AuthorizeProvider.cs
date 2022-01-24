using MyWork.Repository;
using MyWork.Web.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWork.Web.Authorize
{
    public class AuthorizeProvider : IAuthorizeProvider
    {
        private readonly IUserRepository userRepository;
        private readonly HttpContext context;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public AuthorizeProvider(IUserRepository rs)
        {
            this.userRepository = rs;
            this.context = HttpContext.Current;
        }

        private UserInfo _userProfile;
        public UserInfo UserProfile
        {
            get
            {
                if(context.Session["user_profile"]==null)
                {
                    var logonName = context.User.Identity.Name;
                    var shortName = ToShortUserName(logonName);
                    var result = userRepository.SearchByUserName(shortName).FirstOrDefault();
                    var userInfo = new UserInfo()
                    {
                        UserId = result.Id,
                        UserName = shortName,
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
                    context.Session["user_profile"] = userInfo;
                }
                _userProfile = context.Session["user_profile"] as UserInfo;
                return _userProfile;
            }
            set
            {
                context.Session["user_profile"] = value;
                _userProfile = context.Session["user_profile"] as UserInfo;
            }
        }

        public bool HasPermission(string permission)
        {
            return UserProfile.Roles.Where(c => c.Permissions.Any(d => d.PermissionName == permission)).Count() > 0;
        }

        private string ToShortUserName(string logonName)
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