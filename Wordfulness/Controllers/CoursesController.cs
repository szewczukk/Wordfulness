using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wordfulness.Models;
using Wordfulness.Services;
using Wordfulness.ViewModels;

namespace Wordfulness.Controllers
{
	[Authorize(Roles = "ADMIN")]
	public class CoursesController : Controller
	{
		private readonly ICoursesService _coursesService;

		public CoursesController(ICoursesService coursesService)
		{
			_coursesService = coursesService;
		}

		// GET: Courses
		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			var courses = await _coursesService.GetAllCoursesWithLessons();
			return View(courses);
		}

		// GET: Courses/Details/5
		[AllowAnonymous]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var course = await _coursesService.GetCourseWithLesson(id.Value);
			if (course == null)
			{
				return NotFound();
			}

			return View(course);
		}

		// GET: Courses/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Courses/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CourseViewModel courseViewModel)
		{
			if (ModelState.IsValid)
			{
				await _coursesService.CreateCourse(courseViewModel.Name);
				return RedirectToAction(nameof(Index));
			}
			return View(courseViewModel);
		}

		// GET: Courses/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var course = await _coursesService.GetCourseWithLesson(id.Value);
			if (course == null)
			{
				return NotFound();
			}
			return View(course);
		}

		// POST: Courses/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Course course)
		{
			if (id != course.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					await _coursesService.UpdateCourse(course.Id, course.Name);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (_coursesService.GetCourseWithLesson(id) == null)
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
			return View(course);
		}

		// GET: Courses/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var course = await _coursesService.GetCourseWithLesson(id.Value);
			if (course == null)
			{
				return NotFound();
			}

			return View(course);
		}

		// POST: Courses/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _coursesService.DeleteCourse(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
