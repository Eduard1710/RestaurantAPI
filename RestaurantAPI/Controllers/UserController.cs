using RestaurantAPI.ExternalModels;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Services.Managers;
using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Controllers
{
    [Route("user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        [Route("{id}", Name = "GetUser")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult GetUser(int id)
        {
            var userEntity = _userService.GetUser(id);
            if (userEntity == null)
            {
                return NotFound();
            }
            return Ok(userEntity);
        }
        [HttpGet]
        [Route("", Name = "GetAllUsers")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult GetAllUsers()
        {
            var userEntities = _userService.GetAllUsers();
            if (userEntities == null)
            {
                return NotFound();
            }
            return Ok(userEntities);
        }
        [HttpDelete]
        [Route("delete/{id}", Name = "DeleteUser")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult DeleteUser(int id)
        {
            var deleteUser = _userService.DeleteUser(id);
            if (deleteUser == false)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPut]
        [Route("{id}", Name = "UpdateUser")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult UpdateUser([FromBody] UpdateUserDTO user)
        {
            var userEntity = _userService.UpdateUser(user);
            return Ok(userEntity);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register(UserDTO payload)
        {
            _userService.Register(payload);
            return Ok();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(LoginDTO payload)
        {
            var jwtToken = _userService.Validate(payload);

            return Ok(new { token = jwtToken });
        }

    }
}
