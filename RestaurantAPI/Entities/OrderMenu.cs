
namespace RestaurantAPI.Entities
{
    public class OrderMenu
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int MenuID { get; set; }
        public Order Order { get; set; } = null!;
        public Menu Menu { get; set; } = null!;
        public bool? Deleted { get; set; }
    }
}
