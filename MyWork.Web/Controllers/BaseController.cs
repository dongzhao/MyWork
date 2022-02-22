using MyWork.Web.Authorize;
using MyWork.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyWork.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly IAuthorizeProvider provider;

        private UserProfileDto _userProfile;
        protected UserProfileDto UserProfile
        {
            get 
            {
                return provider.UserProfile; 
            }
            set
            {
                provider.UserProfile = value;
            }
        }

        public BaseController(IAuthorizeProvider p)
        {
            this.provider = p;
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            ViewBag.UserName = UserProfile.UserName;
        }
    }
}