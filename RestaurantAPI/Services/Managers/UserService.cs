using AutoMapper;
using RestaurantAPI.Entities;
using RestaurantAPI.ExternalModels;
using RestaurantAPI.Services.UnitsOfWork;

namespace RestaurantAPI.Services.Managers
{
    public class UserService
    {
        private readonly IUserUnitOfWork _userUnit;
        private readonly IMapper _mapper;

        public UserService(IUserUnitOfWork userunit,
            IMapper mapper)
        {
            _userUnit = userunit ?? throw new ArgumentNullException(nameof(userunit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public UserDTO GetUser(int id)
        {
            var userEntity = _userUnit.Users.Get(id);
            return _mapper.Map<UserDTO>(userEntity);
        }

        public List<UserDTO> GetAllUsers()
        {
            var userEntities = _userUnit.Users.Find(u => u.Deleted == false || u.Deleted == null);
            return _mapper.Map<List<UserDTO>>(userEntities);
        }

        public UserDTO AddUser(UserDTO user)
        {
            var userEntity = _mapper.Map<User>(user);
            _userUnit.Users.Add(userEntity);
            _userUnit.Complete();
            return _mapper.Map<UserDTO>(userEntity);
        }

        public bool DeleteUser(int id)
        {
            var userEntity = _userUnit.Users.Get(id);
            if (userEntity == null)
            {
                return false;
            }
            userEntity.Deleted = true;
            _userUnit.Users.Remove(userEntity);
            _userUnit.Complete();
            return true;
        }

        public UserDTO UpdateUser(UpdateUserDTO user)
        {
            var userEntity = _mapper.Map<User>(user);
            _userUnit.Users.Update(userEntity);
            _userUnit.Complete();
            return _mapper.Map<UserDTO>(userEntity);
        }
    }
}
