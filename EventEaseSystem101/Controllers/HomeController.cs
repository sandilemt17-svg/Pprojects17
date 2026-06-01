using EventEaseSystem.Data;
using EventEaseSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EventEaseSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context; 
        }

        public async Task<IActionResult> Index()
        {
            var stats = new
            {
                TotalVenues = await _context.Venues.CountAsync(),
                TotalEvents = await _context.Events.CountAsync(),
                TotalBookings = await _context.Bookings.CountAsync(),
                UpcomingBookings = await _context.Bookings
                    .Where(b => b.BookingDate >= DateTime.Today)
                    .CountAsync()
            };

            ViewBag.Stats = stats;

            var recentBookings = await _context.Bookings
                .Include(b => b.Venue)
                .Include(b => b.Event)
                .OrderByDescending(b => b.BookingDate)
                .Take(5)
                .ToListAsync();

            return View(recentBookings);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
