using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using HabraQuest.Model;

namespace HabraQuest.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20160726131042_AddQuestTask")]
    partial class AddQuestTask
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HabraQuest.Model.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<bool>("HasFinished");

                    b.Property<string>("Name");

                    b.Property<int>("TaskNumber");

                    b.Property<Guid>("Token");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("HabraQuest.Model.QuestTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Answers");

                    b.Property<string>("Content");

                    b.Property<int>("Done");

                    b.Property<string>("Title");

                    b.Property<int>("Watched");

                    b.HasKey("Id");

                    b.ToTable("Tasks");
                });
        }
    }
}
