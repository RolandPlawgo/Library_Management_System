using LibraryManagementSystem.Authentication;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace LibraryManagementSystem.Controllers
{
	[Authorize]
	public class UserAccountsController : Controller
	{
		private readonly ILogger<UserAccountsController> _logger;
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		public UserAccountsController(ILogger<UserAccountsController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_logger = logger;
			_context = context;
			_userManager = userManager;
		}

		//public IActionResult Index()
		//{
		//	return View();
		//}

		//public IActionResult Details(/*string id*/)
		//{
		//	ViewData["user"] = _userManager.GetUserAsync(User).Result;
		//	//if (_userManager.GetUserId(User) == id)
		//	//UserAccountModel user = _context.UserAccounts.Include(i => i.ReservedBooks).Include(i => i.BorrowedBooks).Where(u => u.Id == id).First();
		//	UserAccountModel user = _context.UserAccounts.Include(i => i.ReservedBooks).Include(i => i.BorrowedBooks).Where(u => u.Id == _userManager.GetUserId(User)).FirstOrDefault();
		//	return View(user);
		//}

		//[Authorize(Roles = "Administrator")]
		public IActionResult Details(string id)
		{
			//ViewData["user"] = _userManager.GetUserAsync(User).Result;
			//if (_userManager.GetUserId(User) == id)
			//UserAccountModel user = _context.UserAccounts.Include(i => i.ReservedBooks).Include(i => i.BorrowedBooks).Where(u => u.Id == id).First();

			if (_userManager.GetUserId(User) == id || User.IsInRole("Administrator"))
			{
				UserAccountModel user = _context.UserAccounts.Include(i => i.ReservedBooks).Include(i => i.BorrowedBooks).Where(u => u.Id == id).FirstOrDefault();
				return View(user);
			}
			else
			{
				return Forbid();
			}
		}

		[HttpPost]
		public IActionResult ReserveBook(int id)
		{
			BookModel? book = _context.Books.Find(id);
			if (book == null)
			{
				return NotFound();
			}
			if (book.Availability)
			{
				book.UserReservingId = _userManager.GetUserId(User);
				book.ReservationDueDate = DateTime.Now.Date.AddDays(14);
				book.Availability = false;
				_context.Books.Update(book);
				_context.SaveChanges();
			}
			return RedirectToAction("Details", "Books", new {id = id});
		}

		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public IActionResult BorrowBook(int bookId, string libraryCardNumber)
		{
			ApplicationUser? user = _userManager.Users.Where(u => u.UserName == libraryCardNumber).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            UserAccountModel? account = _context.UserAccounts.Find(user.Id);
			if (account == null)
			{
				return NotFound();
			}
			BookModel? book = _context.Books.Find(bookId);
			if (book == null)
			{
				return NotFound();
			}

			if (book.Availability || book.UserReservingId == user.Id)
			{
				book.UserBorrowingId = user.Id;
				book.UserReservingId = null;
				book.BorrowingDueDate = DateTime.Now.Date.AddDays(30);
				book.ReservationDueDate = null;

				book.Availability = false;
				_context.Books.Update(book);
				_context.SaveChanges();

				return RedirectToAction(actionName: "Index", controllerName: "Home");
			}
			else
			{
				return BadRequest();
			}
		}

		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public IActionResult ReturnBook(int bookId, string libraryCardNumber)
		{
			ApplicationUser? user = _userManager.Users.Where(u => u.UserName == libraryCardNumber).FirstOrDefault();
			if (user == null)
			{
				return NotFound();
			}
			UserAccountModel? account = _context.UserAccounts.Find(user.Id);
			if (account == null)
			{
				return NotFound();
			}
			BookModel? book = _context.Books.Find(bookId);
			if (book == null)
			{
				return NotFound();
			}

			if (account.BorrowedBooks.Contains(book))
			{
				//account.BorrowedBooks.Remove(book);
				//_context.UserAccounts.Update(account);
				book.UserBorrowingId = null;
				book.BorrowingDueDate = null;
				book.Availability = true;
				_context.Books.Update(book);
				_context.SaveChanges();

				return RedirectToAction(actionName: "Index", controllerName: "Home");
			}
			else
			{
				return BadRequest();
			}
		}
	}
}
