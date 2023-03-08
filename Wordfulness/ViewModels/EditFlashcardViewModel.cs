namespace Wordfulness.ViewModels
{
	public class EditFlashcardViewModel
	{
		public int Id { get; set; }

		public string Front { get; set; }

		public string Back { get; set; }

		public int LessonId
		{ get; set; }
	}
}
