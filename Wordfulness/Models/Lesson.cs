using System.ComponentModel.DataAnnotations.Schema;

namespace Wordfulness.Models
{
	public class Lesson
	{
		public int Id { get; set; }

		public string Name { get; set; }

		[ForeignKey("CourseId")]
		public Course Course { get; set; }

		public int CourseId { get; set; }

		public virtual ICollection<Flashcard> Flashcards { get; set; } = new List<Flashcard>();
	}
}
