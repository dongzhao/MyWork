using MyWork.Model;
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

        private UserProfileDto _userProfile;
        public UserProfileDto UserProfile
        {
            get
            {
                if(context.Session["user_profile"]==null)
                {
                    var dto = GetUserProfile();
                    context.Session["user_profile"] = dto;
                }
                _userProfile = context.Session["user_profile"] as UserProfileDto;
                return _userProfile;
            }
            set
            {
                SaveOrUpdateUserProfile(value);
                context.Session["user_profile"] = value;
                _userProfile = context.Session["user_profile"] as UserProfileDto;
            }
        }

        public bool HasPermission(string permission)
        {
            return UserProfile.Roles.Where(c => c.Permissions.Any(d => d.PermissionName == permission)).Count() > 0;
        }

        private void SaveOrUpdateUserProfile(UserProfileDto dto)
        {
            var user = userRepository.GetById(dto.UserId);
            bool hasChanged = false;
            if (user != null)
            {
                if (user.EmailAddress.ToLower() != dto.Email.ToLower())
                {
                    user.EmailAddress = dto.Email;
                    hasChanged = true;
                }
                if(user.UserProfile.FirstName.ToLower() != dto.FirstName.ToLower())
                {
                    user.UserProfile.FirstName = dto.FirstName;
                }
                if (user.UserProfile.LastName.ToLower() != dto.LastName.ToLower())
                {
                    user.UserProfile.LastName = dto.LastName;
                }
                if (hasChanged) userRepository.Update(user);
            }
            else
            {
                user = new User()
                {
                    UserName = dto.UserName,
                    EmailAddress = dto.Email,
                    UserProfile = new UserProfile()
                    {
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                    },
                };
                userRepository.Create(user);
            }
        }

        private UserProfileDto GetUserProfile()
        {
            var logonName = context.User.Identity.Name;
            var shortName = ToShortUserName(logonName);
            var result = userRepository.SearchByUserName(shortName).FirstOrDefault();
            var dto = new UserProfileDto()
            {
                UserId = result.Id,
                UserName = shortName,
                Email = result.EmailAddress,
                FirstName = result.UserProfile.FirstName,
                LastName = result.UserProfile.LastName,
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

                dto.Roles.Add(roleDto);
            }
            return dto;
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