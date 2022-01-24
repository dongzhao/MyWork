using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWork.Web.Models
{
    public class ApiIndexView
    {
        public string ApiName { get; set; }
        public string MethodName { get; set; }
        public string Url { get; set; }
        public string Behaviour { get; set; }
        public string Description { get; set; }
    }
}