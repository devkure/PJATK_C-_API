namespace Kolokwium_apbd_s20414.Models
{
    public class Product
    {
        public int ID { get; set; }

        public string Name { get; set; } = null!;

        public double Price { get; set; }

        public List<ProductOrder> ProductOrders { get; set; }
    }
}
