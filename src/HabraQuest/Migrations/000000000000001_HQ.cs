using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Infrastructure;

namespace HabraQuest.Migrations
{
    public partial class HQ : Migration
    {
        public override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Progress",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Token = c.String(),
                    TaskNumber = c.Int(nullable: false),
                })
                .PrimaryKey("PK_Progress", t => t.Id);

            migrationBuilder.CreateTable(
                "QuestTask",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(),
                    Content = c.String(),
                    Number = c.Int(nullable: false),
                    Answer = c.String(),
                })
                .PrimaryKey("PK_QuestTask", t => t.Id);

        }

        public override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("QuestTask");
            migrationBuilder.DropTable("Progress");
        }
    }

    [ContextType(typeof (Models.ApplicationDbContext))]
    public partial class HQ : IMigrationMetadata
    {
        string IMigrationMetadata.MigrationId
        {
            get { return "000000000000001_HQ"; }
        }

        string IMigrationMetadata.ProductVersion
        {
            get { return "7.0.0-beta2"; }
        }

        IModel IMigrationMetadata.TargetModel
        {
            get
            {
                var builder = new BasicModelBuilder();

                builder.Entity("HabraQuest.Models.Progress", b =>
                {
                    b.Property<int>("Id");
                    b.Property<string>("Token");
                    b.Property<int>("TaskNumber");
                    b.Key("Id");
                    b.ForRelational().Table("Progress");
                });

                builder.Entity("HabraQuest.Models.QuestTask", b =>
                {
                    b.Property<int>("Id");
                    b.Property<string>("Title");
                    b.Property<string>("Content");
                    b.Property<int>("Number");
                    b.Property<string>("Answer");
                    b.Key("Id");
                    b.ForRelational().Table("QuestTask");
                });

                return builder.Model;
            }
        }
    }
}