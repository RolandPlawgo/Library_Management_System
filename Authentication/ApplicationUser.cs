using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystem.Authentication
{
	public class ApplicationUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }
		public DateTime DateOfBirth { get; set; }
		//public string LibraryCardNumber { get; set; }
	}
}
