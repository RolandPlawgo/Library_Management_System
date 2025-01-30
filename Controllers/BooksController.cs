using LibraryManagementSystem.Authentication;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Razor;

namespace LibraryManagementSystem.Controllers
{
	public class BooksController : Controller
	{
		private readonly ILogger<BooksController> _logger;
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		public BooksController(ILogger<BooksController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_logger = logger;
			_context = context;
			_userManager = userManager;
		}

		[HttpGet]
		public IActionResult Details (int id)
		{
			BookModel? bookModel = _context?.Books.Find(id);

			if (bookModel is null)
			{
				return NotFound();
			}

			if (bookModel.UserBorrowingId != null)
			{
				ViewData["UserBorrowing"] = _userManager.Users.Where(u => u.Id == bookModel.UserBorrowingId).FirstOrDefault()?.UserName;
			}
			else
			{
				ViewData["UserBorrowing"] = "";
			}
			if (bookModel.UserReservingId != null)
			{
				ViewData["UserReserving"] = _userManager.Users.Where(u => u.Id == bookModel.UserReservingId).FirstOrDefault()?.UserName;
			}
			else
			{
				ViewData["UserReserving"] = "";
			}


			return View(bookModel);
		}

		[Authorize(Roles = "Administrator")]
		[HttpGet]
		public IActionResult Create ()
		{
			return View();
		}

		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public IActionResult Create (BookModel book, IFormFile image)
		{
			string fileName = Path.GetFileName(image.FileName);
			using (MemoryStream ms = new MemoryStream())
			{
				image.CopyTo(ms);
				book.Image = ms.ToArray();
			}
			_context.Books.Add(book);
			_context.SaveChanges();

			return RedirectToAction("Details", new {id = book.Id});
		}

		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public IActionResult Delete (int id)
		{
			BookModel? book = _context.Books.Find(id);
			if (book is null)
			{
				return NotFound();
			}
			_context.Books.Remove(book);
			_context.SaveChanges();

			return RedirectToAction("Index", "Home");
		}

		[Authorize(Roles = "Administrator")]
		[HttpGet]
		public IActionResult Edit (int id)
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


		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public IActionResult Edit (BookModel book, IFormFile image)
		{
			BookModel? bookModel = _context.Books.Find(book.Id);
			if (bookModel is null)
			{
				return BadRequest();
			}
			//BookModel bookModel = new BookModel()
			//{
			bookModel.Id = book.Id;
			bookModel.Title = book.Title;
			bookModel.Author = book.Author;
			bookModel.Publisher = book.Publisher;
			bookModel.Description = book.Description;
			bookModel.Availability = book.Availability;
			//};
			if (image != null)
			{
				string fileName = Path.GetFileName(image.FileName);
				using (MemoryStream ms = new MemoryStream())
				{
					image.CopyTo(ms);
                    //book.Image = ms.ToArray();
                    bookModel.Image = ms.ToArray();
                }
			}
			//else
			//{
			//	book.Image = _context.Books.Find(book.Id)?.Image;
			//}
			_context.Books.Update(bookModel);
			_context.SaveChanges();

			return RedirectToAction("Details", new { id = book.Id });
		}

		[Authorize(Roles = "Administrator")]
		public IActionResult Borrow (int id)
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

		[Authorize(Roles = "Administrator")]
		public IActionResult Return (int id)
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
