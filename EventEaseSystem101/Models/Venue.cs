using System.ComponentModel.DataAnnotations;

namespace EventEaseSystem.Models
{
    public class Venue
    {
        [Key]
        public int VenueId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Venue Name")]
        public string VenueName { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Location { get; set; } = string.Empty;

        [Required]
        [Range(1, 10000)]
        [Display(Name = "Capacity")]
        public int Capacity { get; set; }

        [Display(Name = "Image URL")]
        [StringLength(500)]
        public string? ImageUrl { get; set; }

        // Navigation property
        public ICollection<Booking>? Bookings { get; set; }

    }
}
