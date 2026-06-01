using EventEaseSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventEaseSystem.Data;
using EventEaseSystem.Models;

namespace EventEaseSystem.Controllers
{
    public class VenuesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly BlobService _blobService;

        public VenuesController(ApplicationDbContext context, BlobService blobService)
        {
            _context = context;
            _blobService = blobService;
        }

        // GET: Venues
        public async Task<IActionResult> Index(string searchString)
        {
            var venues = from v in _context.Venues
                         select v;

            if (!String.IsNullOrEmpty(searchString))
            {
                venues = venues.Where(v => v.VenueName.Contains(searchString)
                                    || v.Location.Contains(searchString));
            }

            return View(await venues.ToListAsync());
        }

        // GET: Venues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venues
                .Include(v => v.Bookings)
                .ThenInclude(b => b.Event)
                .FirstOrDefaultAsync(m => m.VenueId == id);

            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        // GET: Venues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Venues/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Venue venue, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Upload image to Azurite
                    venue.ImageUrl = await _blobService.UploadImageAsync(imageFile);
                }
                else
                {
                    // Use placeholder if no image uploaded
                    venue.ImageUrl = "/images/placeholder-venue.jpg";
                }

                _context.Add(venue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }

        // GET: Venues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venues.FindAsync(id);
            if (venue == null)
            {
                return NotFound();
            }
            return View(venue);
        }

        // POST: Venues/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Venue venue, IFormFile imageFile)
        {
            if (id != venue.VenueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        venue.ImageUrl = await _blobService.UploadImageAsync(imageFile);
                    }

                    _context.Update(venue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenueExists(venue.VenueId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }

        // GET: Venues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venues
                .Include(v => v.Bookings)
                .FirstOrDefaultAsync(m => m.VenueId == id);

            if (venue == null)
            {
                return NotFound();
            }

            // Check if venue has bookings
            if (venue.Bookings != null && venue.Bookings.Any())
            {
                TempData["Error"] = "Cannot delete venue because it has associated bookings.";
                return RedirectToAction(nameof(Index));
            }

            return View(venue);
        }

        // POST: Venues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venue = await _context.Venues
        .Include(v => v.Bookings)
        .FirstOrDefaultAsync(v => v.VenueId == id);

            if (venue == null)
            {
                return NotFound();
            }

            // Check if venue has any active or future bookings
            bool hasActiveBookings = venue.Bookings.Any(b => b.BookingDate > DateTime.Now);

            if (hasActiveBookings)
            {
                TempData["ErrorMessage"] = "Cannot delete this venue because it has active or future bookings.";
                return RedirectToAction(nameof(Index));
            }

            _context.Venues.Remove(venue);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Venue deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
        

        private bool VenueExists(int id)
        {
            return _context.Venues.Any(e => e.VenueId == id);
        }
    }
}

