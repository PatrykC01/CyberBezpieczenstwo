using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZadanieASP1.Models
{
    public class Trip
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TripID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public DateTime TripDate { get; set; }
        public string Duration { get; set; }

        public ICollection<Reservation>? Reservations { get; set; }
    }
}
