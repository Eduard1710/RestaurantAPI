namespace RestaurantAPI.ExternalModels
{
    public class OrderDTO
    {
        public int UserID { get; set; }
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public ICollection<OrderMenuDTO> OrderMenu { get; set; } = null!;
    }
}
