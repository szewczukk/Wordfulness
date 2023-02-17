using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;

namespace Wordfulness.Models
{
	public class User : IdentityUser<int>
	{
		public virtual Collection<Course> Courses { get; set; }
	}
}