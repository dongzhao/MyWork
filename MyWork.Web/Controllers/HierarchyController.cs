using MyWork.Web.Authorize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWork.Web.Controllers
{
    //[ActionAuthorizeAttribute]
    public class HierarchyController : BaseController
    {
        private readonly IHierarchyEditor editor;
        public HierarchyController(IAuthorizeProvider p, IHierarchyEditor ed) 
            : base(p)
        {
            this.editor = ed;
        }
        // GET: Hierarchy
        public ActionResult Index()
        {
            var list = editor.GetAllTreeRoots();
            return View(list);
        }
    }
}