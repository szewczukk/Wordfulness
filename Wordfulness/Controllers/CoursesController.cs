using Microsoft.AspNetCore.Mvc;
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
			return View(new AllCoursesViewModel { Courses = courses });
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}