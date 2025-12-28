using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BooksController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Books
        public async Task<IActionResult> Index(string genreFilter, int? yearFilter, int? ratingFilter)
        {

            var booksQuery = _context.Books.Include(b => b.Reviews).AsQueryable();

			if (!string.IsNullOrEmpty(genreFilter))
            {
                booksQuery = booksQuery.Where(b => b.Genre == genreFilter);
            }

            if (yearFilter.HasValue)
            {
                booksQuery = booksQuery.Where(b => b.PublishedYear == yearFilter);
            }

			if (ratingFilter.HasValue)
			{
				booksQuery = booksQuery.Where(b => b.Reviews.Any() && b.Reviews.Average(r => r.Rating) >= ratingFilter);
			}

			var viewModel = new BookIndexViewModel
			{
				Books = await booksQuery.ToListAsync(),
				GenreFilter = genreFilter,
				YearFilter = yearFilter,
				RatingFilter = ratingFilter, 
				Genres = await _context.Books.Select(b => b.Genre).Distinct().ToListAsync()
			};

			return View(viewModel);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Reviews)
                    .ThenInclude(r => r.User)
                .Include(b => b.Reviews)
                    .ThenInclude(r => r.ReviewVotes) 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,PublishedYear,Genre")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,PublishedYear,Genre")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Vote(int reviewId, bool isUpvote)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Challenge();

            var existingVote = await _context.ReviewVotes
                .FirstOrDefaultAsync(v => v.ReviewId == reviewId && v.UserId == userId);

            if (existingVote != null)
            {
                existingVote.IsUpvote = isUpvote;
            }
            else
            {
                var vote = new ReviewVote
                {
                    ReviewId = reviewId,
                    UserId = userId,
                    IsUpvote = isUpvote
                };
                _context.ReviewVotes.Add(vote);
            }

            await _context.SaveChangesAsync();

            var review = await _context.Reviews.FindAsync(reviewId);
            return RedirectToAction(nameof(Details), new { id = review.BookId });
        }

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddReview(ReviewCreateViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.GetUserAsync(User);
				if (user == null) return Challenge();

				var review = new Review
				{
					BookId = model.BookId,
					Content = model.Content,
					Rating = model.Rating,
					UserId = user.Id,
					DateCreated = DateTime.Now
				};

				_context.Reviews.Add(review);
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Details), new { id = model.BookId });
			}

			return RedirectToAction(nameof(Details), new { id = model.BookId });
		}

	}
}
