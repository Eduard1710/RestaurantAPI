using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.ExternalModels
{
    public class MenuDTO
    {
        public int CategoryID { get; set; }
        public CategoryDTO? Category { get; set; }
        public string MenuName { get; set; } = null!;
        public float Price { get; set; }
    }
}
