namespace LibraryManagementSystem.ViewModels
{
	public class BookIndexViewModel
	{
		public IEnumerable<LibraryManagementSystem.Models.Book> Books { get; set; }

		public string? GenreFilter { get; set; }
		public int? YearFilter { get; set; }

		public List<string>? Genres { get; set; }

		public int? RatingFilter { get; set; }
	}
}
