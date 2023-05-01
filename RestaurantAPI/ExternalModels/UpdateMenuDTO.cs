namespace RestaurantAPI.ExternalModels
{
    public class UpdateMenuDTO
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public string MenuName { get; set; } = null!;
        public float Price { get; set; }
    }
}
