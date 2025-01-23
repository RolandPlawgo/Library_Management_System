using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
	public class BooksController : Controller
	{
		private readonly ILogger<BooksController> _logger;
		private readonly ApplicationDbContext _context;

		public BooksController(ILogger<BooksController> logger, ApplicationDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[Authorize]
		[HttpGet]
		public IActionResult Details (int id)
		{
			BookModel? bookModel = _context?.Books.Find(id);
			if (bookModel is not null)
			{
				return View(bookModel);
			}
			else
			{
				return NotFound();
			}
		}
	}
}
