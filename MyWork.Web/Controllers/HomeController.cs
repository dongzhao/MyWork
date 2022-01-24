using MyWork.Web.Authorize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWork.Web.Controllers
{
    //[ActionAuthorizeAttribute]
    public class HomeController : BaseController
    {
        public HomeController(IAuthorizeProvider p) : base(p)
        {

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewProfile()
        {
            return View(UserProfile);
        }

    }
}