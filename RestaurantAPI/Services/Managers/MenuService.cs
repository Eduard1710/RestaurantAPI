using AutoMapper;
using RestaurantAPI.Entities;
using RestaurantAPI.ExternalModels;
using RestaurantAPI.Services.UnitsOfWork;

namespace RestaurantAPI.Services.Managers
{
    public class MenuService
    {
        private readonly IMenuUnitOfWork _menuUnit;
        private readonly IMapper _mapper;
        public MenuService(IMenuUnitOfWork menuUnit, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _menuUnit = menuUnit ?? throw new ArgumentNullException(nameof(menuUnit));
        }

        public MenuDTO GetMenu(int id)
        {
            var menuEntity = _menuUnit.Menus.Get(id);
            return _mapper.Map<MenuDTO>(menuEntity);
        }

        public List<MenuDTO> GetAllMenus()
        {
            var menuEntities = _menuUnit.Menus.Find(o => o.Deleted == false || o.Deleted == null);
            return _mapper.Map<List<MenuDTO>>(menuEntities);
        }

        public PagedResult<Menu> GetAllMenusOrderedByName(int pageNumber, int pageSize)
        {
            var totalItems = _menuUnit.Menus.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var menus =  _menuUnit.Menus.GetPaginatedList(pageNumber, pageSize);

            return new PagedResult<Menu>
            {
                Items = menus.Items,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }


        public MenuDTO GetMenuDetails(int id)
        {
            var menuEntity = _menuUnit.Menus.GetMenuDetails(id);
            return _mapper.Map<MenuDTO>(menuEntity);
        }

        public MenuDTO AddMenu(MenuDTO menu)
        {
            var menuEntity = _mapper.Map<Menu>(menu);
            _menuUnit.Menus.Add(menuEntity);
            _menuUnit.Complete();
            return _mapper.Map<MenuDTO>(menuEntity);
        }

        public CategoryDTO GetCategoryDetails(int id)
        {
            var categoryEntity = _menuUnit.Categories.GetCategoryDetails(id);
            return _mapper.Map<CategoryDTO>(categoryEntity);
        }

        public bool DeleteMenu(int id)
        {
            var menuEntity = _menuUnit.Menus.Get(id);
            if (menuEntity == null)
            {
                return false;
            }
            menuEntity.Deleted = true;
            _menuUnit.Menus.Remove(menuEntity);
            _menuUnit.Complete();
            return true;
        }
        public MenuDTO UpdateMenu(UpdateMenuDTO menu)
        {
            var menuEntity = _mapper.Map<Menu>(menu);
            _menuUnit.Menus.Update(menuEntity);
            _menuUnit.Complete();
            return _mapper.Map<MenuDTO>(menuEntity);
        }
    }
}
