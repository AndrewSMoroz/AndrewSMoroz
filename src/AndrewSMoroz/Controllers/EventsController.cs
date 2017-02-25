using static AndrewSMoroz.Enums;
using AndrewSMoroz;
using AndrewSMoroz.CustomExceptions;
using AndrewSMoroz.Services;
using AndrewSMoroz.ViewModels.LookupViewModels;
using AndrewSMoroz.ViewModels.PositionViewModels;
using AndrewSMoroz.ViewModels.EventViewModels;
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
    public class EventsController : Controller
    {

        private readonly IBusinessServices _businessServices;
        private readonly ContactsUISettings _uiSettings;

        //--------------------------------------------------------------------------------------------------------------
        public EventsController(IBusinessServices businessServices, IOptions<ContactsUISettings> uiSettings)
        {
            _businessServices = businessServices;
            _uiSettings = uiSettings.Value;
        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: Events/Edit/1
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            EventDetailsViewModel eventDetailsViewModel = await _businessServices.GetEventDetailsAsync(id.Value);
            if (eventDetailsViewModel == null)
            {
                return NotFound();
            }
            await CreateSelectListsAsync(eventDetailsViewModel.EventTypeID);
            return View(eventDetailsViewModel);
        }

        //--------------------------------------------------------------------------------------------------------------
        // POST: Events/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PositionID,EventTypeID,Date,Time,Description,PositionTitle,CompanyName")] EventDetailsViewModel eventDetailsViewModel)
        {

            if (id != eventDetailsViewModel.ID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _businessServices.UpdateEventAsync(eventDetailsViewModel);
                    return RedirectToAction("Details", "Positions", new RouteValueDictionary() { { "id", eventDetailsViewModel.PositionID } });
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

            await CreateSelectListsAsync(eventDetailsViewModel.EventTypeID);
            return View(eventDetailsViewModel);

        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: Events/Create
        [HttpGet]
        public async Task<IActionResult> Create(int? positionID)
        {
            if (positionID == null)
            {
                return BadRequest();
            }

            PositionDetailsViewModel positionDetailsViewModel = await _businessServices.GetPositionDetailsAsync(positionID.Value);

            if (positionDetailsViewModel == null || positionDetailsViewModel.ID == 0)
            {
                return NotFound();
            }

            EventDetailsViewModel eventDetailsViewModel = new EventDetailsViewModel()
            {
                CompanyName = positionDetailsViewModel.CompanyName,
                PositionID = positionDetailsViewModel.ID,
                PositionTitle = positionDetailsViewModel.Title
            };

            await CreateSelectListsAsync(null);
            return View(eventDetailsViewModel);
        }

        //--------------------------------------------------------------------------------------------------------------
        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PositionID,EventTypeID,Date,Time,Description,PositionTitle,CompanyName")] EventDetailsViewModel eventDetailsViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    int newID = await _businessServices.CreateEventAsync(eventDetailsViewModel);
                    return RedirectToAction("Details", "Positions", new RouteValueDictionary() { { "id", eventDetailsViewModel.PositionID } });
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

            await CreateSelectListsAsync(eventDetailsViewModel.EventTypeID);
            return View(eventDetailsViewModel);

        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: Events/Delete/1?lookupType=1
        public async Task<IActionResult> Delete(int? id, int? positionID)
        {

            if (id == null || positionID == null)
            {
                return BadRequest();
            }

            try
            {
                await _businessServices.DeleteEventAsync(id.Value);
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

            return RedirectToAction("Details", "Positions", new RouteValueDictionary() { { "id", positionID } });

        }

        //--------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Creates lists to be used as the sets of choices for dropdown list fields and puts them into the ViewBag
        /// </summary>
        private async Task CreateSelectListsAsync(object selectedEventTypeID = null)
        {

            // Text for default select list item
            ViewBag.SelectListDefaultItemText = _uiSettings.SelectListDefaultItemText;

            // Contact Types list
            IEnumerable<LookupItemViewModel> eventTypeViewModels = await _businessServices.GetLookupListAsync(LookupType.EventType);
            ViewBag.EventTypeList = new SelectList(eventTypeViewModels, "ID", "Description", selectedEventTypeID);

        }

    }

}
