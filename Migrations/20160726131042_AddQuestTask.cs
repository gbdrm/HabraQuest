using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HabraQuest.Migrations
{
    public partial class AddQuestTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Answers = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Done = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Watched = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });

            migrationBuilder.AddColumn<bool>(
                name: "HasFinished",
                table: "Players",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasFinished",
                table: "Players");

            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
