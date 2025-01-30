using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;

namespace LibraryManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly BookAvailability _availability;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, BookAvailability availability)
        {
            _logger = logger;
            _context = context;
            _availability = availability;
        }

        public IActionResult Index(string? searchString = null)
        {
            ViewData["searchString"] = searchString;

            List<BookModel> books = new List<BookModel>();

            if (searchString is not null)
            {
                books = _context.Books.Where(b => b.Title.StartsWith(searchString) || b.Author.StartsWith(searchString)).OrderBy(b => b.Title).ToList();
                books.AddRange(_context.Books.Where(b => (b.Author.Contains(searchString) && !b.Author.StartsWith(searchString)) 
                                                        || (b.Title.Contains(searchString) && !b.Title.StartsWith(searchString))).OrderBy(b => b.Title).ToList());
            }
            else
            {
                books = _context.Books.OrderBy(b => b.Title).ToList();
            }
            return View(books);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
