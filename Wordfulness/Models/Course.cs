using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Wordfulness.Models
{
	public class Course
	{
		public int Id { get; set; }

		[MinLength(1)] public string Name { get; set; }

		public virtual Collection<User> Users { get; set; }
	}
}