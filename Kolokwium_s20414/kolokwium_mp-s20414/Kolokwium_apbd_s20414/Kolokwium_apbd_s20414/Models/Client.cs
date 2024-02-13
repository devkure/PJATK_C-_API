namespace Kolokwium_apbd_s20414.Models
{
    public class Client
    {
        public int ID { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public List<Order> Orders { get; set; }
    }
}
