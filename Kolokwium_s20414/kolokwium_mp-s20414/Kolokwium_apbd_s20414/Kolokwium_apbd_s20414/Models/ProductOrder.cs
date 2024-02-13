namespace Kolokwium_apbd_s20414.Models
{
    public class ProductOrder
    {
        public int Product_ID { get; set; }

        public int Order_ID { get; set; }

        public int Amount { get; set; }

        public Product Product { get; set; }

        public Order Order { get; set; }
    }
}
