namespace Kolokwium_apbd_s20414.Models
{
    public class Status
    {
        public int ID { get; set; }

        public string Name { get; set; } = null!;

        public List<Order> Orders { get; set; }
    }
}
