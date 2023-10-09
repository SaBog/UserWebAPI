using Microsoft.AspNetCore.Mvc;
using UserWebAPI.Exceptions.Filters;
using UserWebAPI.Models;
using UserWebAPI.QueryExtensions.Params;
using UserWebAPI.Services.Interfaces;

namespace UserWebAPI.Controllers
{
    [ApiController]
    [Route("api/roles")]
    [TypeFilter(typeof(ExceptionFilter))]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService rolesService)
        {
            _roleService = rolesService;
        }

        /// <summary>
        /// Get all roles using filter (support filtering and sorting by multiple properties)
        /// string properties use function "LIKE" therefore you can give them pattern
        /// </summary>
        /// <param name="params"></param>
        /// <response code="200">Returns roles matching filters</response>
        [HttpGet]
        public ActionResult GetRoles([FromQuery] RoleQueryParams @params)
        {
            var list = _roleService.GetAll(@params);
            return Ok(list);
        }

        /// <summary>
        /// Get a specific role by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns the role</response>
        /// <response code="404">Role not found</response>
        [HttpGet("{id}")]
        public ActionResult<Role> GetRole(int id)
        {
            var role = _roleService.GetById(id);
            return Ok(role);
        }

    }

}