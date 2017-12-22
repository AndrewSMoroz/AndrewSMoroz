using static AndrewSMoroz.Enums;
using AndrewSMoroz.CustomExceptions;
using AndrewSMoroz.Data;
using AndrewSMoroz.Models;
using AndrewSMoroz.ViewModels.CompanyViewModels;
using AndrewSMoroz.ViewModels.ContactPhoneViewModels;
using AndrewSMoroz.ViewModels.ContactViewModels;
using AndrewSMoroz.ViewModels.EventViewModels;
using AndrewSMoroz.ViewModels.LookupViewModels;
using AndrewSMoroz.ViewModels.PositionViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.Services
{

    public class BusinessServices : IBusinessServices
    {

        private const string REFERENCE_CONFLICT_ERROR_MESSAGE = "conflicted with the reference constraint";
        private const string REFERENCE_CONFLICT_USER_MESSAGE = "This item cannot be deleted, because it is still being used by at least one other entity.";
        private const string ENTITY_NOT_FOUND_USER_MESSAGE = "Could not find an entity with the specified ID.";

        private readonly ContactsDbContext _contactsDbContext;
        private readonly IModelAdapter _modelAdapter;
        private readonly IUserContext _userContext;

        #region Constructors

        //--------------------------------------------------------------------------------------------------------------
        public BusinessServices(ContactsDbContext contactsDbContext, IModelAdapter modelAdapter, IUserContext userContext)
        {
            _contactsDbContext = contactsDbContext;
            _modelAdapter = modelAdapter;
            _userContext = userContext;
        }

        #endregion Constructors

        #region Business Methods

        //--------------------------------------------------------------------------------------------------------------
        public async Task<IEnumerable<CompanyListViewModel>> GetCompanyListAsync()
        {
            IEnumerable<Company> companies = await GetCompaniesAsync();
            return _modelAdapter.ConvertCompanies(companies);
        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task<CompanyDetailsViewModel> GetCompanyDetailsAsync(int id)
        {
            Company company = await GetCompanyAsync(id);
            return _modelAdapter.ConvertCompany(company);
        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task<IEnumerable<ContactListViewModel>> GetContactListAsync()
        {
            IEnumerable<Contact> contacts = await GetContactsAsync();
            return _modelAdapter.ConvertContacts(contacts);
        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task<IEnumerable<ContactListViewModel>> GetContactListForCompanyAsync(int companyID)
        {
            IEnumerable<Contact> contacts = await GetContactsAsync();
            IEnumerable<Contact> contactsForCompany = null;
            if (contacts != null)
            {
                contactsForCompany = contacts.Where(ct => ct.CompanyID == companyID).ToList();
            }
            return _modelAdapter.ConvertContacts(contactsForCompany);
        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task<ContactDetailsViewModel> GetContactDetailsAsync(int id)
        {
            Contact contact = await GetContactAsync(id);
            return _modelAdapter.ConvertContact(contact);
        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task<ContactPhoneViewModel> GetContactPhoneDetailsAsync(int id)
        {
            ContactPhone contactPhone = await GetContactPhoneAsync(id);
            return _modelAdapter.ConvertContactPhone(contactPhone);
        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task<EventDetailsViewModel> GetEventDetailsAsync(int id)
        {
            Event ev = await GetEventAsync(id);
            return _modelAdapter.ConvertEvent(ev);
        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task<LookupItemViewModel> GetLookupItemAsync(LookupType lookupType, int id)
        {

            LookupItem lookupItem = null;

            switch (lookupType)
            {
                case LookupType.ContactType:
                    // TODO: Consider making this pull from a repository method that returns an IQueryable
                    lookupItem = await _contactsDbContext.ContactTypes.AsNoTracking().SingleOrDefaultAsync(item => item.ID == id && item.UserName == _userContext.UserName);
                    break;
                case LookupType.EventType:
                    // TODO: Consider making this pull from a repository method that returns an IQueryable
                    lookupItem = await _contactsDbContext.EventTypes.AsNoTracking().SingleOrDefaultAsync(item => item.ID == id && item.UserName == _userContext.UserName);
                    break;
                case LookupType.PhoneType:
                    // TODO: Consider making this pull from a repository method that returns an IQueryable
                    lookupItem = await _contactsDbContext.ContactPhoneTypes.AsNoTracking().SingleOrDefaultAsync(item => item.ID == id && item.UserName == _userContext.UserName);
                    break;
                default:
                    break;
            }

            return _modelAdapter.ConvertLookupItem(lookupItem);
        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task<IEnumerable<LookupItemViewModel>> GetLookupListAsync(LookupType lookupType)
        {

            IEnumerable<LookupItem> lookupItems = new List<LookupItem>();

            switch (lookupType)
            {
                case LookupType.ContactType:
                    lookupItems = await GetContactTypesAsync();
                    break;
                case LookupType.EventType:
                    lookupItems = await GetEventTypesAsync();
                    break;
                case LookupType.PhoneType:
                    lookupItems = await GetContactPhoneTypesAsync();
                    break;
                default:
                    break;
            }

            return _modelAdapter.ConvertLookupList(lookupItems);

        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task<IEnumerable<PositionListViewModel>> GetPositionListAsync()
        {
            IEnumerable<Position> positions = await GetPositionsAsync();
            return _modelAdapter.ConvertPositions(positions);
        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task<PositionDetailsViewModel> GetPositionDetailsAsync(int id)
        {
            Position position = await GetPositionAsync(id);
            return _modelAdapter.ConvertPosition(position);
        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task<IEnumerable<StateListViewModel>> GetStateListAsync()
        {
            IEnumerable<State> states = await GetStatesAsync();
            return _modelAdapter.ConvertStates(states);
        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task<int> CreateCompanyAsync(CompanyDetailsViewModel companyDetailsViewModel)
        {

            Company newCompany = _modelAdapter.ConvertCompany(companyDetailsViewModel);

            // TODO: Consider making this pull from a repository method that returns an IQueryable
            Company existingCompanyWithSameName = await _contactsDbContext.Companies
                                                                          .AsNoTracking()
                                                                          .Where(c => (c.Name ?? "").ToUpper() == (newCompany.Name ?? "").ToUpper() && c.UserName == _userContext.UserName)
                                                                          .FirstOrDefaultAsync();

            if (existingCompanyWithSameName != null)
            {
                throw new BusinessException("Could not create new company because the specified company name is already in use.");
            }

            _contactsDbContext.Add(newCompany);
            await _contactsDbContext.SaveChangesAsync();

            return newCompany.ID;

        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task<int> CreateContactAsync(ContactDetailsViewModel contactDetailsViewModel)
        {

            Contact newContact = _modelAdapter.ConvertContact(contactDetailsViewModel);

            _contactsDbContext.Add(newContact);
            await _contactsDbContext.SaveChangesAsync();

            return newContact.ID;

        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task<int> CreateContactPhoneAsync(ContactPhoneViewModel contactPhoneViewModel)
        {

            ContactPhone newContactPhone = _modelAdapter.ConvertContactPhone(contactPhoneViewModel);

            // Check whether this contact already has a phone number marked as primary; if not, make this one primary
            // TODO: Consider making this pull from a repository method that returns an IQueryable
            ContactPhone existingPrimary = await _contactsDbContext.ContactPhones
                                                                   .AsNoTracking()
                                                                   .FirstOrDefaultAsync(cp => cp.ContactID == contactPhoneViewModel.ContactID && cp.IsPrimaryPhone == true && cp.UserName == _userContext.UserName);
            if (existingPrimary == null)
            {
                newContactPhone.IsPrimaryPhone = true;
            }

            _contactsDbContext.Add(newContactPhone);
            await _contactsDbContext.SaveChangesAsync();

            return newContactPhone.ID;

        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task<int> CreateEventAsync(EventDetailsViewModel eventDetailsViewModel)
        {

            Event newEvent = _modelAdapter.ConvertEvent(eventDetailsViewModel);
            _contactsDbContext.Add(newEvent);
            await _contactsDbContext.SaveChangesAsync();
            return newEvent.ID;

        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task<int> CreateLookupItemAsync(LookupType lookupType, LookupItemViewModel lookupItemViewModel)
        {

            LookupItem newLookupItem = _modelAdapter.ConvertLookupItem(lookupItemViewModel, lookupType);
            _contactsDbContext.Add(newLookupItem);
            await _contactsDbContext.SaveChangesAsync();
            return newLookupItem.ID;

        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task<int> CreatePositionAsync(PositionDetailsViewModel positionDetailsViewModel)
        {

            Position newPosition = _modelAdapter.ConvertPosition(positionDetailsViewModel);
            _contactsDbContext.Add(newPosition);
            await _contactsDbContext.SaveChangesAsync();
            return newPosition.ID;

        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task DeleteCompanyAsync(int id)
        {
            // TODO: Consider making this pull from a repository method that returns an IQueryable
            Company existingCompany = await _contactsDbContext.Companies
                                                              .AsNoTracking()
                                                              .SingleOrDefaultAsync(c => c.ID == id && c.UserName == _userContext.UserName);
            if (existingCompany == null)
            {
                throw new BusinessException(ENTITY_NOT_FOUND_USER_MESSAGE);
            }
            Company companyToDelete = _modelAdapter.GetCompany(id);
            await DeleteEntityAsync(companyToDelete);
        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task DeleteContactAsync(int id)
        {
            // TODO: Consider making this pull from a repository method that returns an IQueryable
            Contact existingContact = await _contactsDbContext.Contacts
                                                              .AsNoTracking()
                                                              .SingleOrDefaultAsync(ct => ct.ID == id && ct.UserName == _userContext.UserName);
            if (existingContact == null)
            {
                throw new BusinessException(ENTITY_NOT_FOUND_USER_MESSAGE);
            }
            Contact contactToDelete = _modelAdapter.GetContact(id);
            await DeleteEntityAsync(contactToDelete);
        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task DeleteContactPhoneAsync(int id)
        {
            // TODO: Consider making this pull from a repository method that returns an IQueryable
            ContactPhone existingContactPhone = await _contactsDbContext.ContactPhones
                                                                        .AsNoTracking()
                                                                        .SingleOrDefaultAsync(cp => cp.ID == id && cp.UserName == _userContext.UserName);
            if (existingContactPhone == null)
            {
                throw new BusinessException(ENTITY_NOT_FOUND_USER_MESSAGE);
            }
            ContactPhone contactPhoneToDelete = _modelAdapter.GetContactPhone(id);
            await DeleteEntityAsync(contactPhoneToDelete);
        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task DeleteEventAsync(int id)
        {
            // TODO: Consider making this pull from a repository method that returns an IQueryable
            Event existingEvent = await _contactsDbContext.Events
                                                          .AsNoTracking()
                                                          .SingleOrDefaultAsync(e => e.ID == id && e.UserName == _userContext.UserName);
            if (existingEvent == null)
            {
                throw new BusinessException(ENTITY_NOT_FOUND_USER_MESSAGE);
            }
            Event eventToDelete = _modelAdapter.GetEvent(id);
            await DeleteEntityAsync(eventToDelete);
        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task DeleteLookupItemAsync(LookupType lookupType, int id)
        {
            LookupItem existingLookupItem = null;
            switch (lookupType)
            {
                case LookupType.ContactType:
                    // TODO: Consider making this pull from a repository method that returns an IQueryable
                    existingLookupItem = await _contactsDbContext.ContactTypes.AsNoTracking().SingleOrDefaultAsync(item => item.ID == id && item.UserName == _userContext.UserName);
                    break;
                case LookupType.EventType:
                    // TODO: Consider making this pull from a repository method that returns an IQueryable
                    existingLookupItem = await _contactsDbContext.EventTypes.AsNoTracking().SingleOrDefaultAsync(item => item.ID == id && item.UserName == _userContext.UserName);
                    break;
                case LookupType.PhoneType:
                    // TODO: Consider making this pull from a repository method that returns an IQueryable
                    existingLookupItem = await _contactsDbContext.ContactPhoneTypes.AsNoTracking().SingleOrDefaultAsync(item => item.ID == id && item.UserName == _userContext.UserName);
                    break;
                default:
                    break;
            }

            if (existingLookupItem == null)
            {
                throw new BusinessException(ENTITY_NOT_FOUND_USER_MESSAGE);
            }
            LookupItem lookupItemToDelete = _modelAdapter.GetLookupItem(id, lookupType);
            await DeleteEntityAsync(lookupItemToDelete);
        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task DeletePositionAsync(int id)
        {
            // TODO: Consider making this pull from a repository method that returns an IQueryable
            Position existingPosition = await _contactsDbContext.Positions
                                                                .AsNoTracking()
                                                                .SingleOrDefaultAsync(p => p.ID == id && p.UserName == _userContext.UserName);
            if (existingPosition == null)
            {
                throw new BusinessException(ENTITY_NOT_FOUND_USER_MESSAGE);
            }
            Position positionToDelete = _modelAdapter.GetPosition(id);
            await DeleteEntityAsync(positionToDelete);
        }

        //--------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Set the specified contact phone to be the primary for its contact.
        /// Any others will be set as non-primary.
        /// </summary>
        public async Task SetContactPhoneAsPrimaryAsync(int contactPhoneID)
        {

            // TODO: Consider making this pull from a repository method that returns an IQueryable
            int contactID = await _contactsDbContext.ContactPhones
                                                    .AsNoTracking()
                                                    .Where(cp => cp.ID == contactPhoneID && cp.UserName == _userContext.UserName)
                                                    .Select(cp => cp.ContactID)
                                                    .SingleOrDefaultAsync();

            // TODO: Consider making this pull from a repository method that returns an IQueryable
            Contact contact = await _contactsDbContext.Contacts
                                                      .Include(ct => ct.ContactPhones)
                                                      .SingleOrDefaultAsync(ct => ct.ID == contactID && ct.UserName == _userContext.UserName);

            if (contact == null)
            {
                return;
            }

            foreach (ContactPhone contactPhone in contact.ContactPhones)
            {
                contactPhone.IsPrimaryPhone = (contactPhone.ID == contactPhoneID);
                _contactsDbContext.Entry(contactPhone).State = EntityState.Modified;
            }

            _contactsDbContext.SaveChanges();

        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task UpdateCompanyAsync(CompanyDetailsViewModel companyDetailsViewModel)
        {

            Company companyToUpdate = _modelAdapter.ConvertCompany(companyDetailsViewModel);

            // TODO: Consider making this pull from a repository method that returns an IQueryable
            Company existingCompany = await _contactsDbContext.Companies
                                                              .AsNoTracking()
                                                              .SingleOrDefaultAsync(c => c.ID == companyToUpdate.ID && c.UserName == _userContext.UserName);
            if (existingCompany == null)
            {
                throw new BusinessException(ENTITY_NOT_FOUND_USER_MESSAGE);
            }

            // TODO: Consider making this pull from a repository method that returns an IQueryable
            Company existingCompanyWithSameName = await _contactsDbContext.Companies
                                                                          .AsNoTracking()
                                                                          .Where(c => (c.Name ?? "").ToUpper() == (companyToUpdate.Name ?? "").ToUpper() && c.ID != companyToUpdate.ID && c.UserName == _userContext.UserName)
                                                                          .FirstOrDefaultAsync();

            if (existingCompanyWithSameName != null)
            {
                throw new BusinessException("Could not update because the specified company name is already in use.");
            }

            _contactsDbContext.Update(companyToUpdate);
            await _contactsDbContext.SaveChangesAsync();

        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task UpdateContactAsync(ContactDetailsViewModel contactDetailsViewModel)
        {

            Contact contactToUpdate = _modelAdapter.ConvertContact(contactDetailsViewModel);

            // TODO: Consider making this pull from a repository method that returns an IQueryable
            Contact existingContact = await _contactsDbContext.Contacts
                                                              .AsNoTracking()
                                                              .SingleOrDefaultAsync(c => c.ID == contactToUpdate.ID && c.UserName == _userContext.UserName);
            if (existingContact == null)
            {
                throw new BusinessException(ENTITY_NOT_FOUND_USER_MESSAGE);
            }

            _contactsDbContext.Update(contactToUpdate);
            await _contactsDbContext.SaveChangesAsync();

        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task UpdateContactPhoneAsync(ContactPhoneViewModel contactPhoneViewModel)
        {

            ContactPhone contactPhoneToUpdate = _modelAdapter.ConvertContactPhone(contactPhoneViewModel);

            // TODO: Consider making this pull from a repository method that returns an IQueryable
            ContactPhone existingContactPhoneEntity = await _contactsDbContext.ContactPhones
                                                                              .AsNoTracking()
                                                                              .SingleOrDefaultAsync(c => c.ID == contactPhoneToUpdate.ID && c.UserName == _userContext.UserName);
            if (existingContactPhoneEntity == null)
            {
                throw new BusinessException(ENTITY_NOT_FOUND_USER_MESSAGE);
            }

            // If the contact has no existing phones, mark the new one as primary
            ContactPhone existingContactPhone = _contactsDbContext.ContactPhones
                                                                  .AsNoTracking()
                                                                  .SingleOrDefault(cp => cp.ID == contactPhoneToUpdate.ID);
            if (existingContactPhone != null)
            {
                contactPhoneToUpdate.IsPrimaryPhone = existingContactPhone.IsPrimaryPhone;
            }

            _contactsDbContext.Update(contactPhoneToUpdate);
            await _contactsDbContext.SaveChangesAsync();

        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task UpdateEventAsync(EventDetailsViewModel eventDetailsViewModel)
        {

            Event eventToUpdate = _modelAdapter.ConvertEvent(eventDetailsViewModel);

            // TODO: Consider making this pull from a repository method that returns an IQueryable
            Event existingEvent = await _contactsDbContext.Events
                                                          .AsNoTracking()
                                                          .SingleOrDefaultAsync(c => c.ID == eventToUpdate.ID && c.UserName == _userContext.UserName);
            if (existingEvent == null)
            {
                throw new BusinessException(ENTITY_NOT_FOUND_USER_MESSAGE);
            }

            _contactsDbContext.Update(eventToUpdate);
            await _contactsDbContext.SaveChangesAsync();

        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task UpdateLookupItemAsync(LookupType lookupType, LookupItemViewModel lookupItemViewModel)
        {

            LookupItem lookupItemToUpdate = _modelAdapter.ConvertLookupItem(lookupItemViewModel, lookupType);

            LookupItem existingLookupItem = null;
            switch (lookupType)
            {
                case LookupType.ContactType:
                    // TODO: Consider making this pull from a repository method that returns an IQueryable
                    existingLookupItem = await _contactsDbContext.ContactTypes.AsNoTracking().SingleOrDefaultAsync(item => item.ID == lookupItemToUpdate.ID && item.UserName == _userContext.UserName);
                    break;
                case LookupType.EventType:
                    // TODO: Consider making this pull from a repository method that returns an IQueryable
                    existingLookupItem = await _contactsDbContext.EventTypes.AsNoTracking().SingleOrDefaultAsync(item => item.ID == lookupItemToUpdate.ID && item.UserName == _userContext.UserName);
                    break;
                case LookupType.PhoneType:
                    // TODO: Consider making this pull from a repository method that returns an IQueryable
                    existingLookupItem = await _contactsDbContext.ContactPhoneTypes.AsNoTracking().SingleOrDefaultAsync(item => item.ID == lookupItemToUpdate.ID && item.UserName == _userContext.UserName);
                    break;
                default:
                    break;
            }

            if (existingLookupItem == null)
            {
                throw new BusinessException(ENTITY_NOT_FOUND_USER_MESSAGE);
            }

            _contactsDbContext.Update(lookupItemToUpdate);
            await _contactsDbContext.SaveChangesAsync();

        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task UpdatePositionAsync(PositionDetailsViewModel positionDetailsViewModel)
        {

            Position newPosition = _modelAdapter.ConvertPosition(positionDetailsViewModel);

            // TODO: Consider making this pull from a repository method that returns an IQueryable
            Position existingPosition = await _contactsDbContext.Positions
                                                                .AsNoTracking()
                                                                .SingleOrDefaultAsync(c => c.ID == newPosition.ID && c.UserName == _userContext.UserName);
            if (existingPosition == null)
            {
                throw new BusinessException(ENTITY_NOT_FOUND_USER_MESSAGE);
            }

            _contactsDbContext.Update(newPosition);

            // TODO: Consider making this pull from a repository method that returns an IQueryable
            List<int> existingContactIDs = await _contactsDbContext.PositionContacts
                                                                  .AsNoTracking()
                                                                  .Where(pc => pc.PositionID == positionDetailsViewModel.ID)
                                                                  .Select(pc => pc.ContactID)
                                                                  .ToListAsync();

            // TODO: Consider making this pull from a repository method that returns an IQueryable
            List<int> validContactsForCompany = await _contactsDbContext.Contacts
                                                                        .AsNoTracking()
                                                                        .Where(ct => ct.CompanyID == positionDetailsViewModel.CompanyID && ct.UserName == _userContext.UserName)
                                                                        .Select(ct => ct.ID)
                                                                        .ToListAsync();

            // Add any new PositionContacts not already in the database
            foreach (PositionContact pc in newPosition.PositionContacts)
            {
                if (!validContactsForCompany.Contains(pc.ContactID))
                {
                    continue;       // Don't add any IDs that belong to Contacts at a different company
                }
                if (!existingContactIDs.Contains(pc.ContactID))
                {
                    _contactsDbContext.Entry(pc).State = EntityState.Added;
                }
            }

            // Remove any existing PositionContacts from the database as necessary
            foreach (int contactID in existingContactIDs)
            {
                PositionContact pc = newPosition.PositionContacts.FirstOrDefault(o => o.ContactID == contactID);
                if (pc == null)
                {
                    // This PositionContact is in the database, but not in the new collection, so mark it for deletion
                    PositionContact pcToRemove = new PositionContact() { PositionID = newPosition.ID, ContactID = contactID };
                    newPosition.PositionContacts.Add(pcToRemove);
                    _contactsDbContext.Remove(pcToRemove);
                }
                else
                {
                    // This PositionContact is in the database and in the new collection.
                    // Mark it for deletion if it is for a contact at another company
                    if (!validContactsForCompany.Contains(contactID)) {
                        _contactsDbContext.Remove(pc);
                    }
                }
            }
            
            await _contactsDbContext.SaveChangesAsync();

        }

        #endregion Business Methods

        #region Repository Methods

        //--------------------------------------------------------------------------------------------------------------
        private async Task DeleteEntityAsync(object entityToDelete)
        {

            try
            {
                _contactsDbContext.Remove(entityToDelete);
                await _contactsDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException duex)
            {
                if (duex.InnerException != null && duex.InnerException.GetType() == typeof(System.Data.SqlClient.SqlException))
                {
                    if ((duex.InnerException.Message ?? string.Empty).ToLower().Contains(REFERENCE_CONFLICT_ERROR_MESSAGE))
                    {
                        // In this scenario, throw a BusinessException so our specific user-friendly error message
                        // will get displayed by the UI
                        throw new BusinessException(REFERENCE_CONFLICT_USER_MESSAGE);
                    }
                }
                else
                {
                    throw duex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //--------------------------------------------------------------------------------------------------------------
        private async Task<List<Company>> GetCompaniesAsync()
        {
            return await _contactsDbContext.Companies
                                           .AsNoTracking()
                                           .Where(c => c.UserName == _userContext.UserName)
                                           .OrderBy(c => c.Name)
                                           .ToListAsync();
        }

        //--------------------------------------------------------------------------------------------------------------
        private async Task<Company> GetCompanyAsync(int id)
        {
            return await _contactsDbContext.Companies
                                           .AsNoTracking()
                                           .Where(c => c.UserName == _userContext.UserName)
                                           .Include(c => c.Positions)
                                               .ThenInclude(p => p.Events)
                                                   .ThenInclude(ev => ev.EventType)
                                           .Include(c => c.Contacts)
                                               .ThenInclude(ct => ct.ContactType)
                                           .Include(c => c.Contacts)
                                               .ThenInclude(ct => ct.ContactPhones)
                                                   .ThenInclude(cp => cp.ContactPhoneType)
                                           .SingleOrDefaultAsync(c => c.ID == id);
        }

        //--------------------------------------------------------------------------------------------------------------
        private async Task<List<Contact>> GetContactsAsync()
        {
            return await _contactsDbContext.Contacts
                                           .Where(ct => ct.UserName == _userContext.UserName)
                                           .AsNoTracking()
                                           .Include(ct => ct.Company)
                                           .Include(ct => ct.ContactType)
                                           .Include(ct => ct.ContactPhones)
                                               .ThenInclude(cp => cp.ContactPhoneType)
                                           .OrderBy(ct => ct.Company.Name)
                                           .ThenBy(ct => ct.LastName)
                                           .ThenBy(ct => ct.FirstName)
                                           .ToListAsync();
        }

        //--------------------------------------------------------------------------------------------------------------
        private async Task<Contact> GetContactAsync(int id)
        {
            return await _contactsDbContext.Contacts
                                           .Where(ct => ct.UserName == _userContext.UserName)
                                           .AsNoTracking()
                                           .Include(ct => ct.Company)
                                           .Include(ct => ct.ContactType)
                                           .Include(ct => ct.ContactPhones)
                                               .ThenInclude(cp => cp.ContactPhoneType)
                                           .OrderBy(ct => ct.Company.Name)
                                           .ThenBy(ct => ct.LastName)
                                           .ThenBy(ct => ct.FirstName)
                                           .SingleOrDefaultAsync(ct => ct.ID == id);
        }

        //--------------------------------------------------------------------------------------------------------------
        private async Task<List<ContactType>> GetContactTypesAsync()
        {
            return await _contactsDbContext.ContactTypes
                                           .Where(ctt => ctt.UserName == _userContext.UserName)
                                           .AsNoTracking()
                                           .OrderBy(ctt => ctt.Sequence)
                                           .ToListAsync();
        }

        //--------------------------------------------------------------------------------------------------------------
        private async Task<List<ContactPhoneType>> GetContactPhoneTypesAsync()
        {
            return await _contactsDbContext.ContactPhoneTypes
                                           .Where(cpt => cpt.UserName == _userContext.UserName)
                                           .AsNoTracking()
                                           .OrderBy(cpt => cpt.Sequence)
                                           .ToListAsync();
        }

        //--------------------------------------------------------------------------------------------------------------
        private async Task<ContactPhone> GetContactPhoneAsync(int id)
        {
            return await _contactsDbContext.ContactPhones
                                           .Where(cp => cp.UserName == _userContext.UserName)
                                           .AsNoTracking()
                                           .Include(cp => cp.ContactPhoneType)
                                           .Include(cp => cp.Contact)
                                           .SingleOrDefaultAsync(cp => cp.ID == id);
        }

        //--------------------------------------------------------------------------------------------------------------
        private async Task<Event> GetEventAsync(int id)
        {
            return await _contactsDbContext.Events
                                           .Where(e => e.UserName == _userContext.UserName)
                                           .AsNoTracking()
                                           .Include(e => e.EventType)
                                           .Include(e => e.Position)
                                               .ThenInclude(p => p.Company)
                                           .SingleOrDefaultAsync(e => e.ID == id);
        }

        //--------------------------------------------------------------------------------------------------------------
        private async Task<List<EventType>> GetEventTypesAsync()
        {
            return await _contactsDbContext.EventTypes
                                           .Where(et => et.UserName == _userContext.UserName)
                                           .AsNoTracking()
                                           .OrderBy(et => et.Sequence)
                                           .ToListAsync();
        }

        //--------------------------------------------------------------------------------------------------------------
        private async Task<List<Position>> GetPositionsAsync()
        {
            return await _contactsDbContext.Positions
                                           .Where(p => p.UserName == _userContext.UserName)
                                           .AsNoTracking()
                                           .Include(p => p.Company)
                                           .Include(p => p.Events)
                                               .ThenInclude(ev => ev.EventType)
                                           .OrderBy(p => p.Company.Name)
                                           .ThenBy(p => p.Title)
                                           .ToListAsync();
        }

        //--------------------------------------------------------------------------------------------------------------
        private async Task<Position> GetPositionAsync(int id)
        {
            return await _contactsDbContext.Positions
                                           .Where(p => p.UserName == _userContext.UserName)
                                           .AsNoTracking()
                                           .Include(p => p.Events)
                                               .ThenInclude(e => e.EventType)
                                           .Include(p => p.Company)
                                           .Include(p => p.PositionContacts)
                                               .ThenInclude(pc => pc.Contact)
                                                   .ThenInclude(ct => ct.Company)
                                           .Include(p => p.PositionContacts)
                                               .ThenInclude(pc => pc.Contact)
                                                   .ThenInclude(ct => ct.ContactType)
                                           .Include(p => p.PositionContacts)
                                               .ThenInclude(pc => pc.Contact)
                                                   .ThenInclude(ct => ct.ContactPhones)
                                                       .ThenInclude(cp => cp.ContactPhoneType)
                                           .SingleOrDefaultAsync(p => p.ID == id);
        }

        //--------------------------------------------------------------------------------------------------------------
        private async Task<List<State>> GetStatesAsync()
        {
            return await _contactsDbContext.States
                                           .AsNoTracking()
                                           .OrderBy(s => s.Abbreviation)
                                           .ToListAsync();
        }

        #endregion Repository Methods

        #region Other

        public async Task PopulateDataForNewUser(string userName)
        {

            if (userName.Equals(ContactsDbInitializer.USER_NAME, StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            //----------------------------------------------------------------------------------------------------------
            EventType[] eventTypes = new EventType[]
            {
                new EventType { Sequence = 10, Description = "Applied", UserName = userName },
                new EventType { Sequence = 20, Description = "Submitted Resume", UserName = userName },
                new EventType { Sequence = 30, Description = "Phone Interview", UserName = userName },
                new EventType { Sequence = 40, Description = "In-Person Interview", UserName = userName },
                new EventType { Sequence = 50, Description = "Follow-Up Call", UserName = userName },
                new EventType { Sequence = 60, Description = "Removed from Consideration", UserName = userName },
            };
            foreach (EventType et in eventTypes)
            {
                _contactsDbContext.EventTypes.Add(et);
            }
            await _contactsDbContext.SaveChangesAsync();

            //----------------------------------------------------------------------------------------------------------
            ContactType[] contactTypes = new ContactType[]
            {
                new ContactType { Sequence = 10, Description = "HR", UserName = userName },
                new ContactType { Sequence = 20, Description = "Software Development Manager", UserName = userName },
                new ContactType { Sequence = 30, Description = "Other Manager", UserName = userName },
                new ContactType { Sequence = 40, Description = "Developer", UserName = userName },
                new ContactType { Sequence = 50, Description = "QA", UserName = userName },
                new ContactType { Sequence = 60, Description = "Owner", UserName = userName },
                new ContactType { Sequence = 70, Description = "Recruiter", UserName = userName },
            };
            foreach (ContactType ct in contactTypes)
            {
                _contactsDbContext.ContactTypes.Add(ct);
            }
            await _contactsDbContext.SaveChangesAsync();

            //----------------------------------------------------------------------------------------------------------
            ContactPhoneType[] contactPhoneTypes = new ContactPhoneType[]
            {
                new ContactPhoneType { Sequence = 10, Description = "Office", UserName = userName },
                new ContactPhoneType { Sequence = 20, Description = "Work Mobile", UserName = userName },
                new ContactPhoneType { Sequence = 30, Description = "Personal Mobile", UserName = userName },
                new ContactPhoneType { Sequence = 40, Description = "Home", UserName = userName },
            };
            foreach (ContactPhoneType cpt in contactPhoneTypes)
            {
                _contactsDbContext.ContactPhoneTypes.Add(cpt);
            }
            await _contactsDbContext.SaveChangesAsync();

        }

        #endregion Other

    }

}
