namespace LibraryManagementSystem.ViewModels
{
	public class BookIndexViewModel
	{
		// Η λίστα των βιβλίων που θα εμφανίζεται
		public IEnumerable<LibraryManagementSystem.Models.Book> Books { get; set; }

		// Πεδία για τα φίλτρα αναζήτησης
		public string? GenreFilter { get; set; }
		public int? YearFilter { get; set; }

		// Λίστα για το dropdown των ειδών (προαιρετικά)
		public List<string>? Genres { get; set; }
	}
}
