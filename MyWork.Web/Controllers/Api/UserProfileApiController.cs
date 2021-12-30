
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
using MyWork.Web.Models;

namespace MyWork.Web.Controllers
{
    [RoutePrefix("api/v1/userinfo")]
    public class UserProfileApiController : BaseApiController
    {
        private readonly IUserRepository userRepository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public UserProfileApiController(IUserRepository rs){
            this.userRepository = rs;
        }
        [HttpGet]
        [Route("servicename")]
        public IHttpActionResult GetName()
        {
            return Json("UserProfileApiController");
        }

        [HttpGet]
        [Route("get/{username}")]
        [Display(Description = "Get User details by username")]
        public IHttpActionResult Get(string username )
        {
            
            try
            {
                var result = userRepository.SearchByUserName(username).FirstOrDefault();
                var dto = new UserInfo()
                {
                    UserId = result.Id,
                    UserName = username,
                    Email = result.EmailAddress,
                    UserProfile = new UserProfileDto()
                    {
                        ProfileId = result.UserProfile.Id,
                        FirstName = result.UserProfile.FirstName,
                        LastName = result.UserProfile.LastName,
                    },
                    Roles = new List<RoleDto>(),
                };
                
                foreach(var role in result.Roles)
                {
                    var roleDto = new RoleDto()
                    {
                        RoleId = role.Id,
                        RoleName = role.ShortName,
                        Permissions = new List<PermissionDto>(),
                    };  

                    foreach (var permission in role.Permissions)
                    {
                        roleDto.Permissions.Add(new PermissionDto() 
                        {
                            PermissionId = permission.Id,
                            PermissionName = permission.ShortName,
                        });
                        
                    }

                    dto.Roles.Add(roleDto);
                }

                return Json(dto);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                throw new Exception(ex.StackTrace);
            }
        }       
    }
}

