namespace api_orders.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}