using static AndrewSMoroz.Enums;
using AndrewSMoroz.ViewModels.CompanyViewModels;
using AndrewSMoroz.ViewModels.ContactPhoneViewModels;
using AndrewSMoroz.ViewModels.ContactViewModels;
using AndrewSMoroz.ViewModels.EventViewModels;
using AndrewSMoroz.ViewModels.LookupViewModels;
using AndrewSMoroz.ViewModels.PositionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.Services
{

    public interface IBusinessServices
    {

        Task<IEnumerable<CompanyListViewModel>> GetCompanyListAsync();
        Task<CompanyDetailsViewModel> GetCompanyDetailsAsync(int id);
        Task<IEnumerable<ContactListViewModel>> GetContactListAsync();
        Task<IEnumerable<ContactListViewModel>> GetContactListForCompanyAsync(int companyID);
        Task<ContactDetailsViewModel> GetContactDetailsAsync(int id);
        Task<ContactPhoneViewModel> GetContactPhoneDetailsAsync(int id);
        Task<EventDetailsViewModel> GetEventDetailsAsync(int id);
        Task<LookupItemViewModel> GetLookupItemAsync(LookupType lookupType, int id);
        Task<IEnumerable<LookupItemViewModel>> GetLookupListAsync(LookupType lookupType);
        Task<IEnumerable<PositionListViewModel>> GetPositionListAsync();
        Task<PositionDetailsViewModel> GetPositionDetailsAsync(int id);
        Task<IEnumerable<StateListViewModel>> GetStateListAsync();
        Task<int> CreateCompanyAsync(CompanyDetailsViewModel companyDetailsViewModel);
        Task<int> CreateContactAsync(ContactDetailsViewModel contactDetailsViewModel);
        Task<int> CreateContactPhoneAsync(ContactPhoneViewModel contactPhoneViewModel);
        Task<int> CreateEventAsync(EventDetailsViewModel eventDetailsViewModel);
        Task<int> CreateLookupItemAsync(LookupType lookupType, LookupItemViewModel lookupItemViewModel);
        Task<int> CreatePositionAsync(PositionDetailsViewModel positionDetailsViewModel);
        Task DeleteCompanyAsync(int id);
        Task DeleteContactAsync(int id);
        Task DeleteContactPhoneAsync(int id);
        Task DeleteEventAsync(int id);
        Task DeleteLookupItemAsync(LookupType lookupType, int id);
        Task DeletePositionAsync(int id);
        Task SetContactPhoneAsPrimaryAsync(int contactPhoneID);
        Task UpdateCompanyAsync(CompanyDetailsViewModel companyDetailsViewModel);
        Task UpdateContactAsync(ContactDetailsViewModel contactDetailsViewModel);
        Task UpdateContactPhoneAsync(ContactPhoneViewModel contactPhoneViewModel);
        Task UpdateEventAsync(EventDetailsViewModel eventDetailsViewModel);
        Task UpdateLookupItemAsync(LookupType lookupType, LookupItemViewModel lookupItemViewModel);
        Task UpdatePositionAsync(PositionDetailsViewModel positionDetailsViewModel);
        Task PopulateDataForNewUser(string userName);

    }

}
