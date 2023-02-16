﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wordfulness.Migrations
{
	public partial class CustomUserWithCourse : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "Discriminator",
				table: "AspNetUsers",
				type: "TEXT",
				nullable: false,
				defaultValue: "");

			migrationBuilder.CreateTable(
				name: "CourseUser",
				columns: table => new
				{
					CoursesId = table.Column<int>(type: "INTEGER", nullable: false),
					UsersId = table.Column<string>(type: "TEXT", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_CourseUser", x => new { x.CoursesId, x.UsersId });
					table.ForeignKey(
						name: "FK_CourseUser_AspNetUsers_UsersId",
						column: x => x.UsersId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_CourseUser_Courses_CoursesId",
						column: x => x.CoursesId,
						principalTable: "Courses",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_CourseUser_UsersId",
				table: "CourseUser",
				column: "UsersId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "CourseUser");

			migrationBuilder.DropColumn(
				name: "Discriminator",
				table: "AspNetUsers");
		}
	}
}