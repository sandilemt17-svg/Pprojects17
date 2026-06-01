using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventEaseSystem.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        [Display(Name = "Booking Date")]
        [DataType(DataType.DateTime)]
        public DateTime BookingDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Customer Name")]
        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Customer Email")]
        [EmailAddress]
        [StringLength(100)]
        public string CustomerEmail { get; set; } = string.Empty;

        [Display(Name = "Customer Phone")]
        [Phone]
        [StringLength(20)]
        public string? CustomerPhone { get; set; }

        [Display(Name = "Special Requests")]
        [StringLength(500)]
        public string? SpecialRequests { get; set; }

        // Foreign Keys
        [Required]
        [Display(Name = "Venue")]
        public int VenueId { get; set; }

        [Required]
        [Display(Name = "Event")]
        public int EventId { get; set; }

        // Navigation properties
        [ForeignKey("VenueId")]
        public Venue? Venue { get; set; }

        [ForeignKey("EventId")]
        public Event? Event { get; set; }
    }
}
