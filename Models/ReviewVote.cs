using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystem.Models
{
	public class ReviewVote
	{
		public int Id { get; set; } // [cite: 18]
		public int ReviewId { get; set; } // [cite: 18]
		public virtual Review? Review { get; set; }

		public string UserId { get; set; } // [cite: 18]
		public virtual IdentityUser? User { get; set; }

		public bool IsUpvote { get; set; } // [cite: 18]
	}
}
