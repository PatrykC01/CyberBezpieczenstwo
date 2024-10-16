namespace ZadanieASP1.ViewModel
{
    public class TripDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string URLImg { get; set; }
        public float Price { get; set; }
        public DateOnly Date { get; set; }
    }
}
