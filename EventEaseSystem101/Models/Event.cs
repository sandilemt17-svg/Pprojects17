using System.ComponentModel.DataAnnotations;

namespace EventEaseSystem.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Event Name")]
        public string EventName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Description")]
        [StringLength(500)]
        public string? Description { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }

        // Navigation property
        public ICollection<Booking>? Bookings { get; set; }

    }
}
