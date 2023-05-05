using AutoMapper;
using RestaurantAPI.Entities;
using RestaurantAPI.ExternalModels;
using RestaurantAPI.Infrastructure.Exceptions;
using RestaurantAPI.Services.UnitsOfWork;

namespace RestaurantAPI.Services.Managers
{
    public class UserService
    {
        private readonly IUserUnitOfWork _userUnit;
        private readonly IMapper _mapper;
        private readonly AuthorizationService authService;

        public UserService(IUserUnitOfWork userunit,
            IMapper mapper,
            AuthorizationService authorizationService)
        {
            _userUnit = userunit ?? throw new ArgumentNullException(nameof(userunit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            authService = authorizationService;
        }

        public UserDTO GetUser(int id)
        {
            var userEntity = _userUnit.Users.Get(id);
            if (userEntity == null)
                throw new ResourceMissingException($"User with id {id} doesn't exist!");
            return _mapper.Map<UserDTO>(userEntity);
        }

        public List<UserDTO> GetAllUsers()
        {
            var userEntities = _userUnit.Users.Find(u => u.Deleted == false || u.Deleted == null);
            if (userEntities == null)
                throw new ResourceMissingException("No user found!");
            return _mapper.Map<List<UserDTO>>(userEntities);
        }

        public void Register(UserDTO registerUser)
        {
            if (registerUser == null)
            {
                throw new ResourceMissingException("No register data found!");
            }
            var hasEmailConflict = _userUnit.Users
                .Any(c => c.Email == registerUser.Email);
            if (hasEmailConflict)
                throw new ResourceAlreadyExistsException($"User with email {registerUser.Email} already exists!");

            var hashedPassword = authService.HashPassword(registerUser.PasswordHash);
            var user = new User
            {
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                Email = registerUser.Email,
                PasswordHash = hashedPassword,
                IsAdmin = registerUser.IsAdmin
            };
            _userUnit.Users.Add(user);
            _userUnit.Complete();
        }

        public string? Validate(LoginDTO payload)
        {
            var user = _userUnit.Users.GetUserByEmail(payload.Email);

            if (user == null)
                throw new ResourceMissingException($"User with email {payload.Email} doesn't exist!");

            var passwordFine = authService.VerifyHashedPassword(user.PasswordHash, payload.Password);

            if (passwordFine)
            {
                var role = user.IsAdmin == true ? "ADMIN" : "USER";

                return authService.GetToken(user, role);
            }
            else
            {
                throw new InvalidCredentialsException("Invalid password!");
            }

        }

        public bool DeleteUser(int id)
        {
            var userEntity = _userUnit.Users.Get(id);
            if (userEntity == null)
                throw new ResourceMissingException($"User with id {id} doesn't exist!");

            userEntity.Deleted = true;
            _userUnit.Users.Remove(userEntity);
            _userUnit.Complete();
            return true;
        }

        public UserDTO UpdateUser(UpdateUserDTO user)
        {
            var userEntity = _mapper.Map<User>(user);
            if (userEntity == null)
                throw new ResourceMissingException($"User with id {user.Id} doesn't exist!");
            _userUnit.Users.Update(userEntity);
            _userUnit.Complete();
            return _mapper.Map<UserDTO>(userEntity);
        }
    }
}
