using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Model;
using System;

namespace HabraQuest.Migrations
{
    public partial class Finisher : Migration
    {
        public override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable("Finisher",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Token = c.String(),
                    Time = c.String()
                })
                .PrimaryKey("PK_Finisher", t => t.Id);
        }

        public override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Finisher");
        }
    }
}