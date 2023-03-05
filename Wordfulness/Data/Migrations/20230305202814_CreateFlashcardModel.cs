using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wordfulness.Migrations
{
	public partial class CreateFlashcardModel : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Flashcard",
				columns: table => new
				{
					Id = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					Front = table.Column<string>(type: "TEXT", nullable: false),
					Back = table.Column<string>(type: "TEXT", nullable: false),
					CourseId = table.Column<int>(type: "INTEGER", nullable: false),
					LessonId = table.Column<int>(type: "INTEGER", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Flashcard", x => x.Id);
					table.ForeignKey(
						name: "FK_Flashcard_Lessons_CourseId",
						column: x => x.CourseId,
						principalTable: "Lessons",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Flashcard_CourseId",
				table: "Flashcard",
				column: "CourseId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Flashcard");
		}
	}
}
