namespace APBD_5_S20414.Models.DTOs
{
    public class ClientToTripRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Pesel { get; set; }
        public int TripID { get; set; }
    }
}
