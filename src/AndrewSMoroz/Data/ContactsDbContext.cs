using AndrewSMoroz.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.Data
{

    public class ContactsDbContext : DbContext
    {

        public DbSet<Company> Companies { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<ContactPhone> ContactPhones { get; set; }
        public DbSet<ContactPhoneType> ContactPhoneTypes { get; set; }
        public DbSet<PositionContact> PositionContacts { get; set; }
        public DbSet<EventContact> EventContacts { get; set; }
        public DbSet<State> States { get; set; }

        //--------------------------------------------------------------------------------------------------------------
        public ContactsDbContext(DbContextOptions<ContactsDbContext> options) : base(options)
        {
        }

        //--------------------------------------------------------------------------------------------------------------
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        //--------------------------------------------------------------------------------------------------------------
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // EF will create table names as plurals (matching the DbSets above) unless you specify otherwise like this
            modelBuilder.Entity<Company>().ToTable("Company");
            modelBuilder.Entity<Position>().ToTable("Position");
            modelBuilder.Entity<Contact>().ToTable("Contact");
            modelBuilder.Entity<Event>().ToTable("Event");
            modelBuilder.Entity<EventType>().ToTable("EventType", "lookup");
            modelBuilder.Entity<ContactType>().ToTable("ContactType", "lookup");
            modelBuilder.Entity<ContactPhone>().ToTable("ContactPhone");
            modelBuilder.Entity<ContactPhoneType>().ToTable("ContactPhoneType", "lookup");
            modelBuilder.Entity<PositionContact>().ToTable("PositionContact");
            modelBuilder.Entity<EventContact>().ToTable("EventContact");
            modelBuilder.Entity<State>().ToTable("State", "lookup");

            // Set default values for columns that need them
            modelBuilder.Entity<Company>().Property(p => p.IsRecruiter).HasDefaultValue(false);

            // This is how you get EF to create a composite primary key
            modelBuilder.Entity<PositionContact>().HasKey(pc => new { pc.PositionID, pc.ContactID });
            modelBuilder.Entity<EventContact>().HasKey(ac => new { ac.EventID, ac.ContactID });

            // EF creates foreign key constraints with cascading delete rules unless you specify otherwise like this
            // Also, this is how you can specify the names of the foreign key constraints
            modelBuilder.Entity<Position>().HasOne(d => d.Company)
                                           .WithMany(p => p.Positions)
                                           .HasForeignKey(d => d.CompanyID)
                                           .OnDelete(DeleteBehavior.Restrict)
                                           .HasConstraintName("FK_Position_CompanyID");

            modelBuilder.Entity<Position>().HasOne(d => d.RecruiterCompany)
                                           .WithMany(p => p.RecruiterPositions)
                                           .HasForeignKey(d => d.RecruiterCompanyID)
                                           .OnDelete(DeleteBehavior.Restrict)
                                           .HasConstraintName("FK_Position_RecruiterCompanyID")
                                           .IsRequired(false);

            modelBuilder.Entity<Event>().HasOne(d => d.Position)
                                                .WithMany(p => p.Events)
                                                .HasForeignKey(d => d.PositionID)
                                                .OnDelete(DeleteBehavior.Restrict)
                                                .HasConstraintName("FK_Event_PositionID");

            modelBuilder.Entity<Event>().HasOne(d => d.EventType)
                                                .WithMany(p => p.Events)
                                                .HasForeignKey(d => d.EventTypeID)
                                                .OnDelete(DeleteBehavior.Restrict)
                                                .HasConstraintName("FK_Event_EventTypeID");

            modelBuilder.Entity<ContactPhone>().HasOne(d => d.Contact)
                                               .WithMany(p => p.ContactPhones)
                                               .HasForeignKey(d => d.ContactID)
                                               .OnDelete(DeleteBehavior.Restrict)
                                               .HasConstraintName("FK_ContactPhone_ContactID");

            modelBuilder.Entity<ContactPhone>().HasOne(d => d.ContactPhoneType)
                                               .WithMany(p => p.ContactPhones)
                                               .HasForeignKey(d => d.ContactPhoneTypeID)
                                               .OnDelete(DeleteBehavior.Restrict)
                                               .HasConstraintName("FK_ContactPhone_ContactPhoneTypeID");

            modelBuilder.Entity<Contact>().HasOne(d => d.Company)
                                          .WithMany(p => p.Contacts)
                                          .HasForeignKey(d => d.CompanyID)
                                          .OnDelete(DeleteBehavior.Restrict)
                                          .HasConstraintName("FK_Contact_CompanyID");

            modelBuilder.Entity<Contact>().HasOne(d => d.ContactType)
                                          .WithMany(p => p.Contacts)
                                          .HasForeignKey(d => d.ContactTypeID)
                                          .OnDelete(DeleteBehavior.Restrict)
                                          .HasConstraintName("FK_Contact_ContactTypeID");

            modelBuilder.Entity<PositionContact>().HasOne(d => d.Position)
                                                  .WithMany(p => p.PositionContacts)
                                                  .HasForeignKey(d => d.PositionID)
                                                  .OnDelete(DeleteBehavior.Restrict)
                                                  .HasConstraintName("FK_PositionContact_PositionID");

            modelBuilder.Entity<PositionContact>().HasOne(d => d.Contact)
                                                  .WithMany(p => p.PositionContacts)
                                                  .HasForeignKey(d => d.ContactID)
                                                  .OnDelete(DeleteBehavior.Restrict)
                                                  .HasConstraintName("FK_PositionContact_ContactID");

            modelBuilder.Entity<EventContact>().HasOne(d => d.Event)
                                                .WithMany(p => p.EventContacts)
                                                .HasForeignKey(d => d.EventID)
                                                .OnDelete(DeleteBehavior.Restrict)
                                                .HasConstraintName("FK_EventContact_EventID");

            modelBuilder.Entity<EventContact>().HasOne(d => d.Contact)
                                                .WithMany(p => p.EventContacts)
                                                .HasForeignKey(d => d.ContactID)
                                                .OnDelete(DeleteBehavior.Restrict)
                                                .HasConstraintName("FK_EventContact_ContactID");


        }

    }

}
