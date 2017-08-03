using ExploreConsole.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.Data
{

    public class ExploreDbContext : DbContext
    {

        public virtual DbSet<MapSessionSave> MapSessionSaves { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<LocationConnection> LocationConnections { get; set; }
        public virtual DbSet<Map> Maps { get; set; }

        //--------------------------------------------------------------------------------------------------------------
        public ExploreDbContext(DbContextOptions<ExploreDbContext> options) : base(options)
        {
        }

        //--------------------------------------------------------------------------------------------------------------
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        //--------------------------------------------------------------------------------------------------------------
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MapSessionSave>(entity =>
            {
                entity.ToTable("MapSessionSave", "explore");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.SaveData).HasColumnType("varchar(max)");
                entity.Property(e => e.SaveDateTime).HasDefaultValueSql("SYSDATETIME()");
                entity.Property(e => e.MapId).HasColumnName("MapID");
                entity.HasOne(d => d.Map)
                    .WithMany(p => p.MapSessionSaves)
                    .HasForeignKey(d => d.MapId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_MapSessionSave_MapID");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("Item", "explore");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Description).HasColumnType("varchar(500)");
                entity.Property(e => e.Determiner).HasColumnType("varchar(20)");
                entity.Property(e => e.LocationId).HasColumnName("LocationID");
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
                entity.Property(e => e.Plural).HasDefaultValueSql("0");
                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Item_LocationID");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location", "explore");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.MapId).HasColumnName("MapID");
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
                entity.Property(e => e.Description).HasColumnType("varchar(1000)");
                entity.Property(e => e.IsInitialLocation).HasDefaultValueSql("0");
                entity.HasOne(d => d.Map)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.MapId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Location_MapID");
            });

            modelBuilder.Entity<LocationConnection>(entity =>
            {
                entity.ToTable("LocationConnection", "explore");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.ToLocationId).HasColumnName("ToLocationID");
                entity.Property(e => e.Direction)
                    .IsRequired()
                    .HasColumnType("varchar(10)");
                entity.Property(e => e.FromLocationId).HasColumnName("FromLocationID");
                entity.HasOne(d => d.ToLocation)
                    .WithMany(p => p.LocationConnectionToLocations)
                    .HasForeignKey(d => d.ToLocationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_LocationConnection_ToLocationID");
                entity.HasOne(d => d.FromLocation)
                    .WithMany(p => p.LocationConnectionFromLocations)
                    .HasForeignKey(d => d.FromLocationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_LocationConnection_FromLocationID");
            });

            modelBuilder.Entity<Map>(entity =>
            {
                entity.ToTable("Map", "explore");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });
        }

    }

}
