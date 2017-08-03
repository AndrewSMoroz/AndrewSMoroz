using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using AndrewSMoroz.Data;

namespace AndrewSMoroz.Data.Migrations
{
    [DbContext(typeof(ExploreDbContext))]
    [Migration("20170803201724_CreateExploreSchema")]
    partial class CreateExploreSchema
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ExploreConsole.Entities.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Determiner")
                        .HasColumnType("varchar(20)");

                    b.Property<int>("LocationId")
                        .HasColumnName("LocationID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("Plural")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("0");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Item","explore");
                });

            modelBuilder.Entity("ExploreConsole.Entities.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(1000)");

                    b.Property<bool>("IsInitialLocation")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("0");

                    b.Property<int>("MapId")
                        .HasColumnName("MapID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("MapId");

                    b.ToTable("Location","explore");
                });

            modelBuilder.Entity("ExploreConsole.Entities.LocationConnection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<string>("Direction")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<int>("FromLocationId")
                        .HasColumnName("FromLocationID");

                    b.Property<int>("ToLocationId")
                        .HasColumnName("ToLocationID");

                    b.HasKey("Id");

                    b.HasIndex("FromLocationId");

                    b.HasIndex("ToLocationId");

                    b.ToTable("LocationConnection","explore");
                });

            modelBuilder.Entity("ExploreConsole.Entities.Map", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Map","explore");
                });

            modelBuilder.Entity("ExploreConsole.Entities.MapSessionSave", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<int>("MapId")
                        .HasColumnName("MapID");

                    b.Property<string>("SaveData")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime>("SaveDateTime")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIME()");

                    b.HasKey("Id");

                    b.HasIndex("MapId");

                    b.ToTable("MapSessionSave","explore");
                });

            modelBuilder.Entity("ExploreConsole.Entities.Item", b =>
                {
                    b.HasOne("ExploreConsole.Entities.Location", "Location")
                        .WithMany("Items")
                        .HasForeignKey("LocationId")
                        .HasConstraintName("FK_Item_LocationID");
                });

            modelBuilder.Entity("ExploreConsole.Entities.Location", b =>
                {
                    b.HasOne("ExploreConsole.Entities.Map", "Map")
                        .WithMany("Locations")
                        .HasForeignKey("MapId")
                        .HasConstraintName("FK_Location_MapID");
                });

            modelBuilder.Entity("ExploreConsole.Entities.LocationConnection", b =>
                {
                    b.HasOne("ExploreConsole.Entities.Location", "FromLocation")
                        .WithMany("LocationConnectionFromLocations")
                        .HasForeignKey("FromLocationId")
                        .HasConstraintName("FK_LocationConnection_FromLocationID");

                    b.HasOne("ExploreConsole.Entities.Location", "ToLocation")
                        .WithMany("LocationConnectionToLocations")
                        .HasForeignKey("ToLocationId")
                        .HasConstraintName("FK_LocationConnection_ToLocationID");
                });

            modelBuilder.Entity("ExploreConsole.Entities.MapSessionSave", b =>
                {
                    b.HasOne("ExploreConsole.Entities.Map", "Map")
                        .WithMany("MapSessionSaves")
                        .HasForeignKey("MapId")
                        .HasConstraintName("FK_MapSessionSave_MapID");
                });
        }
    }
}
