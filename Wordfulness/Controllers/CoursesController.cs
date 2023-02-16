using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
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
			var courses = _context.Courses.ToList();
			return View(new CoursesIndexViewModel { Courses = courses });
		}

		public async Task<IActionResult> Details(int? id)
		{
			var course = await _context.Courses.FirstOrDefaultAsync(course => course.Id == id);

			if (course == null)
			{
				return NotFound();
			}

			return View(new CoursesDetailsViewModel { Course = course });
		}

		public async Task<IActionResult> Delete(int? id)
		{
			var course = await _context.Courses.FirstOrDefaultAsync(course => course.Id == id);

			if (course == null)
			{
				return NotFound();
			}

			return View(course);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var course = await _context.Courses.FindAsync(id);

			if (course == null)
			{
				return NotFound();
			}

			 _context.Courses.Remove(course);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}