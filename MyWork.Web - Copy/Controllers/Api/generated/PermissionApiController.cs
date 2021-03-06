


















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
    [Route("api/v1/permission")]
    public class PermissionApiController : ControllerBase
    {
        private readonly IPermissionRepository permissionRepository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public PermissionController(IMyWorkDbContextRepository rs){
            this.permissionRepository = rs;
        }

        [HttpGet]
        [(Route("getall"))]
        [Display(Description = "Get All Permission>")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var results = permissionRepository.GetAll();
                return Request.CreateResponse<List<Permission>>(HttpStatusCode.OK, results);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                throw new Exception(ex.StackTrace);
            }
        }

        [HttpGet]
        [(Route("get/{id}"))]
        [Display(Description = "Get Permission> by id value")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var result = permissionRepository.GetById(id);
                return Request.CreateResponse<Permission>(HttpStatusCode.OK, result);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                throw new Exception(ex.StackTrace);
            }
        }

        [HttpPost]
        [(Route("create"))]
        [Display(Description = "create a new Permission> )]
        public HttpResponseMessage Create(Permission permission)
        {
            try
            {
                var result = permissionRepository.Create(permission);
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
        [Display(Description = "update existing Permission> )]
        public HttpResponseMessage Update(Permission permission)
        {
            try
            {
                var result = permissionRepository.Update(permission);
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
        [Display(Description = "delete existing Permission> by id value)]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var result = permissionRepository.Delete(id);
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
        [Display(Description = "query Permission> by shortname value)]
        public HttpResponseMessage QueryShortName(System.String shortname)
        {
            try
            {
                var results = permissionRepository.SearchByShortName(shortname);
                return Request.CreateResponse<List<Permission>>(HttpStatusCode.OK, results);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                throw new Exception(ex.StackTrace);
            }
        }             
         [HttpGet]
        [(Route("query/description/{description}"))]
        [Display(Description = "query Permission> by description value)]
        public HttpResponseMessage QueryDescription(System.String description)
        {
            try
            {
                var results = permissionRepository.SearchByDescription(description);
                return Request.CreateResponse<List<Permission>>(HttpStatusCode.OK, results);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                throw new Exception(ex.StackTrace);
            }
        }             
        
    }
}

