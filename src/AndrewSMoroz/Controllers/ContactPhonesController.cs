using static AndrewSMoroz.Enums;
using AndrewSMoroz;
using AndrewSMoroz.CustomExceptions;
using AndrewSMoroz.Services;
using AndrewSMoroz.ViewModels.ContactViewModels;
using AndrewSMoroz.ViewModels.ContactPhoneViewModels;
using AndrewSMoroz.ViewModels.LookupViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;

namespace AndrewSMoroz.Controllers
{
    [Authorize]
    [RequireHttps]
    public class ContactPhonesController : Controller
    {

        private readonly IBusinessServices _businessServices;
        private readonly ContactsUISettings _uiSettings;

        //--------------------------------------------------------------------------------------------------------------
        public ContactPhonesController(IBusinessServices businessServices, IOptions<ContactsUISettings> uiSettings)
        {
            _businessServices = businessServices;
            _uiSettings = uiSettings.Value;
        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: ContactPhones/Edit/1
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            ContactPhoneViewModel contactPhoneViewModel = await _businessServices.GetContactPhoneDetailsAsync(id.Value);
            if (contactPhoneViewModel == null)
            {
                return NotFound();
            }
            await CreateSelectListsAsync(contactPhoneViewModel.ContactPhoneTypeID);
            return View(contactPhoneViewModel);
        }

        //--------------------------------------------------------------------------------------------------------------
        // POST: ContactPhones/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ContactFullName,ContactID,ContactPhoneTypeID,PhoneNumber,Extension")] ContactPhoneViewModel contactPhoneViewModel)
        {

            if (id != contactPhoneViewModel.ID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _businessServices.UpdateContactPhoneAsync(contactPhoneViewModel);
                    return RedirectToAction("Details", "Contacts", new RouteValueDictionary() { { "id", contactPhoneViewModel.ContactID } });
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

            await CreateSelectListsAsync(contactPhoneViewModel.ContactPhoneTypeID);
            return View(contactPhoneViewModel);

        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: ContactPhones/Create
        [HttpGet]
        public async Task<IActionResult> Create(int? contactID)
        {
            if (contactID == null)
            {
                return BadRequest();
            }

            ContactDetailsViewModel contactDetailsViewModel = await _businessServices.GetContactDetailsAsync(contactID.Value);

            if (contactDetailsViewModel == null || contactDetailsViewModel.ID == 0)
            {
                return NotFound();
            }

            ContactPhoneViewModel contactPhoneViewModel = new ContactPhoneViewModel() { ContactFullName = contactDetailsViewModel.FullName,
                                                                                        ContactID = contactID.Value};

            await CreateSelectListsAsync(null);
            return View(contactPhoneViewModel);
        }

        //--------------------------------------------------------------------------------------------------------------
        // POST: ContactPhones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactFullName,ContactID,ContactPhoneTypeID,PhoneNumber,Extension")] ContactPhoneViewModel contactPhoneViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    int newID = await _businessServices.CreateContactPhoneAsync(contactPhoneViewModel);
                    return RedirectToAction("Details", "Contacts", new RouteValueDictionary() { { "id", contactPhoneViewModel.ContactID } });
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

            await CreateSelectListsAsync(contactPhoneViewModel.ContactPhoneTypeID);
            return View(contactPhoneViewModel);

        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: ContactPhones/Delete/1?contactID=1
        public async Task<IActionResult> Delete(int? id, int? contactID)
        {

            if (id == null || contactID == null)
            {
                return BadRequest();
            }

            try
            {
                await _businessServices.DeleteContactPhoneAsync(id.Value);
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

            return RedirectToAction("Details", "Contacts", new RouteValueDictionary() { { "id", contactID } });

        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: ContactPhones/SetAsPrimary/1?contactID=1
        public async Task<IActionResult> SetAsPrimary(int? id, int? contactID)
        {

            if (id == null || contactID == null)
            {
                return BadRequest();
            }

            try
            {
                await _businessServices.SetContactPhoneAsPrimaryAsync(id.Value);
            }
            catch (BusinessException bex)
            {
                // NOTE: TempData will last for one additional request from the same client.
                //       It uses Session, unless the CookieTempDataProvider is configured in 
                //       Startup.ConfigureServices, in which case it will use cookies
                // NOTE: A UISettings value is used for the key to avoid "magic strings"
                TempData[_uiSettings.KeyOperationErrorMessage] = bex.Message;   
            }
            catch (Exception ex)
            {
                TempData[_uiSettings.KeyOperationErrorMessage] = _uiSettings.UserErrorMessage;
            }

            return RedirectToAction("Details", "Contacts", new RouteValueDictionary() { { "id", contactID } });

        }

        //--------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Creates lists to be used as the sets of choices for dropdown list fields and puts them into the ViewBag
        /// </summary>
        private async Task CreateSelectListsAsync(object selectedContactPhoneTypeID = null)
        {

            // Text for default select list item
            ViewBag.SelectListDefaultItemText = _uiSettings.SelectListDefaultItemText;

            // Contact Types list
            IEnumerable<LookupItemViewModel> contactTypeViewModels = await _businessServices.GetLookupListAsync(LookupType.PhoneType);
            ViewBag.ContactPhoneTypeList = new SelectList(contactTypeViewModels, "ID", "Description", selectedContactPhoneTypeID);

        }

    }

}
