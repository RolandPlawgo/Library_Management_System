using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models
{
	public class UserAccountModel
	{
		public string Id { get; set; } = null!;
		[InverseProperty("UserBorrowing")]
		public List<BookModel> BorrowedBooks { get; set; } = new List<BookModel>();
		[InverseProperty("UserReserving")]
		public List<BookModel> ReservedBooks { get; set; } = new List<BookModel>();
	}
}
