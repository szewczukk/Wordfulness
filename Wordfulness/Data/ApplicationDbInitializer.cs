using Microsoft.AspNetCore.Identity;
using Wordfulness.Models;

namespace Wordfulness.Data
{
	public class ApplicationDbInitializer
	{
		public async static Task Seed(IApplicationBuilder applicationBuilder)
		{
			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

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

				var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
				var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

				if (!await roleManager.RoleExistsAsync("ADMIN"))
				{
					await roleManager.CreateAsync(new IdentityRole<int>("ADMIN"));
				}

				if (!await roleManager.RoleExistsAsync("USER"))
				{
					await roleManager.CreateAsync(new IdentityRole<int>("USER"));
				}

				var admin = await userManager.FindByNameAsync("admin");
				if (admin == null)
				{
					var adminUser = new User()
					{
						UserName = "admin",
					};
					await userManager.CreateAsync(adminUser, "admin");
					await userManager.AddToRoleAsync(adminUser, "ADMIN");
				}

				var user = await userManager.FindByNameAsync("user");
				if (user == null)
				{
					var userUser = new User()
					{
						UserName = "user",
					};
					await userManager.CreateAsync(userUser, "user");
					await userManager.AddToRoleAsync(userUser, "USER");
				}
			}
		}
	}
}
