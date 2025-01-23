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

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
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


        [HttpPost]
        public IActionResult UploadFile(IFormFile postedFile, string title, string author, string publisher, string description)
        {
            string fileName = Path.GetFileName(postedFile.FileName);
            //string contentType = postedFile.ContentType;
            using (MemoryStream ms = new MemoryStream())
            {
                postedFile.CopyTo(ms);
                BookModel book = new BookModel()
                {
                    Title = title,
                    Author = author,
                    Description = description,
                    Publisher = publisher,
                    Image = ms.ToArray(),
                    Availability = true
                };
                _context.Books.Add(book);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
