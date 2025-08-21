using API_Demo.Context;
using API_Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Demo.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDBContext _dbContext;

        public BookController(AppDBContext context)
        {
            _dbContext = context;
        }

        [HttpGet("GetBooks")]
        public IEnumerable<BookModel> GetBooks()
        {
            return _dbContext.BookDB.AsNoTracking()
            .Select(b => new BookModel
            {
                Title = b.Title,
                Description = b.Description,
                Rating = Math.Round(b.Rating, 1),
                Price = b.Price
            })
            .ToList();
        }

        [HttpPost("AddBook")]
        public IActionResult AddBook([FromBody] BookModel book)
        {
            _dbContext.BookDB.Add(book);
            _dbContext.SaveChanges();
            return Ok(book);
        }

        [HttpDelete("DeleteBook/{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _dbContext.BookDB.Find(id);

            if (book == null)
            {
                return NotFound($"Book with Id {id} not found.");
            }

            _dbContext.BookDB.Remove(book);
            _dbContext.SaveChanges();

            return Ok($"Book with Id {id} deleted successfully.");
        }

        [HttpGet("GetBooksOrderByPrice")]
        public IActionResult GetBooksOrderByPrice([FromQuery] bool sort = true)
        {
            var books = _dbContext.BookDB.AsNoTracking().ToList();

            var sortedBooks = sort
                ? books.OrderBy(b => b.Price).ToList()
                : books.OrderByDescending(b => b.Price).ToList();

            return Ok(sortedBooks);
        }

        [HttpGet("GetTop5Books")]
        public IActionResult GetTop5Books()
        {
            var books = _dbContext.BookDB.AsNoTracking()
                .ToList()
                .OrderByDescending(b => b.Price)
                .Take(5)
                .ToList();

            return Ok(books);
        }

        [HttpGet("GetCostDetails")]
        public IActionResult GetCostDetails()
        {
            var books = _dbContext.BookDB.AsNoTracking().ToList();

            if (!books.Any())
            {
                return Ok(new { TotalPrice = 0m, AveragePrice = 0m });
            }

            var total = books.Sum(b => b.Price);
            var average = books.Average(b => b.Price);

            return Ok(new
            {
                TotalPrice = total,
                AveragePrice = Math.Round(average, 2)
            });
        }



    }
}
