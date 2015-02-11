using HabraQuest.Models;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using System;

namespace HabraQuest.Migrations
{
    [ContextType(typeof(HabraQuest.Models.ApplicationDbContext))]
    public class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        public override IModel Model
        {
            get
            {
                var builder = new BasicModelBuilder();
                
                builder.Entity("HabraQuest.Models.Finisher", b =>
                    {
                        b.Property<int>("Id")
                            .GenerateValueOnAdd();
                        b.Property<string>("Name");
                        b.Property<string>("Token");
                        b.Key("Id");
                    });
                
                builder.Entity("HabraQuest.Models.Progress", b =>
                    {
                        b.Property<int>("Id")
                            .GenerateValueOnAdd();
                        b.Property<int>("TaskNumber");
                        b.Property<string>("Token");
                        b.Key("Id");
                    });
                
                builder.Entity("HabraQuest.Models.QuestTask", b =>
                    {
                        b.Property<string>("Answer");
                        b.Property<string>("Content");
                        b.Property<int>("Id")
                            .GenerateValueOnAdd();
                        b.Property<int>("Number");
                        b.Property<string>("Title");
                        b.Key("Id");
                    });
                
                return builder.Model;
            }
        }
    }
}