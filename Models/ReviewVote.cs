using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystem.Models
{
	public class ReviewVote
	{
		public int Id { get; set; } 
		public int ReviewId { get; set; } 
		public virtual Review? Review { get; set; }

		public string UserId { get; set; } 
		public virtual IdentityUser? User { get; set; }

		public bool IsUpvote { get; set; } 
	}
}
