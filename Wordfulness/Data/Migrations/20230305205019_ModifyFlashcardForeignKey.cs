using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wordfulness.Migrations
{
	public partial class ModifyFlashcardForeignKey : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Flashcard_Lessons_CourseId",
				table: "Flashcard");

			migrationBuilder.DropIndex(
				name: "IX_Flashcard_CourseId",
				table: "Flashcard");

			migrationBuilder.DropColumn(
				name: "CourseId",
				table: "Flashcard");

			migrationBuilder.CreateIndex(
				name: "IX_Flashcard_LessonId",
				table: "Flashcard",
				column: "LessonId");

			migrationBuilder.AddForeignKey(
				name: "FK_Flashcard_Lessons_LessonId",
				table: "Flashcard",
				column: "LessonId",
				principalTable: "Lessons",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Flashcard_Lessons_LessonId",
				table: "Flashcard");

			migrationBuilder.DropIndex(
				name: "IX_Flashcard_LessonId",
				table: "Flashcard");

			migrationBuilder.AddColumn<int>(
				name: "CourseId",
				table: "Flashcard",
				type: "INTEGER",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.CreateIndex(
				name: "IX_Flashcard_CourseId",
				table: "Flashcard",
				column: "CourseId");

			migrationBuilder.AddForeignKey(
				name: "FK_Flashcard_Lessons_CourseId",
				table: "Flashcard",
				column: "CourseId",
				principalTable: "Lessons",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
