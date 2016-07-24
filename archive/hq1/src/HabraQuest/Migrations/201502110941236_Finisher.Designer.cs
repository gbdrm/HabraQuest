using HabraQuest.Models;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using System;

namespace HabraQuest.Migrations
{
    [ContextType(typeof(HabraQuest.Models.ApplicationDbContext))]
    public partial class Finisher : IMigrationMetadata
    {
        string IMigrationMetadata.MigrationId
        {
            get
            {
                return "201502110941236_Finisher";
            }
        }
        
        string IMigrationMetadata.ProductVersion
        {
            get
            {
                return "7.0.0-beta2-11909";
            }
        }
        
        IModel IMigrationMetadata.TargetModel
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
                        b.Property<string>("Time");
                        b.Key("Id");
                    });
                
                return builder.Model;
            }
        }
    }
}