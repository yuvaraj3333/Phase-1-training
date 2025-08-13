using Microsoft.AspNetCore.Mvc;
using MovieBookingApp.Models;
using System;
using System.Linq;

namespace MovieBookingApp.Controllers
{
    public class MovieController : Controller
    {
        private readonly AppDbContext _context;

        public MovieController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.MovieName = "Coolie";
            ViewBag.Description = "Coolie is a thrilling movie featuring adventure, comedy, and action!";
            return View();
        }

        [HttpGet]
        public IActionResult BookTicket()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BookTicket(Audience audience)
        {
            if (ModelState.IsValid)
            {
                // Calculate Age from DOB if not provided
                if (audience.DOB != default(DateTime))
                {
                    audience.Age = DateTime.Now.Year - audience.DOB.Year;
                    if (DateTime.Now.DayOfYear < audience.DOB.DayOfYear)
                        audience.Age--;
                }

                _context.Audiences.Add(audience);
                _context.SaveChanges();
                return RedirectToAction("AudienceList");
            }
            return View(audience);
        }

        public IActionResult AudienceList()
        {
            var audienceList = _context.Audiences.ToList();

            ViewBag.TotalAudience = audienceList.Count;
            ViewBag.AverageAge = audienceList.Count > 0 ? Math.Round(audienceList.Average(a => a.Age), 1) : 0;

            return View(audienceList);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var audience = _context.Audiences.FirstOrDefault(a => a.Id == id);
            if (audience != null)
            {
                _context.Audiences.Remove(audience);
                _context.SaveChanges();
            }
            return RedirectToAction("AudienceList");
        }
    }
}
