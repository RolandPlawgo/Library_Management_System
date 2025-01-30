using LibraryManagementSystem.Authentication;
using LibraryManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ApplicationUsersController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_userManager.Users.ToList());
		}

		public IActionResult Create()
		{
            var random = new Random();
            ViewData["RandomNumber"] = random.Next(999999);
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(ApplicationUser user, string role)
		{
            user.UserName = (role == "administrator" ? "A" : "U") + user.UserName;
            //var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            //await _userManager.ResetPasswordAsync(user, token, user.DateOfBirth.Date.ToShortDateString().Replace(".", ""));

            var password = user.DateOfBirth.Date.ToShortDateString().Replace(".", "");
            //var password = user.DateOfBirth.Date.ToShortDateString();
            await _userManager.CreateAsync(user, password);

            if (role == "administrator")
            {
                await _userManager.AddToRoleAsync(user, "Administrator");
            }

			_context.UserAccounts.Add(new Models.UserAccountModel() { Id = user.Id, BorrowedBooks = new List<Models.BookModel>(), ReservedBooks = new List<Models.BookModel>() });
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            ApplicationUser? user = _userManager.Users.Where(u => u.Id == id).FirstOrDefault();
            if (user is null)
            {
                return NotFound();
            }
            await _userManager.DeleteAsync(user);

            var userAccount = _context.UserAccounts.Find(id);
            if (userAccount is null)
            {
                return NotFound();
            }
            _context.UserAccounts.Remove(userAccount);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
