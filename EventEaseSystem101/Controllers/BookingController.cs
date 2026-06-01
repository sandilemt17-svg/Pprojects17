using EventEaseSystem.Data;
using EventEaseSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventEaseSystem.ViewModels;


namespace EventEaseSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index(string searchString, DateTime? searchDate)
        {
            var bookings = _context.Bookings
                .Include(b => b.Venue)
                .Include(b => b.Event)
                .AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                bookings = bookings.Where(b => b.CustomerName.Contains(searchString)
                                    || b.CustomerEmail.Contains(searchString)
                                    || b.Venue.VenueName.Contains(searchString)
                                    || b.Event.EventName.Contains(searchString));
            }

            if (searchDate.HasValue)
            {
                bookings = bookings.Where(b => b.BookingDate.Date == searchDate.Value.Date);
            }

            // Order by date descending
            bookings = bookings.OrderByDescending(b => b.BookingDate);

            return View(await bookings.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Venue)
                .Include(b => b.Event)
                .FirstOrDefaultAsync(m => m.BookingId == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName");
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventName");
            return View();
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingId,BookingDate,CustomerName,CustomerEmail,CustomerPhone,SpecialRequests,VenueId,EventId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                // Checking for double booking
                if (IsVenueAlreadyBooked(booking.VenueId, booking.BookingDate))
                {
                    ModelState.AddModelError("", "This venue is already booked on the selected date. Please choose another date or venue.");

                    
                    ViewBag.VenueId = new SelectList(_context.Venues, "VenueId", "VenueName", booking.VenueId);
                    ViewBag.EventId = new SelectList(_context.Events, "EventId", "EventName", booking.EventId);
                    return View(booking);
                }

                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.VenueId = new SelectList(_context.Venues, "VenueId", "VenueName", booking.VenueId);
            ViewBag.EventId = new SelectList(_context.Events, "EventId", "EventName", booking.EventId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName", booking.VenueId);
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventName", booking.EventId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,BookingDate,CustomerName,CustomerEmail,CustomerPhone,SpecialRequests,VenueId,EventId")] Booking booking)
        {
            if (id != booking.BookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Check for double booking (exclude current booking)
                    if (IsVenueAlreadyBooked(booking.VenueId, booking.BookingDate, booking.BookingId))
                    {
                        ModelState.AddModelError("", "This venue is already booked on the selected date. Please choose another date or venue.");

                        ViewBag.VenueId = new SelectList(_context.Venues, "VenueId", "VenueName", booking.VenueId);
                        ViewBag.EventId = new SelectList(_context.Events, "EventId", "EventName", booking.EventId);
                        return View(booking);
                    }

                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingId))
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

            ViewBag.VenueId = new SelectList(_context.Venues, "VenueId", "VenueName", booking.VenueId);
            ViewBag.EventId = new SelectList(_context.Events, "EventId", "EventName", booking.EventId);
            return View(booking);
        }


        private bool IsVenueAlreadyBooked(int venueId, DateTime bookingDate, int? excludeBookingId = null)
        {
            return _context.Bookings.Any(b =>
                b.VenueId == venueId &&
                b.BookingDate.Date == bookingDate.Date &&
                (excludeBookingId == null || b.BookingId != excludeBookingId));
        }


        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Venue)
                .Include(b => b.Event)
                .FirstOrDefaultAsync(m => m.BookingId == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ConsolidatedBookings(string searchTerm)
        {
            var bookings = from b in _context.Bookings
                           join v in _context.Venues on b.VenueId equals v.VenueId
                           join e in _context.Events on b.EventId equals e.EventId
                           select new EventEaseSystem.ViewModels.BookingViewModel
                           {
                               BookingId = b.BookingId,
                               VenueName = v.VenueName,
                               EventName = e.EventName,
                               BookingDate = b.BookingDate,
                               CustomerName = b.CustomerName,
                               CustomerEmail = b.CustomerEmail,
                               CustomerPhone = b.CustomerPhone,
                               SpecialRequests = b.SpecialRequests
                           };

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bookings = bookings.Where(b =>
                    b.BookingId.ToString().Contains(searchTerm) ||
                    b.EventName.Contains(searchTerm));
            }

            ViewBag.SearchTerm = searchTerm;
            return View(await bookings.ToListAsync());
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }
    }
}
