namespace EventEaseSystem.ViewModels
{
    public class BookingViewModel
    {
        public int BookingId { get; set; }
        public string VenueName { get; set; }
        public string EventName { get; set; }
        public DateTime BookingDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }
        public string? SpecialRequests { get; set; }
    }
}

