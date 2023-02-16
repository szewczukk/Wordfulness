using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Wordfulness.Models;

namespace Wordfulness.Controllers
{
	public class CoursesController : Controller
	{

		public CoursesController() { }

		public IActionResult Index()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}