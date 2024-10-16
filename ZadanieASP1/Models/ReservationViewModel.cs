namespace ZadanieASP1.Models
{
    public class ReservationViewModel
    {
        public int ReservationID { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public DateTime DateOfReturn { get; set; }

        public string Title { get; set; }

        public string CustomerName { get; set; }
    }

}
