using AutoMapper;
using RestaurantAPI.Entities;
using RestaurantAPI.ExternalModels;
using RestaurantAPI.Infrastructure.Exceptions;
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
            if (menuEntity == null)
                throw new ResourceMissingException($"Menu with id {id} doesn't exist!");
            return _mapper.Map<MenuDTO>(menuEntity);
        }

        public List<MenuDTO> GetAllMenus()
        {
            var menuEntities = _menuUnit.Menus.Find(o => o.Deleted == false || o.Deleted == null);
            if (menuEntities == null)
                throw new ResourceMissingException("No menu found!");
            return _mapper.Map<List<MenuDTO>>(menuEntities);
        }

        public PagedResult<Menu> GetAllMenusOrderedByName(int pageNumber, int pageSize)
        {
            var totalItems = _menuUnit.Menus.GetTotalCount();
            if (totalItems == 0)
                throw new ResourceMissingException("No menu found!");
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
            if (menuEntity == null)
                throw new ResourceMissingException($"Menu with id {id} doesn't exist!");
            return _mapper.Map<MenuDTO>(menuEntity);
        }

        public MenuDTO AddMenu(MenuDTO menu)
        {
            var menuEntity = _mapper.Map<Menu>(menu);
            var hasNameConflict = _menuUnit.Menus
                .Any(c => c.MenuName == menu.MenuName);

            if (hasNameConflict)
                throw new ResourceAlreadyExistsException($"Menu with name {menu.MenuName} already exists!");

            _menuUnit.Menus.Add(menuEntity);
            _menuUnit.Complete();
            return _mapper.Map<MenuDTO>(menuEntity);
        }

        public CategoryDTO GetCategoryDetails(int id)
        {
            var categoryEntity = _menuUnit.Categories.GetCategoryDetails(id);
            if (categoryEntity == null)
                throw new ResourceMissingException($"Category with id {id} doesn't exist!");
            return _mapper.Map<CategoryDTO>(categoryEntity);
        }

        public bool DeleteMenu(int id)
        {
            var menuEntity = _menuUnit.Menus.Get(id);
            if (menuEntity == null)
                throw new ResourceMissingException($"Menu with id {id} doesn't exist!");

            menuEntity.Deleted = true;
            _menuUnit.Menus.Remove(menuEntity);
            _menuUnit.Complete();
            return true;
        }
        public MenuDTO UpdateMenu(UpdateMenuDTO menu)
        {
            var menuEntity = _mapper.Map<Menu>(menu);
            if (menuEntity == null)
                throw new ResourceMissingException($"Menu with id {menu.ID} doesn't exist!");
            _menuUnit.Menus.Update(menuEntity);
            _menuUnit.Complete();
            return _mapper.Map<MenuDTO>(menuEntity);
        }
    }
}
