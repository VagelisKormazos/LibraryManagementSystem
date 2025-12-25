using System.ComponentModel.DataAnnotations;
namespace LibraryManagementSystem.Models
{
	public class Book
	{
		public int Id { get; set; } 
		[Required]
		public string Title { get; set; } 
		[Required]
		public string Author { get; set; } 
		public int PublishedYear { get; set; } 
		public string Genre { get; set; } 

		public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

		//UnitTest
		public double AverageRating => Reviews != null && Reviews.Any()
		? Reviews.Average(r => r.Rating)
		: 0;
	}
}
