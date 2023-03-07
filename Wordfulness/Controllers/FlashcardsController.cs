using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wordfulness.Data;
using Wordfulness.Models;
using Wordfulness.ViewModels;

namespace Wordfulness.Controllers
{
	[Authorize(Roles = "ADMIN")]
	public class FlashcardsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public FlashcardsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Flashcards
		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			return View(await _context.Flashcard.Include("Lesson").ToListAsync());
		}

		// GET: Flashcards/Details/5
		[AllowAnonymous]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Flashcard == null)
			{
				return NotFound();
			}

			var flashcard = await _context.Flashcard
				.Include(f => f.Lesson)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (flashcard == null)
			{
				return NotFound();
			}

			return View(flashcard);
		}

		// GET: Flashcards/Create
		public IActionResult Create()
		{
			ViewData["LessonId"] = new SelectList(_context.Lessons, "Id", "Name");
			return View();
		}

		// POST: Flashcards/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateFlashcardViewModel flashcard)
		{
			if (ModelState.IsValid)
			{
				_context.Flashcard.Add(new Flashcard
				{
					Front = flashcard.Front,
					Back = flashcard.Back,
					LessonId = flashcard.LessonId,
				});
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["LessonId"] = new SelectList(_context.Lessons, "Id", "Name", flashcard.LessonId);
			return View(flashcard);
		}

		// GET: Flashcards/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Flashcard == null)
			{
				return NotFound();
			}

			var flashcard = await _context.Flashcard.FindAsync(id);
			if (flashcard == null)
			{
				return NotFound();
			}
			return View(flashcard);
		}

		// POST: Flashcards/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Front,Back,LessonId")] Flashcard flashcard)
		{
			if (id != flashcard.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(flashcard);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!FlashcardExists(flashcard.Id))
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
			return View(flashcard);
		}

		// GET: Flashcards/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Flashcard == null)
			{
				return NotFound();
			}

			var flashcard = await _context.Flashcard
				.FirstOrDefaultAsync(m => m.Id == id);
			if (flashcard == null)
			{
				return NotFound();
			}

			return View(flashcard);
		}

		// POST: Flashcards/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Flashcard == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Flashcard'  is null.");
			}
			var flashcard = await _context.Flashcard.FindAsync(id);
			if (flashcard != null)
			{
				_context.Flashcard.Remove(flashcard);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool FlashcardExists(int id)
		{
			return _context.Flashcard.Any(e => e.Id == id);
		}
	}
}
