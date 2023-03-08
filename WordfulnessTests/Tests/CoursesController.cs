using Microsoft.AspNetCore.Mvc;
using Moq;
using Wordfulness.Controllers;
using Wordfulness.Models;
using Wordfulness.Services;

namespace WordfulnessTests.Tests
{
    public class CourseControllerTests
    {
        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfCourses()
        {
            var courses = new List<Course>()
            {
                new Course { Id = 1, Name = "Course 1" },
                new Course { Id = 2, Name = "Course 2" },
                new Course { Id = 3, Name = "Course 3" }
            };
            var mockService = new Mock<ICoursesService>();
            mockService.Setup(s => s.GetAllCoursesWithLessons()).ReturnsAsync(courses);
            var controller = new CoursesController(mockService.Object);

            var result = await controller.Index();

            var actionResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Course>>(actionResult.ViewData.Model);
            Assert.Equal(3, model.Count());
        }

        [Fact]
        public async Task Detailed_ReturnsAViewResult_WithACourseAndLessons()
        {
            var courses = new List<Course>()
            {
                new Course()
                {
                    Id = 1,
                    Name = "Course 1",
                    Lessons = new List<Lesson>()
                    {
                        new Lesson()
                        {
                            Id = 1,
                            Name = "Hello World",
                            CourseId = 1
                        }
                    }
                },
                new Course()
                {
                    Id = 2,
                    Name = "Course 2",
                    Lessons = new List<Lesson>()
                    {
                        new Lesson()
                        {
                            Id = 2,
                            Name = "Hello World",
                            CourseId = 2
                        }
                    }
                },
            };
            var mockService = new Mock<ICoursesService>();
            mockService.Setup(s => s.GetCourseWithLesson(1)).ReturnsAsync(courses[0]);
            var controller = new CoursesController(mockService.Object);

            var result = await controller.Details(1);

            var actionResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Course>(actionResult.ViewData.Model);
            Assert.Equal(1, model.Id);
            Assert.Equal("Course 1", model.Name);
            Assert.Equal(1, model.Lessons.ToList()[0].Id);
            Assert.Equal(1, model.Lessons.ToList()[0].CourseId);
            Assert.Equal("Hello World", model.Lessons.ToList()[0].Name);
        }
    }
}