using RestaurantAPI.ExternalModels;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Services.Managers;
using Microsoft.AspNetCore.Authorization;
using RestaurantAPI.Infrastructure.Exceptions;

namespace RestaurantAPI.Controllers
{
    [Route("User")]
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
            try
            {
                var userEntity = _userService.GetUser(id);
                return Ok(userEntity);
            }
            catch (ResourceMissingException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("", Name = "GetAllUsers")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var userEntities = _userService.GetAllUsers();
                return Ok(userEntities);
            }
            catch (ResourceMissingException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete/{id}", Name = "DeleteUser")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var deleteUser = _userService.DeleteUser(id);
                return NoContent();
            }
            catch (ResourceMissingException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut]
        [Route("{id}", Name = "UpdateUser")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult UpdateUser([FromBody] UpdateUserDTO user)
        {
            try
            {
                var userEntity = _userService.UpdateUser(user);
                return Ok(userEntity);
            }
            catch (ResourceMissingException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public IActionResult Register(UserDTO payload)
        {
            try
            {
                _userService.Register(payload);
                return Ok();
            }
            catch (ResourceAlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login(LoginDTO payload)
        {
            try
            {
                var jwtToken = _userService.Validate(payload);

                return Ok(new { token = jwtToken });
            }
            catch (InvalidCredentialsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ResourceMissingException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
