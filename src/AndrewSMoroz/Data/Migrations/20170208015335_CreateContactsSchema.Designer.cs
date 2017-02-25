using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using AndrewSMoroz.Data;

namespace AndrewSMoroz.Data.Migrations
{
    [DbContext(typeof(ContactsDbContext))]
    [Migration("20170208015335_CreateContactsSchema")]
    partial class CreateContactsSchema
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AndrewSMoroz.Models.Company", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(100);

                    b.Property<string>("City")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("PostalCode")
                        .HasMaxLength(10);

                    b.Property<string>("State")
                        .HasMaxLength(2);

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("ID");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("AndrewSMoroz.Models.Contact", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompanyID");

                    b.Property<int>("ContactTypeID");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .HasMaxLength(50);

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("ID");

                    b.HasIndex("CompanyID");

                    b.HasIndex("ContactTypeID");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("AndrewSMoroz.Models.ContactPhone", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContactID");

                    b.Property<int>("ContactPhoneTypeID");

                    b.Property<string>("Extension")
                        .HasMaxLength(6);

                    b.Property<bool>("IsPrimaryPhone");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(12);

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("ID");

                    b.HasIndex("ContactID");

                    b.HasIndex("ContactPhoneTypeID");

                    b.ToTable("ContactPhone");
                });

            modelBuilder.Entity("AndrewSMoroz.Models.ContactPhoneType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(50);

                    b.Property<int>("Sequence");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("ID");

                    b.ToTable("ContactPhoneType","lookup");
                });

            modelBuilder.Entity("AndrewSMoroz.Models.ContactType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(50);

                    b.Property<int>("Sequence");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("ID");

                    b.ToTable("ContactType","lookup");
                });

            modelBuilder.Entity("AndrewSMoroz.Models.Event", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<int>("EventTypeID");

                    b.Property<int>("PositionID");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("ID");

                    b.HasIndex("EventTypeID");

                    b.HasIndex("PositionID");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("AndrewSMoroz.Models.EventContact", b =>
                {
                    b.Property<int>("EventID");

                    b.Property<int>("ContactID");

                    b.HasKey("EventID", "ContactID");

                    b.HasIndex("ContactID");

                    b.ToTable("EventContact");
                });

            modelBuilder.Entity("AndrewSMoroz.Models.EventType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(50);

                    b.Property<int>("Sequence");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("ID");

                    b.ToTable("EventType","lookup");
                });

            modelBuilder.Entity("AndrewSMoroz.Models.Position", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompanyID");

                    b.Property<DateTime?>("DatePosted");

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<string>("Title")
                        .HasMaxLength(100);

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("ID");

                    b.HasIndex("CompanyID");

                    b.ToTable("Position");
                });

            modelBuilder.Entity("AndrewSMoroz.Models.PositionContact", b =>
                {
                    b.Property<int>("PositionID");

                    b.Property<int>("ContactID");

                    b.Property<bool>("IsPrimaryContact");

                    b.HasKey("PositionID", "ContactID");

                    b.HasIndex("ContactID");

                    b.ToTable("PositionContact");
                });

            modelBuilder.Entity("AndrewSMoroz.Models.State", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abbreviation")
                        .HasMaxLength(2);

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<int>("Sequence");

                    b.HasKey("ID");

                    b.ToTable("State","lookup");
                });

            modelBuilder.Entity("AndrewSMoroz.Models.Contact", b =>
                {
                    b.HasOne("AndrewSMoroz.Models.Company", "Company")
                        .WithMany("Contacts")
                        .HasForeignKey("CompanyID")
                        .HasConstraintName("FK_Contact_CompanyID");

                    b.HasOne("AndrewSMoroz.Models.ContactType", "ContactType")
                        .WithMany("Contacts")
                        .HasForeignKey("ContactTypeID")
                        .HasConstraintName("FK_Contact_ContactTypeID");
                });

            modelBuilder.Entity("AndrewSMoroz.Models.ContactPhone", b =>
                {
                    b.HasOne("AndrewSMoroz.Models.Contact", "Contact")
                        .WithMany("ContactPhones")
                        .HasForeignKey("ContactID")
                        .HasConstraintName("FK_ContactPhone_ContactID");

                    b.HasOne("AndrewSMoroz.Models.ContactPhoneType", "ContactPhoneType")
                        .WithMany("ContactPhones")
                        .HasForeignKey("ContactPhoneTypeID")
                        .HasConstraintName("FK_ContactPhone_ContactPhoneTypeID");
                });

            modelBuilder.Entity("AndrewSMoroz.Models.Event", b =>
                {
                    b.HasOne("AndrewSMoroz.Models.EventType", "EventType")
                        .WithMany("Events")
                        .HasForeignKey("EventTypeID")
                        .HasConstraintName("FK_Event_EventTypeID");

                    b.HasOne("AndrewSMoroz.Models.Position", "Position")
                        .WithMany("Events")
                        .HasForeignKey("PositionID")
                        .HasConstraintName("FK_Event_PositionID");
                });

            modelBuilder.Entity("AndrewSMoroz.Models.EventContact", b =>
                {
                    b.HasOne("AndrewSMoroz.Models.Contact", "Contact")
                        .WithMany("EventContacts")
                        .HasForeignKey("ContactID")
                        .HasConstraintName("FK_EventContact_ContactID");

                    b.HasOne("AndrewSMoroz.Models.Event", "Event")
                        .WithMany("EventContacts")
                        .HasForeignKey("EventID")
                        .HasConstraintName("FK_EventContact_EventID");
                });

            modelBuilder.Entity("AndrewSMoroz.Models.Position", b =>
                {
                    b.HasOne("AndrewSMoroz.Models.Company", "Company")
                        .WithMany("Positions")
                        .HasForeignKey("CompanyID")
                        .HasConstraintName("FK_Position_CompanyID");
                });

            modelBuilder.Entity("AndrewSMoroz.Models.PositionContact", b =>
                {
                    b.HasOne("AndrewSMoroz.Models.Contact", "Contact")
                        .WithMany("PositionContacts")
                        .HasForeignKey("ContactID")
                        .HasConstraintName("FK_PositionContact_ContactID");

                    b.HasOne("AndrewSMoroz.Models.Position", "Position")
                        .WithMany("PositionContacts")
                        .HasForeignKey("PositionID")
                        .HasConstraintName("FK_PositionContact_PositionID");
                });
        }
    }
}
