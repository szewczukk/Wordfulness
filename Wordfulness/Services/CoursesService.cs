using Microsoft.EntityFrameworkCore;
using Wordfulness.Data;
using Wordfulness.Models;

namespace Wordfulness.Services
{
	public interface ICoursesService
	{
		public Task<List<Course>> GetAllCoursesWithLessons();
		public Task<Course?> GetCourseWithLesson(int id);
		public Task CreateCourse(string name);
		public Task UpdateCourse(int id, string name);
		public Task DeleteCourse(int id);
	}

	public class CoursesService : ICoursesService
	{
		private readonly ApplicationDbContext _context;

		public CoursesService(ApplicationDbContext context)
		{
			_context = context;
		}

		public Task<List<Course>> GetAllCoursesWithLessons()
		{
			return _context.Courses.Include("Lessons").ToListAsync();
		}

		public Task<Course?> GetCourseWithLesson(int id)
		{
			return _context.Courses.Include("Lessons").FirstOrDefaultAsync(m => m.Id == id);
		}

		public async Task CreateCourse(string name)
		{
			_context.Courses.Add(new Course() { Name = name });
			await _context.SaveChangesAsync();
		}

		public async Task UpdateCourse(int id, string name)
		{
			_context.Courses.Update(new Course() { Id = id, Name = name });
			await _context.SaveChangesAsync();
		}

		public async Task DeleteCourse(int id)
		{
			var course = await GetCourseWithLesson(id);
			if (course != null)
			{
				_context.Courses.Remove(course);
				await _context.SaveChangesAsync();
			}
		}
	}
}
