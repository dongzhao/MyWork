using MyWork.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Web.Authorize
{
    public interface IAuthorizeProvider
    {
        UserInfo UserProfile { get; set; }
        bool HasPermission(string permission);
    }
}
