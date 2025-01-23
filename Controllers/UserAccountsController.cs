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

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Details(/*string id*/)
		{
			ViewData["user"] = _userManager.GetUserAsync(User).Result;
			//if (_userManager.GetUserId(User) == id)
			//UserAccountModel user = _context.UserAccounts.Include(i => i.ReservedBooks).Include(i => i.BorrowedBooks).Where(u => u.Id == id).First();
			UserAccountModel user = _context.UserAccounts.Include(i => i.ReservedBooks).Include(i => i.BorrowedBooks).Where(u => u.Id == _userManager.GetUserId(User)).First();
			return View(user);
		}

		[HttpPost]
		public IActionResult ReserveBook(int id)
		{
			BookModel? book = _context.Books.Find(id);
			if (book == null)
			{
				return NotFound();
			}
			if (book.UserReservingId == null)
			{
				_context.UserAccounts.Find(_userManager.GetUserId(User))?.ReservedBooks.Add(book);
				_context.Books.Find(book.Id)!.Availability = false;
				_context.SaveChanges();
			}
			return RedirectToAction("Index");
		}
	}
}
