using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;

namespace Wordfulness.Models
{
	public class User : IdentityUser
	{
		public virtual Collection<Course> Courses { get; set; }
	}
}
