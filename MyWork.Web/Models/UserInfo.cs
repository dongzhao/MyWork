using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWork.Web.Models
{
    public class UserInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserProfileDto UserProfile { get; set; }
        public List<RoleDto> Roles { get; set; }
    }

    public class UserProfileDto
    {
        public int ProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class RoleDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public List<PermissionDto> Permissions { get; set; }
    }

    public class PermissionDto
    {   
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
    }
}