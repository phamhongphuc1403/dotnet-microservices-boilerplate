using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyCRM.Domain.Constants;


namespace TinyCRM.API.Controllers
{
    [Route("api/permissions")]
    [ApiController]
    [Authorize(Roles = Role.SuperAdmin)]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _service;

        public PermissionController(IPermissionService permissionService)
        {
            _service = permissionService;
        }

        [HttpGet]
        public ActionResult<List<PermissionContent>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("roles/{roleId:Guid}/permissions")]
        public ActionResult<List<PermissionContent>> GetAllByRoleIdAsync(Guid roleId)
        {
            return Ok(_service.GetAllByRoleIdAsync(roleId));
        }
    }



    public interface IPermissionService
    {
        List<PermissionContent> GetAll();
        Task<List<PermissionContent>> GetAllByRoleIdAsync(Guid roleId);
    }

    public class PermissionService : IPermissionService
    {
        public List<PermissionContent> GetAll()
        {
            return Permission.PermissionsList.ToList();
        }

        public Task<List<PermissionContent>> GetAllByRoleIdAsync(Guid roleId)
        {
            throw new NotImplementedException();
        }
    }
}
