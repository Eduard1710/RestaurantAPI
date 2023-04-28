using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Entities
{
    public class Category
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; } = null!;
        public bool? Deleted { get; set; }

        public virtual Menu Menu { get; set; } = null!;
    }
}
