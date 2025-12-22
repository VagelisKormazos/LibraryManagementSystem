using System.ComponentModel.DataAnnotations;
namespace LibraryManagementSystem.Models
{
	public class Book
	{
		public int Id { get; set; } // [cite: 16]
		[Required]
		public string Title { get; set; } // [cite: 16]
		[Required]
		public string Author { get; set; } // [cite: 16]
		public int PublishedYear { get; set; } // [cite: 16]
		public string Genre { get; set; } // [cite: 16]

		public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
	}
}
