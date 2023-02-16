using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;

namespace Wordfulness.Models
{
	public class User : IdentityUser
	{
		public virtual Collection<Course> Courses { get; set; }
	}
}