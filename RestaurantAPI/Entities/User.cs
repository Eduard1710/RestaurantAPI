using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Entities
{
    public class User
    {
        public int ID { get; set; }

        [MaxLength(150)]
        public string FirstName { get; set; } = null!;

        [MaxLength(150)]
        public string LastName { get; set; } = null!;

        [MaxLength(150)]
        public string PasswordHash { get; set; } = null!;

        [MaxLength(150)]
        public string Email { get; set; } = null!;
        public bool IsAdmin { get; set; }
        public bool? Deleted { get; set; }

        public virtual ICollection<Order> Order { get; set; } = null!;
    }

}
