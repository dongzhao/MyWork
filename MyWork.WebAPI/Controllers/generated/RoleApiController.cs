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

namespace MyWork.WebAPI.Controllers
{
    [Route("api/v1/role")]
    public class RoleApiController : BaseController
    {
        private readonly IRoleRepository roleRepository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public RoleApiController(IRoleRepository rs){
            this.roleRepository = rs;
        }

        [HttpGet]
        [Route("getall")]
        [Display(Description = "Get All Role")]
        public IHttpActionResult GetAll()
        {
            try
            {
                var results = roleRepository.GetAll();
                return Json(results);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                throw new Exception(ex.StackTrace);
            }
        }

        [HttpGet]
        [Route("get/{id}")]
        [Display(Description = "Get Role by id value")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var result = roleRepository.GetById(id);
                return Json(result);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                throw new Exception(ex.StackTrace);
            }
        }

        [HttpPost]
        [Route("create")]
        [Display(Description = "create a new Role")]
        public IHttpActionResult Create(Role role)
        {
            try
            {
                var result = roleRepository.Create(role);
                return Json(result);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                throw new Exception(ex.StackTrace);
            }
        }

        [HttpPost]
        [Route("update")]
        [Display(Description = "update existing Role")]
        public IHttpActionResult Update(Role role)
        {
            try
            {
                roleRepository.Update(role);
                return Json(HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                throw new Exception(ex.StackTrace);
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Display(Description = "delete existing Role by id")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                roleRepository.Delete(id);
                return Json(HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                throw new Exception(ex.StackTrace);
            }
        }        

         [HttpGet]
        [Route("query/shortname/{shortname}")]
        [Display(Description = "query Role by shortname value")]
        public IHttpActionResult QueryShortName(System.String shortname)
        {
            try
            {
                var results = roleRepository.SearchByShortName(shortname);
                return Json(results);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                throw new Exception(ex.StackTrace);
            }
        }             
         [HttpGet]
        [Route("query/description/{description}")]
        [Display(Description = "query Role by description value")]
        public IHttpActionResult QueryDescription(System.String description)
        {
            try
            {
                var results = roleRepository.SearchByDescription(description);
                return Json(results);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                throw new Exception(ex.StackTrace);
            }
        }             
        
    }
}

