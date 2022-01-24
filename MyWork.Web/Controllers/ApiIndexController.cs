using MyWork.Web.Authorize;
using MyWork.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MyWork.Web.Controllers
{
    public class ApiIndexController : Controller
    {
        public ActionResult Index()
        {
            var request = HttpContext.Request;
            var baseUrl = string.Format("{0}://{1}", request.Url.Scheme, request.Url.Authority);
            var directories = GetApiRouteInfo(baseUrl);
            return View(directories);
        }

        private List<ApiIndexView> GetApiRouteInfo(string baseUri)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var typeList = assembly.GetTypes();
            var directories = new List<ApiIndexView>();
            var prefix = "";
            foreach (var type in typeList)
            {
                if (type.BaseType != typeof(BaseApiController)) continue;

                var attrs = type.GetCustomAttributes<System.Web.Http.RoutePrefixAttribute>();
                if (attrs != null && attrs.Count() > 0)
                {
                    prefix = attrs.FirstOrDefault().Prefix;
                }

                foreach (var method in type.GetMethods())
                {
                    var d = new ApiIndexView()
                    {
                        ApiName = type.Name,
                        MethodName = method.Name,
                    };

                    var attr = method.GetCustomAttributes<System.Web.Http.RouteAttribute>();
                    
                    
                    if (attr == null || attr.Count() < 1) continue;

                    d.Url = baseUri + "/" + prefix + "/" + attr.FirstOrDefault().Template;

                    var dAttr = method.GetCustomAttributes<System.ComponentModel.DataAnnotations.DisplayAttribute>();
                    if (dAttr != null && dAttr.Count() > 0)
                    {
                        d.Description = dAttr.FirstOrDefault().Description;
                    }
                    var getAttr = method.GetCustomAttributes<System.Web.Http.HttpGetAttribute>();
                    if (getAttr != null && getAttr.Count() > 0)
                    {
                        d.Behaviour = "GET";
                        directories.Add(d);
                        continue;
                    }
                    var postAttr = method.GetCustomAttributes<System.Web.Http.HttpPostAttribute>();
                    if (postAttr != null && postAttr.Count() > 0)
                    {
                        d.Behaviour = "POST";
                        directories.Add(d);
                        continue;
                    }
                    var putAttr = method.GetCustomAttributes<System.Web.Http.HttpPutAttribute>();
                    if (putAttr != null && putAttr.Count() > 0)
                    {
                        d.Behaviour = "PUT";
                        directories.Add(d);
                        continue;
                    }
                    var delAttr = method.GetCustomAttributes<System.Web.Http.HttpDeleteAttribute>();
                    if (delAttr != null && delAttr.Count() > 0)
                    {
                        d.Behaviour = "DELETE";
                        directories.Add(d);
                        continue;
                    }
                }
            }
            return directories;
        }
    }
}