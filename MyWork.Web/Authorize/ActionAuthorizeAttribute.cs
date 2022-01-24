using MyWork.Web.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyWork.Web.Authorize
{
    public class ActionAuthorizeAttribute : AuthorizeAttribute
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = filterContext.ActionDescriptor.ActionName;
            var permission = string.Format("{0}-{1}", controllerName, actionName);

            var authProvider = DependencyResolver.Current.GetService<IAuthorizeProvider>();
            var valid = authProvider.HasPermission(permission);
            if (!valid)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "Index" }, { "controller", "Unauthorized" } });
            }

            //var logonName = filterContext.HttpContext.User.Identity.Name;
            //var userInfo = (UserInfo)filterContext.HttpContext.Session["user_info"];
            //var valid = false;
            //try
            //{
            //    var authProvider = DependencyResolver.Current.GetService<IAuthorizeProvider>();
            //    if (userInfo == null)
            //    {
            //        var username = authProvider.ToShortUserName(logonName);
            //        userInfo = authProvider.UserProfile(username);
            //        filterContext.HttpContext.Session["user_info"] = userInfo;
            //    }
            //    valid = authProvider.HasPermission(permission, userInfo);
            //}
            //catch(Exception ex)
            //{
            //    logger.Error(ex.StackTrace);
            //    throw new Exception("Unthroized user!");
            //}
            //finally
            //{
            //    if (!valid)
            //    {
            //        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "Index" }, { "controller", "Unauthorized" } });
            //    }
            //}
        }

    }
}