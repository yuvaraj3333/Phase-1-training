using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private AppDbContext appDbContext { get; set; }


        public HomeController(ILogger<HomeController> logger,AppDbContext appDbContext)
        {
            _logger = logger;
            this.appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddStudent(student stud)
        {
            appDbContext.students.Add(stud);
            appDbContext.SaveChanges();
            return RedirectToAction("Student");
        }


        public IActionResult Student()
        {

            var allstudents=appDbContext.students.ToList();
            var totalAge = allstudents.Sum(x => x.Age);
            ViewBag.totalAge = totalAge;
            return View(allstudents);
        }

        [HttpPost]
        public IActionResult UpdateAction(int id, string field, string value)
        {
            var student = appDbContext.students.FirstOrDefault(s => s.StudentID == id);
            if (student == null) return NotFound();

            switch (field)
            {
                case "FirstName":
                    student.FirstName = value;
                    break;
                case "LastName":
                    student.LastName = value;
                    break;
                case "Age":
                    if (int.TryParse(value, out int age))
                        student.Age = age;
                    break;
                case "Gender":
                    student.Gender = value;
                    break;
                case "Department":
                    student.Department = value;
                    break;
                default:
                    return BadRequest("Invalid field");
            }

            appDbContext.SaveChanges();
            return RedirectToAction("Student");
        }


        public IActionResult Product()
        {

            var allprod = appDbContext.prod.ToList();
            return View(allprod);
        }

        public IActionResult AddProd(Product prod1)
        {
            appDbContext.prod.Add(prod1);
            appDbContext.SaveChanges();
            return RedirectToAction("Product");
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Createoreditstudent()
        {
            return View();
        }

        public IActionResult CreateOrEditProd()
        {
            return View();
        }

        public IActionResult DeleteAction(int id)
        {
            var stud = appDbContext.students.FirstOrDefault(a => a.StudentID == id);
           
            if (stud != null)
            {
                _logger.LogWarning($"{stud.FirstName} is being deleted");
                appDbContext.students.Remove(stud);
                appDbContext.SaveChanges();
            }
            return RedirectToAction("Student");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
