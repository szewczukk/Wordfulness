using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wordfulness.Models
{
	public class Flashcard
	{
		public int Id { get; set; }

		[MinLength(1)]
		public string Front { get; set; }

		[MinLength(1)]
		public string Back { get; set; }

		[ForeignKey("CourseId")]
		public Lesson Lesson { get; set; }

		public int LessonId { get; set; }
	}
}