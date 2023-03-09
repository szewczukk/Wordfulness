using Wordfulness.Models;
using Wordfulness.Services;

namespace WordfulnessTests.Services
{
    public class TestCoursesService : ICoursesService
    {
        private int _nextId = 0;
        private readonly Dictionary<int, Course> _courses = new Dictionary<int, Course>();

        public Task CreateCourse(string name)
        {
            _courses[_nextId] = new Course { Id = 0, Name = name, Lessons = new List<Lesson>() };
            _nextId++;

            return Task.CompletedTask;
        }

        public Task<List<Course>> GetAllCoursesWithLessons()
        {
            return Task.FromResult(_courses.Values.ToList());
        }

        public Task<Course?> GetCourseWithLesson(int id)
        {
            return Task.FromResult(_courses[id]);
        }

        public Task UpdateCourse(int id, string name)
        {
            _courses[id] = new Course() { Id = id, Name = name, Lessons = _courses[id].Lessons };
            return Task.CompletedTask;
        }

        public Task DeleteCourse(int id)
        {
            _courses.Remove(id);
            return Task.CompletedTask;
        }
    }
}
