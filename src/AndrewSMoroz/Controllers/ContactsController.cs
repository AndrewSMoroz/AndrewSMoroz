using static AndrewSMoroz.Enums;
using AndrewSMoroz;
using AndrewSMoroz.CustomExceptions;
using AndrewSMoroz.Services;
using AndrewSMoroz.ViewModels.ContactViewModels;
using AndrewSMoroz.ViewModels.CompanyViewModels;
using AndrewSMoroz.ViewModels.LookupViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace AndrewSMoroz.Controllers
{
    [Authorize]
    [RequireHttps]
    public class ContactsController : Controller
    {

        private readonly IBusinessServices _businessServices;
        private readonly ContactsUISettings _uiSettings;

        //--------------------------------------------------------------------------------------------------------------
        public ContactsController(IBusinessServices businessServices, IOptions<ContactsUISettings> uiSettings)
        {
            _businessServices = businessServices;
            _uiSettings = uiSettings.Value;
        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: Contacts
        public async Task<IActionResult> Index()
        {

            // If another controller put an error message in TempData, transfer that message to the ViewBag for display
            if (TempData[_uiSettings.KeyOperationErrorMessage] != null)
            {
                ViewBag.OperationErrorMessage = TempData[_uiSettings.KeyOperationErrorMessage].ToString();
            }

            IEnumerable<ContactListViewModel> contactListViewModels = await _businessServices.GetContactListAsync();
            return View(contactListViewModels);

        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: Contacts/Details/1
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return BadRequest();
            }

            ContactDetailsViewModel contactListViewModel = await _businessServices.GetContactDetailsAsync(id.Value);
            if (contactListViewModel == null)
            {
                return NotFound();
            }

            // If another controller put an error message in TempData, transfer that message to the ViewBag for display
            if (TempData[_uiSettings.KeyOperationErrorMessage] != null)
            {
                ViewBag.OperationErrorMessage = TempData[_uiSettings.KeyOperationErrorMessage].ToString();
            }

            return View(contactListViewModel);

        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: Contacts/Edit/1
        public async Task<IActionResult> Edit(int? id, string fromController, string fromAction)
        {
            ViewBag.FromController = fromController;
            ViewBag.FromAction = fromAction;

            if (id == null)
            {
                return BadRequest();
            }
            ContactDetailsViewModel contactDetailsViewModel = await _businessServices.GetContactDetailsAsync(id.Value);
            if (contactDetailsViewModel == null)
            {
                return NotFound();
            }
            await CreateSelectListsAsync(contactDetailsViewModel.CompanyID, contactDetailsViewModel.ContactTypeID);
            return View(contactDetailsViewModel);
        }

        //--------------------------------------------------------------------------------------------------------------
        // POST: Contacts/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,CompanyID,ContactTypeID")] ContactDetailsViewModel contactDetailsViewModel, string fromController, string fromAction)
        {

            ViewBag.FromController = fromController;
            ViewBag.FromAction = fromAction;

            if (id != contactDetailsViewModel.ID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _businessServices.UpdateContactAsync(contactDetailsViewModel);
                    string controller = (string.IsNullOrEmpty(fromController) ? "Contacts" : fromController);
                    string action = (string.IsNullOrEmpty(fromAction) ? "Details" : fromAction);
                    RouteValueDictionary routeValues = ("details,edit".Contains((action ?? "").ToLower()) ? new RouteValueDictionary() { { "id", contactDetailsViewModel.ID } } : null);
                    return RedirectToAction(action, controller, routeValues);
                }
                catch (BusinessException bex)
                {
                    ModelState.AddModelError("", bex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", _uiSettings.UserErrorMessage);
                }
            }

            await CreateSelectListsAsync(contactDetailsViewModel.CompanyID, contactDetailsViewModel.ContactTypeID);
            return View(contactDetailsViewModel);

        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: Contacts/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await CreateSelectListsAsync(null);
            return View();
        }

        //--------------------------------------------------------------------------------------------------------------
        // POST: Contacts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,CompanyID,ContactTypeID")] ContactDetailsViewModel contactDetailsViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    int newID = await _businessServices.CreateContactAsync(contactDetailsViewModel);
                    return RedirectToAction("Details", "Contacts", new RouteValueDictionary() { { "id", newID } });
                }
                catch (BusinessException bex)
                {
                    ModelState.AddModelError("", bex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", _uiSettings.UserErrorMessage);
                }
            }

            await CreateSelectListsAsync(contactDetailsViewModel.CompanyID);
            return View(contactDetailsViewModel);

        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: Contacts/Delete/1
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                await _businessServices.DeleteContactAsync(id.Value);
            }
            catch (BusinessException bex)
            {
                // NOTE: TempData will last for one additional request from the same client.
                //       It uses Session, unless the CookieTempDataProvider is configured in 
                //       Startup.ConfigureServices, in which case it will use cookies
                TempData[_uiSettings.KeyOperationErrorMessage] = bex.Message;
            }
            catch (Exception ex)
            {
                TempData[_uiSettings.KeyOperationErrorMessage] = _uiSettings.UserErrorMessage;
            }

            return RedirectToAction("Index");

        }

        //--------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Creates lists to be used as the sets of choices for dropdown list fields and puts them into the ViewBag
        /// </summary>
        private async Task CreateSelectListsAsync(object selectedCompanyID = null, object selectedContactTypeID = null)
        {

            // Text for default select list item
            ViewBag.SelectListDefaultItemText = _uiSettings.SelectListDefaultItemText;

            // Company list
            IEnumerable<CompanyListViewModel> companyListViewModels = await _businessServices.GetCompanyListAsync();
            ViewBag.CompanyList = new SelectList(companyListViewModels, "ID", "Name", selectedCompanyID);

            // Contact Types list
            IEnumerable<LookupItemViewModel> contactTypeViewModels = await _businessServices.GetLookupListAsync(LookupType.ContactType);
            ViewBag.ContactTypeList = new SelectList(contactTypeViewModels, "ID", "Description", selectedContactTypeID);

        }

    }

}
