using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using HabraQuest.Model;

namespace HabraQuest.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20160725063832_Initial")]
    partial class Initial
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

                    b.Property<string>("Name");

                    b.Property<int>("TaskNumber");

                    b.Property<Guid>("Token");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });
        }
    }
}
