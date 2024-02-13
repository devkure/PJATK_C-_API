namespace Kolokwium_apbd_s20414.Models
{
    public class Order
    {
        public int ID { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? FulfilledAt { get; set; }

        public int Client_ID { get; set; }

        public int Status_ID { get; set; }

        public Client Client { get; set; }

        public Status Status { get; set; }

        public List<ProductOrder> ProductOrders { get; set; }
    }
}
