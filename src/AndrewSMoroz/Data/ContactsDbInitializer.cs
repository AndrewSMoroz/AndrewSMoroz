using AndrewSMoroz.Models;
using AndrewSMoroz.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.Data
{

    public static class ContactsDbInitializer
    {

        public static void Initialize(ContactsDbContext context)
        {

            // There are 2 ways to create the database from Entity Framework (that I know of)
            // 1. context.Database.EnsureCreated()
            //    C# code
            //    This will create the database if it's not there, but will prevent you from using migrations in the future
            // 2. dotnet ef migrations add [migrationName] -c [contextName]
            //    dotnet ef database update -c [contextName]
            //    Command Line Interface tools
            //    Creates a migration to serve as a starting point, and applies it

            const string USER_NAME = "andrewsmoroz@yahoo.com";

            // Look for data in lookup tables
            if (context.EventTypes.Any())
            {
                return;   // DB has already been initialized with test data
            }

            //----------------------------------------------------------------------------------------------------------
            EventType[] eventTypes = new EventType[]
            {
                new EventType { Sequence = 10, Description = "Applied", UserName = USER_NAME },
                new EventType { Sequence = 20, Description = "Submitted Resume", UserName = USER_NAME },
                new EventType { Sequence = 30, Description = "Phone Interview", UserName = USER_NAME },
                new EventType { Sequence = 40, Description = "In-Person Interview", UserName = USER_NAME },
                new EventType { Sequence = 50, Description = "Follow-Up Call", UserName = USER_NAME },
                new EventType { Sequence = 60, Description = "Removed from Consideration", UserName = USER_NAME },
            };
            foreach (EventType et in eventTypes)
            {
                context.EventTypes.Add(et);
            }
            context.SaveChanges();

            //----------------------------------------------------------------------------------------------------------
            ContactType[] contactTypes = new ContactType[]
            {
                new ContactType { Sequence = 10, Description = "HR", UserName = USER_NAME },
                new ContactType { Sequence = 20, Description = "Software Development Manager", UserName = USER_NAME },
                new ContactType { Sequence = 30, Description = "Other Manager", UserName = USER_NAME },
                new ContactType { Sequence = 40, Description = "Developer", UserName = USER_NAME },
                new ContactType { Sequence = 50, Description = "QA", UserName = USER_NAME },
                new ContactType { Sequence = 50, Description = "Owner", UserName = USER_NAME },
            };
            foreach (ContactType ct in contactTypes)
            {
                context.ContactTypes.Add(ct);
            }
            context.SaveChanges();

            //----------------------------------------------------------------------------------------------------------
            ContactPhoneType[] contactPhoneTypes = new ContactPhoneType[]
            {
                new ContactPhoneType { Sequence = 10, Description = "Office", UserName = USER_NAME },
                new ContactPhoneType { Sequence = 20, Description = "Work Mobile", UserName = USER_NAME },
                new ContactPhoneType { Sequence = 30, Description = "Personal Mobile", UserName = USER_NAME },
                new ContactPhoneType { Sequence = 40, Description = "Home", UserName = USER_NAME },
            };
            foreach (ContactPhoneType cpt in contactPhoneTypes)
            {
                context.ContactPhoneTypes.Add(cpt);
            }
            context.SaveChanges();

            //----------------------------------------------------------------------------------------------------------
            State[] states = new State[]
            {
                new State { Abbreviation = "AL", Name = "Alabama", Sequence = 10 },
                new State { Abbreviation = "AK", Name = "Alaska", Sequence = 20 },
                new State { Abbreviation = "AZ", Name = "Arizona", Sequence = 30 },
                new State { Abbreviation = "AR", Name = "Arkansas", Sequence = 40 },
                new State { Abbreviation = "CA", Name = "California", Sequence = 50 },
                new State { Abbreviation = "CO", Name = "Colorado", Sequence = 60 },
                new State { Abbreviation = "CT", Name = "Connecticut", Sequence = 70 },
                new State { Abbreviation = "DE", Name = "Delaware", Sequence = 80 },
                new State { Abbreviation = "FL", Name = "Florida", Sequence = 90 },
                new State { Abbreviation = "GA", Name = "Georgia", Sequence = 100 },
                new State { Abbreviation = "HI", Name = "Hawaii", Sequence = 110 },
                new State { Abbreviation = "ID", Name = "Idaho", Sequence = 120 },
                new State { Abbreviation = "IL", Name = "Illinois", Sequence = 130 },
                new State { Abbreviation = "IN", Name = "Indiana", Sequence = 140 },
                new State { Abbreviation = "IA", Name = "Iowa", Sequence = 150 },
                new State { Abbreviation = "KS", Name = "Kansas", Sequence = 160 },
                new State { Abbreviation = "KY", Name = "Kentucky", Sequence = 170 },
                new State { Abbreviation = "LA", Name = "Louisiana", Sequence = 180 },
                new State { Abbreviation = "ME", Name = "Maine", Sequence = 190 },
                new State { Abbreviation = "MD", Name = "Maryland", Sequence = 200 },
                new State { Abbreviation = "MA", Name = "Massachusetts", Sequence = 210 },
                new State { Abbreviation = "MI", Name = "Michigan", Sequence = 220 },
                new State { Abbreviation = "MN", Name = "Minnesota", Sequence = 230 },
                new State { Abbreviation = "MS", Name = "Mississippi", Sequence = 240 },
                new State { Abbreviation = "MO", Name = "Missouri", Sequence = 250 },
                new State { Abbreviation = "MT", Name = "Montana", Sequence = 260 },
                new State { Abbreviation = "NE", Name = "Nebraska", Sequence = 270 },
                new State { Abbreviation = "NV", Name = "Nevada", Sequence = 280 },
                new State { Abbreviation = "NH", Name = "New Hampshire", Sequence = 290 },
                new State { Abbreviation = "NJ", Name = "New Jersey", Sequence = 300 },
                new State { Abbreviation = "NM", Name = "New Mexico", Sequence = 310 },
                new State { Abbreviation = "NY", Name = "New York", Sequence = 320 },
                new State { Abbreviation = "NC", Name = "North Carolina", Sequence = 330 },
                new State { Abbreviation = "ND", Name = "North Dakota", Sequence = 340 },
                new State { Abbreviation = "OH", Name = "Ohio", Sequence = 350 },
                new State { Abbreviation = "OK", Name = "Oklahoma", Sequence = 360 },
                new State { Abbreviation = "OR", Name = "Oregon", Sequence = 370 },
                new State { Abbreviation = "PA", Name = "Pennsylvania", Sequence = 380 },
                new State { Abbreviation = "RI", Name = "Rhode Island", Sequence = 390 },
                new State { Abbreviation = "SC", Name = "South Carolina", Sequence = 400 },
                new State { Abbreviation = "SD", Name = "South Dakota", Sequence = 410 },
                new State { Abbreviation = "TN", Name = "Tennessee", Sequence = 420 },
                new State { Abbreviation = "TX", Name = "Texas", Sequence = 430 },
                new State { Abbreviation = "UT", Name = "Utah", Sequence = 440 },
                new State { Abbreviation = "VT", Name = "Vermont", Sequence = 450 },
                new State { Abbreviation = "VA", Name = "Virginia", Sequence = 460 },
                new State { Abbreviation = "WA", Name = "Washington", Sequence = 470 },
                new State { Abbreviation = "WV", Name = "West Virginia", Sequence = 480 },
                new State { Abbreviation = "WI", Name = "Wisconsin", Sequence = 490 },
                new State { Abbreviation = "WY", Name = "Wyoming", Sequence = 500 },
                new State { Abbreviation = "AS", Name = "American Samoa", Sequence = 510 },
                new State { Abbreviation = "DC", Name = "District of Columbia", Sequence = 520 },
                new State { Abbreviation = "FM", Name = "Federated States of Micronesia", Sequence = 530 },
                new State { Abbreviation = "GU", Name = "Guam", Sequence = 540 },
                new State { Abbreviation = "MH", Name = "Marshall Islands", Sequence = 550 },
                new State { Abbreviation = "MP", Name = "Northern Mariana Islands", Sequence = 560 },
                new State { Abbreviation = "PW", Name = "Palau", Sequence = 570 },
                new State { Abbreviation = "PR", Name = "Puerto Rico", Sequence = 580 },
                new State { Abbreviation = "VI", Name = "Virgin Islands", Sequence = 590 }
            };
            foreach (State state in states)
            {
                context.States.Add(state);
            }
            context.SaveChanges();

            //----------------------------------------------------------------------------------------------------------
            List<Company> companies = new List<Company>();
            companies.Add(new Company() { Name = "IBM", Address = "123 I Street", City = "Indianapolis", State = "IN", PostalCode = "55555-5555", UserName = USER_NAME });
            companies.Add(new Company() { Name = "Procter & Gamble", Address = "234 P Street", City = "Cincinnati", State = "OH", PostalCode = "44444-4444", UserName = USER_NAME });
            companies.Add(new Company() { Name = "Frank's Books", Address = "345 F Avenue", City = "Miami", State = "FL", PostalCode = "33333-3333", UserName = USER_NAME });
            foreach (Company c in companies)
            {
                context.Companies.Add(c);
            }
            context.SaveChanges();

            //----------------------------------------------------------------------------------------------------------
            List<Position> positions = new List<Position>();
            positions.Add(new Position() { CompanyID = context.Companies.Single(c => c.Name == "IBM").ID, Title = "C# Developer", Description = "C# Developer position description", DatePosted = new DateTime(2016, 5, 15), UserName = USER_NAME });
            positions.Add(new Position() { CompanyID = context.Companies.Single(c => c.Name == "IBM").ID, Title = "Web Developer", Description = "Web Developer position description", DatePosted = new DateTime(2016, 7, 21), UserName = USER_NAME });
            positions.Add(new Position() { CompanyID = context.Companies.Single(c => c.Name == "IBM").ID, Title = "DBA", Description = "DBA position description", DatePosted = new DateTime(2016, 6, 19), UserName = USER_NAME });
            positions.Add(new Position() { CompanyID = context.Companies.Single(c => c.Name == "Frank's Books").ID, Title = "WPF Developer", Description = "WPF Developer position description", DatePosted = new DateTime(2016, 12, 15), UserName = USER_NAME });
            foreach (Position p in positions)
            {
                context.Positions.Add(p);
            }
            context.SaveChanges();

            //----------------------------------------------------------------------------------------------------------
            List<Contact> contacts = new List<Contact>();
            contacts.Add(new Contact() { CompanyID = context.Companies.Single(c => c.Name == "IBM").ID, ContactTypeID = context.ContactTypes.Single(ct => ct.Description == "HR").ID, FirstName = "John", LastName = "Johnson", UserName = USER_NAME });
            contacts.Add(new Contact() { CompanyID = context.Companies.Single(c => c.Name == "IBM").ID, ContactTypeID = context.ContactTypes.Single(ct => ct.Description == "Software Development Manager").ID, FirstName = "Phil", LastName = "Phillips", UserName = USER_NAME });
            contacts.Add(new Contact() { CompanyID = context.Companies.Single(c => c.Name == "IBM").ID, ContactTypeID = context.ContactTypes.Single(ct => ct.Description == "Other Manager").ID, FirstName = "Doug", LastName = "Douglas", UserName = USER_NAME });
            contacts.Add(new Contact() { CompanyID = context.Companies.Single(c => c.Name == "IBM").ID, ContactTypeID = context.ContactTypes.Single(ct => ct.Description == "Developer").ID, FirstName = "Megan", LastName = "Smith", UserName = USER_NAME });
            contacts.Add(new Contact() { CompanyID = context.Companies.Single(c => c.Name == "IBM").ID, ContactTypeID = context.ContactTypes.Single(ct => ct.Description == "QA").ID, FirstName = "Wendy", LastName = "West", UserName = USER_NAME });
            contacts.Add(new Contact() { CompanyID = context.Companies.Single(c => c.Name == "Frank's Books").ID, ContactTypeID = context.ContactTypes.Single(ct => ct.Description == "Owner").ID, FirstName = "Frank", LastName = "Bookman", UserName = USER_NAME });
            foreach (Contact c in contacts)
            {
                context.Contacts.Add(c);
            }
            context.SaveChanges();

            //----------------------------------------------------------------------------------------------------------
            List<ContactPhone> contactPhones = new List<ContactPhone>();
            contactPhones.Add(new ContactPhone() { ContactID = context.Contacts.Single(c => c.Company.Name == "IBM" && c.FullName == "John Johnson").ID, ContactPhoneTypeID = context.ContactPhoneTypes.Single(cpt => cpt.Description == "Office").ID, IsPrimaryPhone = true, PhoneNumber = "555-555-1111", UserName = USER_NAME });
            contactPhones.Add(new ContactPhone() { ContactID = context.Contacts.Single(c => c.Company.Name == "IBM" && c.FullName == "Phil Phillips").ID, ContactPhoneTypeID = context.ContactPhoneTypes.Single(cpt => cpt.Description == "Office").ID, IsPrimaryPhone = true, PhoneNumber = "555-555-2222", Extension = "x5890", UserName = USER_NAME });
            contactPhones.Add(new ContactPhone() { ContactID = context.Contacts.Single(c => c.Company.Name == "IBM" && c.FullName == "Phil Phillips").ID, ContactPhoneTypeID = context.ContactPhoneTypes.Single(cpt => cpt.Description == "Work Mobile").ID, IsPrimaryPhone = false, PhoneNumber = "555-555-3333", UserName = USER_NAME });
            contactPhones.Add(new ContactPhone() { ContactID = context.Contacts.Single(c => c.Company.Name == "IBM" && c.FullName == "Phil Phillips").ID, ContactPhoneTypeID = context.ContactPhoneTypes.Single(cpt => cpt.Description == "Personal Mobile").ID, IsPrimaryPhone = false, PhoneNumber = "555-555-4444", UserName = USER_NAME });
            contactPhones.Add(new ContactPhone() { ContactID = context.Contacts.Single(c => c.Company.Name == "Frank's Books" && c.FullName == "Frank Bookman").ID, ContactPhoneTypeID = context.ContactPhoneTypes.Single(cpt => cpt.Description == "Personal Mobile").ID, IsPrimaryPhone = true, PhoneNumber = "555-555-5555", UserName = USER_NAME });
            contactPhones.Add(new ContactPhone() { ContactID = context.Contacts.Single(c => c.Company.Name == "Frank's Books" && c.FullName == "Frank Bookman").ID, ContactPhoneTypeID = context.ContactPhoneTypes.Single(cpt => cpt.Description == "Office").ID, IsPrimaryPhone = false, PhoneNumber = "555-555-6666", Extension = "x490", UserName = USER_NAME });
            foreach (ContactPhone cp in contactPhones)
            {
                context.ContactPhones.Add(cp);
            }
            context.SaveChanges();

            //----------------------------------------------------------------------------------------------------------
            List<PositionContact> positionContacts = new List<PositionContact>();
            positionContacts.Add(new PositionContact() { PositionID = context.Positions.Single(p => p.Company.Name == "IBM" && p.Title == "C# Developer").ID, ContactID = context.Contacts.Single(c => c.Company.Name == "IBM" && c.FullName == "John Johnson").ID, IsPrimaryContact = true });
            positionContacts.Add(new PositionContact() { PositionID = context.Positions.Single(p => p.Company.Name == "IBM" && p.Title == "C# Developer").ID, ContactID = context.Contacts.Single(c => c.Company.Name == "IBM" && c.FullName == "Phil Phillips").ID, IsPrimaryContact = false });
            positionContacts.Add(new PositionContact() { PositionID = context.Positions.Single(p => p.Company.Name == "IBM" && p.Title == "Web Developer").ID, ContactID = context.Contacts.Single(c => c.Company.Name == "IBM" && c.FullName == "John Johnson").ID, IsPrimaryContact = true });
            positionContacts.Add(new PositionContact() { PositionID = context.Positions.Single(p => p.Company.Name == "IBM" && p.Title == "Web Developer").ID, ContactID = context.Contacts.Single(c => c.Company.Name == "IBM" && c.FullName == "Phil Phillips").ID, IsPrimaryContact = false });
            positionContacts.Add(new PositionContact() { PositionID = context.Positions.Single(p => p.Company.Name == "IBM" && p.Title == "DBA").ID, ContactID = context.Contacts.Single(c => c.Company.Name == "IBM" && c.FullName == "John Johnson").ID, IsPrimaryContact = true });
            positionContacts.Add(new PositionContact() { PositionID = context.Positions.Single(p => p.Company.Name == "IBM" && p.Title == "DBA").ID, ContactID = context.Contacts.Single(c => c.Company.Name == "IBM" && c.FullName == "Doug Douglas").ID, IsPrimaryContact = false });
            positionContacts.Add(new PositionContact() { PositionID = context.Positions.Single(p => p.Company.Name == "Frank's Books" && p.Title == "WPF Developer").ID, ContactID = context.Contacts.Single(c => c.Company.Name == "Frank's Books" && c.FullName == "Frank Bookman").ID, IsPrimaryContact = true });
            foreach (PositionContact pc in positionContacts)
            {
                context.PositionContacts.Add(pc);
            }
            context.SaveChanges();

        }

    }

}
