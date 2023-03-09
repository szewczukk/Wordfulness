using Microsoft.AspNetCore.Mvc;
using Wordfulness.Controllers;
using Wordfulness.Models;
using Wordfulness.Services;
using Wordfulness.ViewModels;
using WordfulnessTests.Services;

namespace WordfulnessTests.Tests
{
    public class CourseControllerTests
    {
        private ICoursesService CreateTestCoursesService()
        {
            ICoursesService coursesService = new TestCoursesService();
            coursesService.CreateCourse("Course 1");
            coursesService.CreateCourse("Course 2");
            coursesService.CreateCourse("Course 3");

            return coursesService;
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfCourses()
        {
            var coursesService = CreateTestCoursesService();
            var controller = new CoursesController(coursesService);

            var result = await controller.Index();

            var actionResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Course>>(actionResult.ViewData.Model);
            Assert.Equal(3, model.Count());
        }

        [Fact]
        public async Task Detailed_ReturnsAViewResult_WithACourseAndLessons()
        {
            var coursesService = CreateTestCoursesService();
            var controller = new CoursesController(coursesService);

            var result = await controller.Edit(0);

            var actionResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Course>(actionResult.ViewData.Model);
            Assert.Equal(0, model.Id);
            Assert.Equal("Course 1", model.Name);
            Assert.Equal(0, model.Lessons.Count);
        }

        [Fact]
        public async Task Edit_ReturnsAViewResult_WithAChangedCourseAndLessons()
        {
            var coursesService = CreateTestCoursesService();
            var controller = new CoursesController(coursesService);

            var result = await controller.Edit(1, new Course()
            {
                Id = 1,
                Name = "Fancy course",
                Lessons = new List<Lesson>()
            });
            var course = await coursesService.GetCourseWithLesson(1);

            var actionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", actionResult.ActionName);
            Assert.Equal("Fancy course", course.Name);
        }

        [Fact]
        public void Create_ReturnsAViewResult()
        {
            var coursesService = CreateTestCoursesService();
            var controller = new CoursesController(coursesService);

            var result = controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsARedirect_And_CreatesANewCourse()
        {
            var coursesService = CreateTestCoursesService();
            var controller = new CoursesController(coursesService);

            var result = await controller.Create(new CourseViewModel()
            {
                Name = "Course 4"
            });
            var course = await coursesService.GetCourseWithLesson(3);

            var actionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", actionResult.ActionName);
            Assert.Equal("Course 4", course.Name);
        }

        [Fact]
        public async Task Delete_ReturnsAViewResult()
        {
            var coursesService = CreateTestCoursesService();
            var controller = new CoursesController(coursesService);

            var result = await controller.Delete(2);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_ReturnsARedirect_And_DeletesANewCourse()
        {
            var coursesService = CreateTestCoursesService();
            var controller = new CoursesController(coursesService);

            var result = await controller.DeleteConfirmed(2);

            var actionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", actionResult.ActionName);
            await Assert.ThrowsAsync<KeyNotFoundException>(() => coursesService.GetCourseWithLesson(2));
        }
    }
}