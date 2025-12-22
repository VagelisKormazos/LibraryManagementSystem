using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
	public class Review
	{
		public int Id { get; set; } // 
		[Required]
		public string Content { get; set; } // 
		[Range(1, 5)]
		public int Rating { get; set; } // 
		public DateTime DateCreated { get; set; } = DateTime.Now; // 

		// Foreign Keys
		public int BookId { get; set; } // 
		public virtual Book? Book { get; set; }

		public string UserId { get; set; } // 
		public virtual IdentityUser? User { get; set; }

		public virtual ICollection<ReviewVote> Votes { get; set; } = new List<ReviewVote>();
	}
}
