namespace ZadanieASP1.Models
{
    
    public class Customer
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
    }
}
