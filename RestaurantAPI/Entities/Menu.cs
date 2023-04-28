using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Entities
{
    public class Menu
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; } = null!;
        [MaxLength(100)]
        public string MenuName { get; set; } = null!;
        [Required]
        public float Price { get; set; }
        public List<OrderMenu> OrderMenu { get; set; } = null!;
        public bool? Deleted { get; set; } 

    }
}
