using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserWebAPI.DTO;
using UserWebAPI.Exceptions.Filters;
using UserWebAPI.Models;
using UserWebAPI.QueryExtensions.Params;
using UserWebAPI.Services.Interfaces;

namespace UserWebAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    [TypeFilter(typeof(ExceptionFilter))]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get all users using filter (support filtering and sorting by multiple properties)
        /// string properties use function "LIKE" therefore you can give them pattern
        /// </summary>
        /// <param name="params"></param>
        /// <response code="200">Returns users matching filters</response>
        [HttpGet]
        public ActionResult GetUsers([FromQuery] UserQueryParams @params)
        {
            var list = _userService.GetAll(@params);
            return Ok(list);
        }

        /// <summary>
        /// Get a specific user by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns the user</response>
        /// <response code="404">User not found</response>
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _userService.GetById(id);
            return Ok(user);
        }

        /// <summary>
        /// Creates a new user (roles are assigned based on their id)
        /// </summary>
        /// <param name="user"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/users
        ///     {
        ///        "name": "Anton",
        ///        "age": 21,
        ///        "email": "example@mail.com",
        ///        "roles" : [
        ///             { "id": 1 }, { "id": 4 }
        ///        ]
        ///     }
        ///
        /// </remarks>
        /// <response code="201">User successfully created</response>
        /// <response code="400">If the user is not created</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult CreateUser(UserDto user)
        {
            _userService.Create(user);
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Update a specific user by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <response code="200">User successfully updated</response>
        /// <response code="404">User not found</response>
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserDto user)
        {
            user.Id = id;
            _userService.Update(user);
            return Ok();
        }

        /// <summary>
        /// Delete a specific user by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">User deleted</response>
        /// <response code="403">You not have access to execute this api</response>
        /// <response code="404">User not found</response>
        [Authorize(Roles = "Admin, SuperAdmin")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpDelete("{id}")]
        public ActionResult<User> DeleteUser(int id)
        {
            _userService.Delete(id);
            return NoContent();

        }

        /// <summary>
        /// Get JSON Web Token by email
        /// </summary>
        /// <param name="email"></param>
        /// <response code="200">returns JWT</response>
        /// <response code="404">User not found</response>
        [HttpPost("/login")]
        public IActionResult Token(string email)
        {
            var token = _userService.GetToken(email);
            return Ok(token);
        }
    }
}