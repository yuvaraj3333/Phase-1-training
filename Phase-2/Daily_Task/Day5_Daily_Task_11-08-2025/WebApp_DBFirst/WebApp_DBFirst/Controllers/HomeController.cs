using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp_DBFirst.Models;
using WebApp_DBFirst.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WebApp_DBFirst.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly VishnunDbContext _context;

        public HomeController(ILogger<HomeController> logger, VishnunDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products); 
        }

        public async Task<IActionResult> Data()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
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