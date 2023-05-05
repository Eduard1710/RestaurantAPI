using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;
using RestaurantAPI.ExternalModels;
using RestaurantAPI.Infrastructure.Exceptions;
using RestaurantAPI.Services;
using RestaurantAPI.Services.Managers;
using Swashbuckle.AspNetCore.Annotations;

namespace RestaurantAPI.Controllers
{
    [Route("Menu")]
    [ApiController]
    [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly MenuService _menuService;
        public MenuController(MenuService menuService)
        {
            _menuService = menuService ?? throw new ArgumentNullException(nameof(menuService));
        }

        [HttpGet]
        [Route("{id}", Name = "GetMenu")]
        public IActionResult GetMenu(int id)
        {
            try
            {
                var menuEntity = _menuService.GetMenu(id);
                return Ok(menuEntity);
            }
            catch (ResourceMissingException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ordered-by-name", Name = "GetAllMenusOrderedByName")]
        [ProducesResponseType(typeof(PagedResult<MenuDTO>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public IActionResult GetAllMenusOrderedByName([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var menuEntities = _menuService.GetAllMenusOrderedByName(pageNumber, pageSize);
                return Ok(menuEntities);
            }
            catch (ResourceMissingException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("", Name = "GetAllMenus")]
        public IActionResult GetAllMenus()
        {
            try
            {
                var menuEntities = _menuService.GetAllMenus();
                return Ok(menuEntities);
            }
            catch (ResourceMissingException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Details/{id}", Name = "GetMenuDetails")]
        public IActionResult GetMenuDetails(int id)
        {
            try
            {
                var menuEntity = _menuService.GetMenuDetails(id);
                return Ok(menuEntity);
            }
            catch (ResourceMissingException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Add", Name = "AddMenu")]
        public IActionResult AddMenu([FromBody] MenuDTO menu)
        {
            try
            {
                var mEntity = _menuService.AddMenu(menu);
                return Ok(mEntity);
            }
            catch (ResourceAlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut]
        [Route("{id}", Name = "UpdateMenu")]
        public IActionResult UpdateMenu([FromBody] UpdateMenuDTO menu)
        {
            try
            {
                var menuEntity = _menuService.UpdateMenu(menu);
                return Ok(menuEntity);
            }
            catch (ResourceMissingException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Category/{categoryId}", Name = "GetCategoryDetails")]
        public IActionResult GetCategoryDetails(int categoryId)
        {
            try
            {
                var categoryEntity = _menuService.GetCategoryDetails(categoryId);
                return Ok(categoryEntity);
            }
            catch (ResourceMissingException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpDelete]
        [Route("Delete/{id}", Name = "DeleteMenu")]
        public IActionResult DeleteMenu(int id)
        {
            try
            {
                var deleteMenu = _menuService.DeleteMenu(id);
                return NoContent();
            }
            catch (ResourceMissingException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
