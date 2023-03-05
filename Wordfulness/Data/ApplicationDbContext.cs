using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wordfulness.Models;

namespace Wordfulness.Data
{
	public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Course> Courses { get; set; }

		public DbSet<Lesson> Lessons { get; set; }

		public DbSet<Wordfulness.Models.Flashcard> Flashcard { get; set; }
	}
}