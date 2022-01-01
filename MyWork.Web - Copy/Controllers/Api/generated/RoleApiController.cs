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
    [Route("api/v1/role")]
    public class RoleApiController : ControllerBase
    {
        private readonly IRoleRepository roleRepository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public RoleController(IMyWorkDbContextRepository rs){
            this.roleRepository = rs;
        }

        [HttpGet]
        [(Route("getall"))]
        [Display(Description = "Get All Role>")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var results = roleRepository.GetAll();
                return Request.CreateResponse<List<Role>>(HttpStatusCode.OK, results);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                throw new Exception(ex.StackTrace);
            }
        }

        [HttpGet]
        [(Route("get/{id}"))]
        [Display(Description = "Get Role> by id value")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var result = roleRepository.GetById(id);
                return Request.CreateResponse<Role>(HttpStatusCode.OK, result);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                throw new Exception(ex.StackTrace);
            }
        }

        [HttpPost]
        [(Route("create"))]
        [Display(Description = "create a new Role> )]
        public HttpResponseMessage Create(Role role)
        {
            try
            {
                var result = roleRepository.Create(role);
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
        [Display(Description = "update existing Role> )]
        public HttpResponseMessage Update(Role role)
        {
            try
            {
                var result = roleRepository.Update(role);
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
        [Display(Description = "delete existing Role> by id value)]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var result = roleRepository.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                throw new Exception(ex.StackTrace);
            }
        }        

         [HttpGet]
        [(Route("query/shortname/{shortname}"))]
        [Display(Description = "query Role> by shortname value)]
        public HttpResponseMessage QueryShortName(System.String shortname)
        {
            try
            {
                var results = roleRepository.SearchByShortName(shortname);
                return Request.CreateResponse<List<Role>>(HttpStatusCode.OK, results);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                throw new Exception(ex.StackTrace);
            }
        }             
         [HttpGet]
        [(Route("query/description/{description}"))]
        [Display(Description = "query Role> by description value)]
        public HttpResponseMessage QueryDescription(System.String description)
        {
            try
            {
                var results = roleRepository.SearchByDescription(description);
                return Request.CreateResponse<List<Role>>(HttpStatusCode.OK, results);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                throw new Exception(ex.StackTrace);
            }
        }             
        
    }
}

