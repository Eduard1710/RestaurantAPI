namespace RestaurantAPI.ExternalModels
{
    public class UpdateOrderDTO
    {
        public int ID{ get; set; }
        public int UserID { get; set; }
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public ICollection<UpdateOrderMenuDTO> OrderMenu { get; set; } = null!;
    }
}
