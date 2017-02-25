using static AndrewSMoroz.Enums;
using AndrewSMoroz.Models;
using AndrewSMoroz.ViewModels.CompanyViewModels;
using AndrewSMoroz.ViewModels.ContactViewModels;
using AndrewSMoroz.ViewModels.ContactPhoneViewModels;
using AndrewSMoroz.ViewModels.EventViewModels;
using AndrewSMoroz.ViewModels.PositionViewModels;
using AndrewSMoroz.ViewModels.LookupViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.Services
{

    /// <summary>
    /// This class converts domain model objects to their counterpart UI view models, and vice versa.
    /// The UI view models may not contain all domain fields for reasons of security or brevity;
    /// they also may contain derived or user-input fields that aren't stored in the database.
    /// </summary>
    public class ModelAdapter : IModelAdapter
    {

        private readonly IUserContext _userContext;

        //--------------------------------------------------------------------------------------------------------------
        public ModelAdapter(IUserContext userContext)
        {
            _userContext = userContext;
        }

        #region Companies

        //--------------------------------------------------------------------------------------------------------------
        public IEnumerable<CompanyListViewModel> ConvertCompanies(IEnumerable<Company> companies)
        {

            List<CompanyListViewModel> viewModels = new List<CompanyListViewModel>();

            if (companies == null)
            {
                return viewModels;
            }

            foreach (Company company in companies)
            {
                CompanyListViewModel vm = new CompanyListViewModel()
                {
                    Address = company.Address,
                    City = company.City,
                    ID = company.ID,
                    Name = company.Name,
                    PostalCode = company.PostalCode,
                    State = company.State
                };

                viewModels.Add(vm);
            }

            return viewModels;

        }

        //--------------------------------------------------------------------------------------------------------------
        public CompanyDetailsViewModel ConvertCompany(Company company)
        {

            if (company == null)
            {
                return new CompanyDetailsViewModel();
            }

            CompanyDetailsViewModel viewModel = new CompanyDetailsViewModel()
            {
                Address = company.Address,
                City = company.City,
                Contacts = ConvertContacts(company.Contacts),
                ID = company.ID,
                Name = company.Name,
                Positions = ConvertPositions(company.Positions),
                PostalCode = company.PostalCode,
                State = company.State
            };

            return viewModel;

        }

        //--------------------------------------------------------------------------------------------------------------
        public Company ConvertCompany(CompanyDetailsViewModel companyDetailsViewModel)
        {

            if (companyDetailsViewModel == null)
            {
                return new Company();
            }

            Company company = new Company()
            {
                Address = companyDetailsViewModel.Address,
                City = companyDetailsViewModel.City,
                ID = companyDetailsViewModel.ID,
                Name = companyDetailsViewModel.Name,
                PostalCode = companyDetailsViewModel.PostalCode,
                State = companyDetailsViewModel.State,
                UserName = _userContext.UserName
            };

            return company;

        }

        //--------------------------------------------------------------------------------------------------------------
        public Company GetCompany(int id)
        {
            return new Company() {
                ID = id,
                UserName = _userContext.UserName
            };
        }

        #endregion Companies

        #region Contacts

        //--------------------------------------------------------------------------------------------------------------
        public IEnumerable<ContactListViewModel> ConvertContacts(IEnumerable<Contact> contacts)
        {

            List<ContactListViewModel> viewModels = new List<ContactListViewModel>();

            if (contacts == null)
            {
                return viewModels;
            }

            foreach (Contact contact in contacts)
            {
                ContactListViewModel vm = new ContactListViewModel()
                {
                    CompanyName = contact.Company.Name,
                    CompanyID = contact.CompanyID,
                    ContactType = contact.ContactType.Description,
                    FullName = contact.FullName,
                    ID = contact.ID,
                };
                ContactPhone primaryContactPhone = contact.ContactPhones.FirstOrDefault(ct => ct.IsPrimaryPhone);
                if (primaryContactPhone != null)
                {
                    vm.PrimaryPhone = primaryContactPhone.PhoneNumber + " " + (primaryContactPhone.Extension ?? "");
                    vm.PrimaryPhoneType = primaryContactPhone.ContactPhoneType.Description;
                }
                viewModels.Add(vm);
            }

            return viewModels;

        }

        //--------------------------------------------------------------------------------------------------------------
        public ContactDetailsViewModel ConvertContact(Contact contact)
        {

            if (contact == null)
            {
                return new ContactDetailsViewModel();
            }

            ContactDetailsViewModel viewModel = new ContactDetailsViewModel()
            {
                CompanyID = contact.CompanyID,
                CompanyName = contact.Company.Name,
                ContactType = contact.ContactType.Description,
                ContactTypeID = contact.ContactTypeID,
                FirstName = contact.FirstName,
                FullName = contact.FullName,
                ID = contact.ID,
                LastName = contact.LastName,
                PhoneNumbers = ConvertContactPhones(contact.ContactPhones)
            };

            return viewModel;

        }

        //--------------------------------------------------------------------------------------------------------------
        public Contact ConvertContact(ContactDetailsViewModel contactDetailsViewModel)
        {

            if (contactDetailsViewModel == null)
            {
                return new Contact();
            }

            Contact contact = new Contact()
            {
                CompanyID = contactDetailsViewModel.CompanyID,
                ContactTypeID = contactDetailsViewModel.ContactTypeID,
                FirstName = contactDetailsViewModel.FirstName,
                ID = contactDetailsViewModel.ID,
                LastName = contactDetailsViewModel.LastName,
                UserName = _userContext.UserName
            };

            return contact;

        }

        //--------------------------------------------------------------------------------------------------------------
        public Contact GetContact(int id)
        {
            return new Contact() {
                ID = id,
                UserName = _userContext.UserName
            };
        }

        #endregion Contacts

        #region Contact Phones

        //--------------------------------------------------------------------------------------------------------------
        public ContactPhoneViewModel ConvertContactPhone(ContactPhone contactPhone)
        {

            if (contactPhone == null)
            {
                return new ContactPhoneViewModel();
            }

            ContactPhoneViewModel viewModel = new ContactPhoneViewModel()
            {
                ContactFullName = contactPhone.Contact.FullName,
                ContactID = contactPhone.ContactID,
                ContactPhoneType = contactPhone.ContactPhoneType.Description,
                ContactPhoneTypeID = contactPhone.ContactPhoneTypeID,
                Extension = contactPhone.Extension,
                ID = contactPhone.ID,
                IsPrimaryPhone = contactPhone.IsPrimaryPhone,
                PhoneNumber = contactPhone.PhoneNumber
            };

            return viewModel;

        }

        //--------------------------------------------------------------------------------------------------------------
        public IEnumerable<ContactPhoneViewModel> ConvertContactPhones(IEnumerable<ContactPhone> contactPhones)
        {

            List<ContactPhoneViewModel> viewModels = new List<ContactPhoneViewModel>();

            if (contactPhones == null)
            {
                return viewModels;
            }

            foreach (ContactPhone contactPhone in contactPhones)
            {
                ContactPhoneViewModel vm = new ContactPhoneViewModel()
                {
                    ContactID = contactPhone.ContactID,
                    ContactFullName = contactPhone.Contact.FullName,
                    ContactPhoneType = contactPhone.ContactPhoneType.Description,
                    ContactPhoneTypeID = contactPhone.ContactPhoneTypeID,
                    Extension = contactPhone.Extension,
                    ID = contactPhone.ID,
                    IsPrimaryPhone = contactPhone.IsPrimaryPhone,
                    PhoneNumber = contactPhone.PhoneNumber
                };
                viewModels.Add(vm);
            }

            return viewModels;

        }

        //--------------------------------------------------------------------------------------------------------------
        public ContactPhone ConvertContactPhone(ContactPhoneViewModel contactPhoneViewModel)
        {

            if (contactPhoneViewModel == null)
            {
                return new ContactPhone();
            }

            ContactPhone contactPhone = new ContactPhone()
            {
                ContactID = contactPhoneViewModel.ContactID,
                ContactPhoneTypeID = contactPhoneViewModel.ContactPhoneTypeID,
                Extension = contactPhoneViewModel.Extension,
                ID = contactPhoneViewModel.ID,
                IsPrimaryPhone = contactPhoneViewModel.IsPrimaryPhone,
                PhoneNumber = contactPhoneViewModel.PhoneNumber,
                UserName = _userContext.UserName
            };

            return contactPhone;

        }

        //--------------------------------------------------------------------------------------------------------------
        public ContactPhone GetContactPhone(int id)
        {
            return new ContactPhone() {
                ID = id,
                UserName = _userContext.UserName
            };
        }

        #endregion ContactPhones

        #region Events

        //--------------------------------------------------------------------------------------------------------------
        public EventDetailsViewModel ConvertEvent(Event ev)
        {

            if (ev == null)
            {
                return new EventDetailsViewModel();
            }

            EventDetailsViewModel viewModel = new EventDetailsViewModel()
            {
                CompanyName = ev.Position.Company.Name,
                Date = ev.DateTime,
                Description = ev.Description,
                EventTypeID = ev.EventTypeID,
                ID = ev.ID,
                PositionID = ev.PositionID,
                PositionTitle = ev.Position.Title,
                Time = ev.DateTime
            };

            return viewModel;

        }

        //--------------------------------------------------------------------------------------------------------------
        public IEnumerable<EventListViewModel> ConvertEvents(IEnumerable<Event> events)
        {

            List<EventListViewModel> viewModels = new List<EventListViewModel>();

            if (events == null)
            {
                return viewModels;
            }

            foreach (Event ev in events)
            {
                EventListViewModel vm = new EventListViewModel()
                {
                    DateTime = ev.DateTime,
                    Description = ev.Description,
                    EventType = ev.EventType.Description,
                    ID = ev.ID
                };
                viewModels.Add(vm);
            }

            return viewModels.OrderBy(ev => ev.DateTime);

        }

        //--------------------------------------------------------------------------------------------------------------
        public Event ConvertEvent(EventDetailsViewModel eventDetailsViewModel)
        {

            if (eventDetailsViewModel == null)
            {
                return new Event();
            }

            Event newEvent = new Event()
            {
                Description = eventDetailsViewModel.Description,
                EventTypeID = eventDetailsViewModel.EventTypeID,
                ID = eventDetailsViewModel.ID,
                PositionID = eventDetailsViewModel.PositionID,
                UserName = _userContext.UserName
            };

            // Combine the separate Date and Time fields into a single value for the DateTime property
            if (eventDetailsViewModel.Date.HasValue)
            {
                newEvent.DateTime = new DateTime(
                    eventDetailsViewModel.Date.Value.Year,
                    eventDetailsViewModel.Date.Value.Month,
                    eventDetailsViewModel.Date.Value.Day,
                    (eventDetailsViewModel.Time.HasValue ? eventDetailsViewModel.Time.Value.Hour : 0),
                    (eventDetailsViewModel.Time.HasValue ? eventDetailsViewModel.Time.Value.Minute : 0),
                    0
                );
            }
            else
            {
                newEvent.DateTime = DateTime.Now;
            }

            return newEvent;

        }

        //--------------------------------------------------------------------------------------------------------------
        public Event GetEvent(int id)
        {
            return new Models.Event() {
                ID = id,
                UserName = _userContext.UserName
            };
        }

        #endregion Events

        #region Positions

        //--------------------------------------------------------------------------------------------------------------
        public IEnumerable<PositionListViewModel> ConvertPositions(IEnumerable<Position> positions)
        {

            List<PositionListViewModel> viewModels = new List<PositionListViewModel>();

            if (positions == null)
            {
                return viewModels;
            }

            foreach (Position position in positions)
            {
                PositionListViewModel vm = new PositionListViewModel()
                {
                    CompanyID = position.CompanyID,
                    CompanyName = position.Company.Name,
                    DatePosted = position.DatePosted,
                    Description = position.Description,
                    ID = position.ID,
                    Title = position.Title,
                };
                Event mostRecentEvent = position.Events.OrderByDescending(ev => ev.DateTime).FirstOrDefault();
                if (mostRecentEvent != null)
                {
                    vm.MostRecentEventDateTime = mostRecentEvent.DateTime;
                    vm.MostRecentEventType = mostRecentEvent.EventType.Description;
                }
                viewModels.Add(vm);
            }


            return viewModels;

        }

        //--------------------------------------------------------------------------------------------------------------
        public PositionDetailsViewModel ConvertPosition(Position position)
        {

            if (position == null)
            {
                return new PositionDetailsViewModel();
            }

            PositionDetailsViewModel viewModel = new PositionDetailsViewModel()
            {
                CompanyName = position.Company.Name,
                CompanyID = position.CompanyID,
                Contacts = ConvertContacts(position.PositionContacts.Select(pc => pc.Contact)),
                DatePosted = position.DatePosted,
                Description = position.Description,
                Events = ConvertEvents(position.Events),
                ID = position.ID,
                Title = position.Title
            };

            if (viewModel.Contacts != null)
            {
                viewModel.ContactIDs = viewModel.Contacts.Select(ct => ct.ID).ToList();
            }

            return viewModel;

        }

        //--------------------------------------------------------------------------------------------------------------
        public Position ConvertPosition(PositionDetailsViewModel positionDetailsViewModel)
        {

            if (positionDetailsViewModel == null)
            {
                return new Position();
            }

            Position position = new Position()
            {
                CompanyID = positionDetailsViewModel.CompanyID,
                DatePosted = positionDetailsViewModel.DatePosted,
                Description = positionDetailsViewModel.Description,
                ID = positionDetailsViewModel.ID,
                PositionContacts = new List<PositionContact>(),
                Title = positionDetailsViewModel.Title,
                UserName = _userContext.UserName
            };

            if (positionDetailsViewModel.ContactIDs != null && positionDetailsViewModel.ContactIDs.Any())
            {
                foreach(int contactID in positionDetailsViewModel.ContactIDs)
                {
                    position.PositionContacts.Add(new PositionContact() { PositionID = position.ID, ContactID = contactID, IsPrimaryContact = false });
                }
            }

            return position;

        }

        //--------------------------------------------------------------------------------------------------------------
        public Position GetPosition(int id)
        {
            return new Position() {
                ID = id,
                UserName = _userContext.UserName
            };
        }

        #endregion Positions

        #region Lookups

        //--------------------------------------------------------------------------------------------------------------
        public LookupItemViewModel ConvertLookupItem(LookupItem lookupItem)
        {

            if (lookupItem == null)
            {
                return new LookupItemViewModel();
            }

            LookupItemViewModel viewModel = new LookupItemViewModel()
            {
                Description = lookupItem.Description,
                ID = lookupItem.ID,
                Sequence = lookupItem.Sequence
            };

            return viewModel;

        }

        //--------------------------------------------------------------------------------------------------------------
        public IEnumerable<LookupItemViewModel> ConvertLookupList(IEnumerable<LookupItem> lookupItems)
        {

            List<LookupItemViewModel> viewModels = new List<LookupItemViewModel>();

            if (lookupItems == null)
            {
                return viewModels;
            }

            foreach (LookupItem lookupItem in lookupItems)
            {
                LookupItemViewModel vm = new LookupItemViewModel()
                {
                    Description = lookupItem.Description,
                    ID = lookupItem.ID,
                    Sequence = lookupItem.Sequence
                };
                viewModels.Add(vm);
            }

            return viewModels;

        }

        //--------------------------------------------------------------------------------------------------------------
        public LookupItem ConvertLookupItem(LookupItemViewModel lookupItemViewModel, LookupType lookupType)
        {

            // TODO: Look into implementing this as a generic method instead

            if (lookupItemViewModel == null)
            {
                return new LookupItem();
            }

            // Must return a specific model type (not the LookupItem superclass type) so Entity Framework
            // will know which table to work with
            LookupItem lookupItem = new LookupItem();
            switch (lookupType)
            {
                case LookupType.ContactType:
                    lookupItem = new ContactType();
                    break;
                case LookupType.EventType:
                    lookupItem = new EventType();
                    break;
                case LookupType.PhoneType:
                    lookupItem = new ContactPhoneType();
                    break;
                default:
                    break;
            }

            lookupItem.Description = lookupItemViewModel.Description;
            lookupItem.ID = lookupItemViewModel.ID;
            lookupItem.Sequence = lookupItemViewModel.Sequence;
            lookupItem.UserName = _userContext.UserName;

            return lookupItem;

        }

        //--------------------------------------------------------------------------------------------------------------
        public LookupItem GetLookupItem(int id, LookupType lookupType)
        {

            // Must return a specific model type (not the LookupItem superclass type) so Entity Framework
            // will know which table to work with
            LookupItem lookupItem = new LookupItem();
            switch (lookupType)
            {
                case LookupType.ContactType:
                    lookupItem = new ContactType();
                    break;
                case LookupType.EventType:
                    lookupItem = new EventType();
                    break;
                case LookupType.PhoneType:
                    lookupItem = new ContactPhoneType();
                    break;
                default:
                    break;
            }

            lookupItem.ID = id;
            lookupItem.UserName = _userContext.UserName;

            return lookupItem;

        }

        //--------------------------------------------------------------------------------------------------------------
        public IEnumerable<StateListViewModel> ConvertStates(IEnumerable<State> states)
        {

            List<StateListViewModel> viewModels = new List<StateListViewModel>();

            if (states == null)
            {
                return viewModels;
            }

            foreach (State state in states)
            {
                StateListViewModel vm = new StateListViewModel()
                {
                    Abbreviation = state.Abbreviation,
                    ID = state.ID,
                    Name = state.Name,
                    Sequence = state.Sequence
                };
                viewModels.Add(vm);
            }

            return viewModels;

        }
        
        #endregion Lookups

    }

}
