using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LibraryManagementSystem.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        [AllowNull]
        public string Description { get; set; } = null!;
        public string Publisher { get; set; } = null!;
        public bool Availability { get; set; }
        [AllowNull]
        public byte[]? Image { get; set; }

		public string? UserReservingId { get; set; }
		[ForeignKey("UserReservingId")]
		public UserAccountModel UserReserving { get; set; } = null!;
        public string? UserBorrowingId { get; set; }
		[ForeignKey("UserBorrowingId")]
        public UserAccountModel  UserBorrowing { get; set; } = null!;

        [AllowNull]
        public DateTime? ReservationDueDate { get; set; }
        [AllowNull]
        public DateTime? BorrowingDueDate { get; set; }
	}
}
