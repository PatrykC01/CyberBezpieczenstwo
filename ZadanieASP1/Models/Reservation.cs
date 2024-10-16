namespace ZadanieASP1.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public int TripID { get; set; }
        public int CustomerID { get; set; }
        public DateTime ReservationDate { get; set; }

        public DateTime DateOfDeparture { get; set; }
        public DateTime DateOfReturn { get; set; }

        public Trip? Trip { get; set; }
        public Customer? Customer { get; set; }
    }
}
