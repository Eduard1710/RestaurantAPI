using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Entities
{
    public class Order
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; } = null!;

        [MaxLength(150)]
        public string Address { get; set; } = null!;

        [MaxLength(10)]
        public string Phone { get; set; } = null!;
        public virtual ICollection<OrderMenu> OrderMenu { get; set; } = null!;
        public bool? Deleted { get; set; }

    }
}
