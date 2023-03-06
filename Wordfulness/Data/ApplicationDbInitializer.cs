using Wordfulness.Models;

namespace Wordfulness.Data
{
	public class ApplicationDbInitializer
	{
		public static void Seed(IApplicationBuilder applicationBuilder)
		{
			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

				context.Database.EnsureCreated();

				if (!context.Courses.Any())
				{
					context.Courses.AddRange(new List<Course>()
					{
						new Course()
						{
							Name = "Spanish"
						},
						new Course()
						{
							Name = "German"
						}
					});

					context.SaveChanges();
				}

				if (!context.Lessons.Any())
				{
					context.Lessons.AddRange(new List<Lesson>()
					{
						new Lesson()
						{
							Name = "Spanish 101",
							CourseId = 1
						},
						new Lesson()
						{
							Name = "Spanish 102",
							CourseId = 1
						},
						new Lesson()
						{
							Name = "German 101",
							CourseId = 2
						},
					});

					context.SaveChanges();
				}

				if (!context.Flashcard.Any())
				{
					context.Flashcard.AddRange(new List<Flashcard>()
					{
						new Flashcard()
						{
							LessonId = 1,
							Front = "Hola",
							Back = "Hi"
						},
						new Flashcard()
						{
							LessonId = 1,
							Front = "Buenos dias",
							Back = "Good morning"
						},
						new Flashcard()
						{
							LessonId = 2,
							Front = "Yo soy",
							Back = "I am"
						},
						new Flashcard()
						{
							LessonId = 3,
							Front = "Hallo",
							Back = "Hello"
						}
					});

					context.SaveChanges();
				}
			}
		}
	}
}
