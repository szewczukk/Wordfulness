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
		private readonly ILogger _logger;

		public CoursesController(ApplicationDbContext context, ILogger<CoursesController> logger)
		{
			_context = context;
			_logger = logger;
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

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}