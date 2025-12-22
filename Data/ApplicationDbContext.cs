using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
		public DbSet<Book> Books { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<ReviewVote> ReviewVotes { get; set; }
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			// LookThisAgain
			// Απενεργοποίηση του Cascade Delete για τα ReviewVotes 
			// ώστε να μην έχουμε πολλαπλά μονοπάτια διαγραφής
			modelBuilder.Entity<ReviewVote>()
				.HasOne(rv => rv.Review)
				.WithMany(r => r.Votes)
				.HasForeignKey(rv => rv.ReviewId)
				.OnDelete(DeleteBehavior.Restrict); // Αλλάζουμε το Cascade σε Restrict

			modelBuilder.Entity<ReviewVote>()
				.HasOne(rv => rv.User)
				.WithMany()
				.HasForeignKey(rv => rv.UserId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
