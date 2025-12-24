using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
namespace LibraryManagementSystem.Controllers;

public static class BookEndpoints
{
    public static void MapBookEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Book").WithTags(nameof(Book));

        group.MapGet("/", async (ApplicationDbContext db) =>
        {
            return await db.Books.ToListAsync();
        })
        .WithName("GetAllBooks");

        group.MapGet("/{id}", async Task<Results<Ok<Book>, NotFound>> (int id, ApplicationDbContext db) =>
        {
            return await db.Books.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Book model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetBookById");

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Book book, ApplicationDbContext db) =>
        {
            var affected = await db.Books
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, book.Id)
                    .SetProperty(m => m.Title, book.Title)
                    .SetProperty(m => m.Author, book.Author)
                    .SetProperty(m => m.PublishedYear, book.PublishedYear)
                    .SetProperty(m => m.Genre, book.Genre)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateBook");

        group.MapPost("/", async (Book book, ApplicationDbContext db) =>
        {
            db.Books.Add(book);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Book/{book.Id}",book);
        })
        .WithName("CreateBook");

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, ApplicationDbContext db) =>
        {
            var affected = await db.Books
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteBook");

		 
		group.MapGet("/{id}/reviews", async (int id, ApplicationDbContext db) =>
		{
			return await db.Reviews
				.Where(r => r.BookId == id)
				.Select(r => new { r.Id, r.Content, r.Rating, r.User.UserName })
				.ToListAsync();
		})
        .WithName("GetBookReviews")
        .WithOpenApi();

		var reviewGroup = routes.MapGroup("/api/reviews").WithTags("Reviews");

		reviewGroup.MapPost("/", async (Review review, ClaimsPrincipal user, ApplicationDbContext db) =>
		{
			var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
			if (userId == null) return Results.Challenge();

			review.UserId = userId;
			review.DateCreated = DateTime.Now;

			db.Reviews.Add(review);
			await db.SaveChangesAsync();
			return TypedResults.Created($"/api/reviews/{review.Id}", review);
		})
		.WithName("CreateReviewApi")
		.RequireAuthorization();

		reviewGroup.MapPost("/{id}/vote", async (int id, bool isUpvote, ClaimsPrincipal user, ApplicationDbContext db) =>
		{
			var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
			if (userId == null) return Results.Challenge();


			var existingVote = await db.ReviewVotes
				.FirstOrDefaultAsync(v => v.ReviewId == id && v.UserId == userId);

			if (existingVote != null)
			{
				existingVote.IsUpvote = isUpvote;
			}
			else
			{
				db.ReviewVotes.Add(new ReviewVote
				{
					ReviewId = id,
					UserId = userId,
					IsUpvote = isUpvote
				});
			}

			await db.SaveChangesAsync();
			return TypedResults.Ok();
		})
		.WithName("VoteReviewApi")
		.RequireAuthorization();
	}
}
