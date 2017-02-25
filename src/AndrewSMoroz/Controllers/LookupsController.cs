using static AndrewSMoroz.Enums;
using AndrewSMoroz;
using AndrewSMoroz.CustomExceptions;
using AndrewSMoroz.Services;
using AndrewSMoroz.ViewModels.LookupViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;

namespace AndrewSMoroz.Controllers
{
    [Authorize]
    [RequireHttps]
    public class LookupsController : Controller
    {

        private readonly IBusinessServices _businessServices;
        private readonly ContactsUISettings _uiSettings;

        //--------------------------------------------------------------------------------------------------------------
        public LookupsController(IBusinessServices businessServices, IOptions<ContactsUISettings> uiSettings)
        {
            _businessServices = businessServices;
            _uiSettings = uiSettings.Value;
        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: Lookups?lookupType=1
        // NOTE: If a value for lookupType is not provided, the model binder will assign a value of zero.
        //       For this reason, the LookupType enum does not have an item with a value of zero.
        //       That way, a 400 will be returned, rather than going to the Index page with one of the lookup lists as a default.
        public async Task<IActionResult> Index(LookupType lookupType)
        {

            if (!Enum.IsDefined(typeof(LookupType), lookupType))
            {
                return BadRequest();
            }

            IEnumerable<LookupItemViewModel> lookupItemViewModels = await _businessServices.GetLookupListAsync(lookupType);

            // If another controller put an error message in TempData, transfer that message to the ViewBag for display
            if (TempData[_uiSettings.KeyOperationErrorMessage] != null)
            {
                ViewBag.OperationErrorMessage = TempData[_uiSettings.KeyOperationErrorMessage].ToString();
            }

            PopulatePageData(lookupType);
            return View(lookupItemViewModels);

        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: Lookups/Edit/1?lookupType=1
        [HttpGet]
        public async Task<IActionResult> Edit(int? id, LookupType lookupType)
        {

            if (id == null || !Enum.IsDefined(typeof(LookupType), lookupType))
            {
                return BadRequest();
            }

            LookupItemViewModel lookupItemViewModel = await _businessServices.GetLookupItemAsync(lookupType, id.Value);

            if (lookupItemViewModel == null)
            {
                return NotFound();
            }

            PopulatePageData(lookupType);
            return View(lookupItemViewModel);

        }

        //--------------------------------------------------------------------------------------------------------------
        // POST: Lookups/Edit/1?lookupType=1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LookupType lookupType, [Bind("ID,Description,Sequence")] LookupItemViewModel lookupItemViewModel)
        {

            if (id != lookupItemViewModel.ID || !Enum.IsDefined(typeof(LookupType), lookupType))
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _businessServices.UpdateLookupItemAsync(lookupType, lookupItemViewModel);
                    return RedirectToAction("Index", "Lookups", new RouteValueDictionary() { { "lookupType", (int)lookupType } });
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

            PopulatePageData(lookupType);
            return View(lookupItemViewModel);

        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: Lookups/Create?lookupType=1
        [HttpGet]
        public IActionResult Create(LookupType lookupType)
        {
            if (!Enum.IsDefined(typeof(LookupType), lookupType))
            {
                return BadRequest();
            }
            PopulatePageData(lookupType);
            return View();
        }

        //--------------------------------------------------------------------------------------------------------------
        // POST: Lookups/Create?lookupType=1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LookupType lookupType, [Bind("Description,Sequence")] LookupItemViewModel lookupItemViewModel)
        {

            if (!Enum.IsDefined(typeof(LookupType), lookupType))
            {
                return BadRequest();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    int newID = await _businessServices.CreateLookupItemAsync(lookupType, lookupItemViewModel);
                    return RedirectToAction("Index", "Lookups", new RouteValueDictionary() { { "lookupType", (int)lookupType } });
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

            PopulatePageData(lookupType);
            return View(lookupItemViewModel);

        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: Lookups/Delete/1?lookupType=1
        public async Task<IActionResult> Delete(int? id, LookupType lookupType)
        {

            if (id == null || !Enum.IsDefined(typeof(LookupType), lookupType))
            {
                return BadRequest();
            }

            try
            {
                await _businessServices.DeleteLookupItemAsync(lookupType, id.Value);
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

            PopulatePageData(lookupType);
            return RedirectToAction("Index", new RouteValueDictionary() { { "lookupType", (int)lookupType } });

        }

        //--------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Populates the ViewBag with the data necessary for the view to present itself as the specified type of lookup list
        /// </summary>
        private void PopulatePageData(LookupType lookupType)
        {

            ViewBag.LookupType = (int)lookupType;

            string lookupDescription = "Lookup List";
            switch (lookupType)
            {
                case LookupType.ContactType:
                    lookupDescription = _uiSettings.LookupDescriptionContactTypes;
                    break;
                case LookupType.EventType:
                    lookupDescription = _uiSettings.LookupDescriptionEventTypes;
                    break;
                case LookupType.PhoneType:
                    lookupDescription = _uiSettings.LookupDescriptionPhoneTypes;
                    break;
                default:
                    break;
            }

            ViewBag.LookupDescription = lookupDescription;

        }

    }

}
