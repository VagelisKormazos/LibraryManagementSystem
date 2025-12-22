using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.ViewModels
{
	public class ReviewCreateViewModel
	{
		public int BookId { get; set; }
		public string? BookTitle { get; set; }

		[Required(ErrorMessage = "Content is required")]
		[StringLength(500, MinimumLength = 10, ErrorMessage = "Content must be between 10 and 500 characters")]
		public string Content { get; set; }

		[Required]
		[Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
		public int Rating { get; set; }
	}
}
