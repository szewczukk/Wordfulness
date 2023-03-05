using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wordfulness.Data;
using Wordfulness.Models;
using Wordfulness.ViewModels;

namespace Wordfulness.Controllers
{
	public class LessonsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public LessonsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Lessons
		public async Task<IActionResult> Index()
		{
			var applicationDbContext = _context.Lessons.Include(l => l.Course);
			return View(await applicationDbContext.ToListAsync());
		}

		// GET: Lessons/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Lessons == null)
			{
				return NotFound();
			}

			var lesson = await _context.Lessons
				.Include(l => l.Course)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (lesson == null)
			{
				return NotFound();
			}

			return View(lesson);
		}

		// GET: Lessons/Create
		public IActionResult Create()
		{
			ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
			return View();
		}

		// POST: Lessons/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateLessonViewModel lesson)
		{
			if (ModelState.IsValid)
			{
				_context.Lessons.Add(new Lesson { Name = lesson.Name, CourseId = lesson.CourseId });
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", lesson.CourseId);
			return View(lesson);
		}

		// GET: Lessons/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Lessons == null)
			{
				return NotFound();
			}

			var lesson = await _context.Lessons.FindAsync(id);
			if (lesson == null)
			{
				return NotFound();
			}
			ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", lesson.CourseId);
			return View(lesson);
		}

		// POST: Lessons/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Lesson lesson)
		{
			if (id != lesson.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(lesson);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!LessonExists(lesson.Id))
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
			ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", lesson.CourseId);
			return View(lesson);
		}

		// GET: Lessons/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Lessons == null)
			{
				return NotFound();
			}

			var lesson = await _context.Lessons
				.Include(l => l.Course)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (lesson == null)
			{
				return NotFound();
			}

			return View(lesson);
		}

		// POST: Lessons/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Lessons == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Lesson'  is null.");
			}
			var lesson = await _context.Lessons.FindAsync(id);
			if (lesson != null)
			{
				_context.Lessons.Remove(lesson);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool LessonExists(int id)
		{
			return _context.Lessons.Any(e => e.Id == id);
		}
	}
}
