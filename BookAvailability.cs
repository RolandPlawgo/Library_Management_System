using LibraryManagementSystem.Data;

namespace LibraryManagementSystem
{
	public class BookAvailability
	{
		public readonly ApplicationDbContext _context;
		public BookAvailability(ApplicationDbContext context)
		{
			_context = context;
		}

		public void CheckAvailability()
		{
			foreach (var book in _context.Books)
			{
				if (book.ReservationDueDate is not null)
				{
					if (book.ReservationDueDate < DateTime.Now.Date)
					{
						book.ReservationDueDate = null;
						book.UserReservingId = null;
						book.Availability = true;
						_context.Books.Update(book);
						_context.SaveChanges();
					}
				}
			}
		}
	}
}
