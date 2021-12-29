using MyWork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
//using System.Web.Mvc;

namespace MyWork.WebAPI.Controllers
{
    [RoutePrefix("api/v1/mytest")]
    public class MyTestController : BaseController
    {
        // GET: api/MyTest
        [HttpGet]
        [Route("getall")]
        public IHttpActionResult GetAll()
        {
            var users = new List<User>() { 
                new User()
                {
                    Id = 1,
                    UserName = "user1",
                    Password = "user123",
                    EmailAddress = "abc@test.com",
                },
                new User()
                {
                    Id = 2,
                    UserName = "user2",
                    Password = "user123",
                    EmailAddress = "abc@test.com",
                },
            };
            //var strList = new string[] { "value1", "value2" }; 
            return Json(users);
        }

        // GET: api/MyTest/5
        [HttpGet]
        [Route("get/{id}")]
        public IHttpActionResult Get(int id)
        {
            return Json("value");
        }

        // POST: api/MyTest
        [HttpPost]
        [Route("create")]
        public IHttpActionResult Post([FromBody]User dto)
        {
            var user = new User();
            user = dto;
            return Json("ok");
        }

        // PUT: api/MyTest/5
        [HttpPost]
        [Route("update")]
        public void Put([FromBody]string value)
        {
        }

        // DELETE: api/MyTest/5
        [HttpDelete]
        [Route("delete/{id}")]
        public void Delete(int id)
        {
            
        }
    }

}
