using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWork.Service.RestClient
{
    public class UserProfileDto : IEqualityComparer<UserProfileDto>
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool Equals(UserProfileDto x, UserProfileDto y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }

            return (x.ProfileId == y.ProfileId &&
                x.FirstName == y.FirstName &&
                x.LastName == y.LastName);
        }

        public int GetHashCode(UserProfileDto obj)
        {
            throw new NotImplementedException();
        }
    }

    public class RoleDto : IEqualityComparer<RoleDto>
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public List<PermissionDto> Permissions { get; set; }

        public bool Equals(RoleDto x, RoleDto y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }

            return (x.RoleId == y.RoleId &&
                x.RoleName == y.RoleName);
        }

        public int GetHashCode(RoleDto obj)
        {
            throw new NotImplementedException();
        }
    }

    public class PermissionDto : IEqualityComparer<PermissionDto>
    {   
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }

        public bool Equals(PermissionDto x, PermissionDto y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }

            return (x.PermissionId == y.PermissionId &&
                x.PermissionName == y.PermissionName);
        }

        public int GetHashCode(PermissionDto obj)
        {
            throw new NotImplementedException();
        }
    }
}