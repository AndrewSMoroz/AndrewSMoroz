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

    public interface IModelAdapter
    {

        // Methods to convert domain models to UI view models
        IEnumerable<CompanyListViewModel> ConvertCompanies(IEnumerable<Company> companies);
        CompanyDetailsViewModel ConvertCompany(Company company);
        IEnumerable<ContactListViewModel> ConvertContacts(IEnumerable<Contact> contacts);
        ContactDetailsViewModel ConvertContact(Contact contact);
        IEnumerable<ContactPhoneViewModel> ConvertContactPhones(IEnumerable<ContactPhone> contactPhones);
        ContactPhoneViewModel ConvertContactPhone(ContactPhone contactPhone);
        EventDetailsViewModel ConvertEvent(Event ev);
        LookupItemViewModel ConvertLookupItem(LookupItem lookupItem);
        IEnumerable<LookupItemViewModel> ConvertLookupList(IEnumerable<LookupItem> lookupItems);
        IEnumerable<PositionListViewModel> ConvertPositions(IEnumerable<Position> positions);
        PositionDetailsViewModel ConvertPosition(Position position);
        IEnumerable<StateListViewModel> ConvertStates(IEnumerable<State> states);

        // Methods to convert UI view models to domain models
        Company ConvertCompany(CompanyDetailsViewModel companyDetailsViewModel);
        Contact ConvertContact(ContactDetailsViewModel contactDetailsViewModel);
        ContactPhone ConvertContactPhone(ContactPhoneViewModel contactPhoneViewModel);
        Event ConvertEvent(EventDetailsViewModel eventDetailsViewModel);
        LookupItem ConvertLookupItem(LookupItemViewModel lookupItemViewModel, LookupType lookupType);
        Position ConvertPosition(PositionDetailsViewModel positionDetailsViewModel);

        // Methods to return domain models from IDs
        Company GetCompany(int id);
        Contact GetContact(int id);
        ContactPhone GetContactPhone(int id);
        Event GetEvent(int id);
        LookupItem GetLookupItem(int id, LookupType lookupType);
        Position GetPosition(int id);

    }

}
