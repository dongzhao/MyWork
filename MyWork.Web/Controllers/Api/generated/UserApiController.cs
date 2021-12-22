/**
The controller class generated by codegen 
*/
using MyWork.Model;
using MyWork.Repository;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.ComponentModel.DataAnnotations;
using NLog;

namespace MyWork.Web.Controller
{
    [Route("api/v1/user")]
    public class UserApiController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public UserController(IMyWorkDbContextRepository rs){
            this.userRepository = rs;
        }

        [HttpGet]
        [(Route("getall"))]
        [Display(Description = "Get All User>")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var results = userRepository.GetAll();
                return Request.CreateResponse<List<User>>(HttpStatusCode.OK, results);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                throw new Exception(ex.StackTrace);
            }
        }

        [HttpGet]
        [(Route("get/{id}"))]
        [Display(Description = "Get User> by id value")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var result = userRepository.GetById(id);
                return Request.CreateResponse<User>(HttpStatusCode.OK, result);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                throw new Exception(ex.StackTrace);
            }
        }

        [HttpPost]
        [(Route("create"))]
        [Display(Description = "create a new User> )]
        public HttpResponseMessage Create(User user)
        {
            try
            {
                var result = userRepository.Create(user);
                return Request.CreateResponse<int>(HttpStatusCode.OK, result);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                throw new Exception(ex.StackTrace);
            }
        }

        [HttpPost]
        [(Route("update"))]
        [Display(Description = "update existing User> )]
        public HttpResponseMessage Update(User user)
        {
            try
            {
                var result = userRepository.Update(user);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                throw new Exception(ex.StackTrace);
            }
        }

        [HttpDelete]
        [(Route("delete/{id}"))]
        [Display(Description = "delete existing User> by id value)]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var result = userRepository.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                throw new Exception(ex.StackTrace);
            }
        }        

         [HttpGet]
        [(Route("query/username/{username}"))]
        [Display(Description = "query User> by username value)]
        public HttpResponseMessage QueryUserName(System.String username)
        {
            try
            {
                var results = userRepository.SearchByUserName(username);
                return Request.CreateResponse<List<User>>(HttpStatusCode.OK, results);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                throw new Exception(ex.StackTrace);
            }
        }             
         [HttpGet]
        [(Route("query/emailaddress/{emailaddress}"))]
        [Display(Description = "query User> by emailaddress value)]
        public HttpResponseMessage QueryEmailAddress(System.String emailaddress)
        {
            try
            {
                var results = userRepository.SearchByEmailAddress(emailaddress);
                return Request.CreateResponse<List<User>>(HttpStatusCode.OK, results);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                throw new Exception(ex.StackTrace);
            }
        }             
        
    }
}

