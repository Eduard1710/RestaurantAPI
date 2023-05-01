using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;
using RestaurantAPI.ExternalModels;
using RestaurantAPI.Services;
using RestaurantAPI.Services.Managers;
using Swashbuckle.AspNetCore.Annotations;

namespace RestaurantAPI.Controllers
{
    [Route("menu")]
    [ApiController]
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
            var menuEntity = _menuService.GetMenu(id);
            if (menuEntity == null)
            {
                return NotFound();
            }
            return Ok(menuEntity);
        }

        [HttpGet]
        [Route("ordered-by-name", Name = "GetAllMenusOrderedByName")]
        [ProducesResponseType(typeof(PagedResult<MenuDTO>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public IActionResult GetAllMenusOrderedByName([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var menuEntities = _menuService.GetAllMenusOrderedByName(pageNumber,pageSize);
            if (menuEntities == null)
            {
                return NotFound();
            }
            return Ok(menuEntities);
        }

        [HttpGet]
        [Route("", Name = "GetAllMenus")]
        public IActionResult GetAllMenus()
        {
            var menuEntities = _menuService.GetAllMenus();
            if (menuEntities == null)
            {
                return NotFound();
            }
            return Ok(menuEntities);
        }

        [HttpGet]
        [Route("details/{id}", Name = "GetMenuDetails")]
        public IActionResult GetMenuDetails(int id)
        {
            var menuEntity = _menuService.GetMenuDetails(id);
            if (menuEntity == null)
            {
                return NotFound();
            }
            return Ok(menuEntity);
        }
        [HttpPost]
        [Route("add", Name = "AddMenu")]
        public IActionResult AddMenu([FromBody] MenuDTO menu)
        {
            var mEntity = _menuService.AddMenu(menu);
            return Ok(mEntity);
        }
        [HttpPut]
        [Route("{id}", Name = "UpdateMenu")]
        public IActionResult UpdateMenu([FromBody] UpdateMenuDTO menu)
        {
            var menuEntity = _menuService.UpdateMenu(menu);
            return Ok(menuEntity);
        }
        [HttpGet]
        [Route("category/{categoryId}", Name = "GetCategoryDetails")]
        public IActionResult GetCategoryDetails(int categoryId)
        {
            var categoryEntity = _menuService.GetCategoryDetails(categoryId);
            if (categoryEntity == null)
            {
                return NotFound();
            }
            return Ok(categoryEntity);
        }
        [HttpDelete]
        [Route("delete/{id}", Name = "DeleteMenu")]
        public IActionResult DeleteMenu(int id)
        {
            var deleteMenu = _menuService.DeleteMenu(id);
            if (deleteMenu == false)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
