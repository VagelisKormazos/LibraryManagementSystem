using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
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
    }
}
