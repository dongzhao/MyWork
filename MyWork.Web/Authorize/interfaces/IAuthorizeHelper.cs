using MyWork.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Web.Authorize
{
    public interface IAuthorizeHelper
    {
        UserInfo GetUserInfo(string username);
        bool HasPermission(string permission, UserInfo userInfo);
        string ToShortUserName(string logonName);
    }
}
