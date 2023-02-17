using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wordfulness.Data;
using Wordfulness.Models;

namespace Wordfulness.Controllers
{
	public class CoursesController : Controller
	{
		private readonly ApplicationDbContext _context;

		public CoursesController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			List<Course> courses = _context.Courses.ToList();

			return View(new CoursesIndexViewModel { Courses = courses });
		}

		public async Task<IActionResult> Details(int? id)
		{
			Course? course = await _context.Courses.FirstOrDefaultAsync(course => course.Id == id);

			if (course == null)
			{
				return NotFound();
			}

			return View(new CoursesDetailsViewModel { Course = course });
		}

		public async Task<IActionResult> Delete(int? id)
		{
			Course? course = await _context.Courses.FirstOrDefaultAsync(course => course.Id == id);

			if (course == null)
			{
				return NotFound();
			}

			return View(course);
		}

		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			Course? course = await _context.Courses.FindAsync(id);

			if (course == null)
			{
				return NotFound();
			}

			_context.Courses.Remove(course);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Course course)
		{
			if (!ModelState.IsValid)
			{
				return View(course);
			}

			_context.Courses.Add(course);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Edit(int? id)
		{
			Course? course = await _context.Courses.FirstOrDefaultAsync(course => course.Id == id);

			if (course == null)
			{
				return NotFound();
			}

			return View(course);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Course course)
		{
			if (!ModelState.IsValid)
			{
				return View(course);
			}

			_context.Courses.Update(course);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Details), new { id = course.Id });
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}