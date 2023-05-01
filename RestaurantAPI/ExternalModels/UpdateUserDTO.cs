namespace RestaurantAPI.ExternalModels
{
    public class UpdateUserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsAdmin { get; set; }
    }
}
