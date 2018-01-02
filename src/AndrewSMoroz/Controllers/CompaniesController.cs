using AndrewSMoroz;
using AndrewSMoroz.CustomExceptions;
using AndrewSMoroz.Services;
using AndrewSMoroz.ViewModels.LookupViewModels;
using AndrewSMoroz.ViewModels.CompanyViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;

namespace AndrewSMoroz.Controllers
{
    [Authorize]
    [RequireHttps]
    public class CompaniesController : Controller
    {

        private readonly IBusinessServices _businessServices;
        private readonly ContactsUISettings _uiSettings;

        //--------------------------------------------------------------------------------------------------------------
        public CompaniesController(IBusinessServices businessServices, IOptions<ContactsUISettings> uiSettings)
        {
            _businessServices = businessServices;
            _uiSettings = uiSettings.Value;
        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: Companies
        public async Task<IActionResult> Index()
        {

            // If another controller put an error message in TempData, transfer that message to the ViewBag for display
            if (TempData[_uiSettings.KeyOperationErrorMessage] != null)
            {
                ViewBag.OperationErrorMessage = TempData[_uiSettings.KeyOperationErrorMessage].ToString();
            }

            IEnumerable<CompanyListViewModel> companyListViewModel = await _businessServices.GetCompanyListAsync();
            return View(companyListViewModel);

        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: Companies/Details/1
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            CompanyDetailsViewModel companyDetailsViewModel = await _businessServices.GetCompanyDetailsAsync(id.Value);
            if (companyDetailsViewModel == null)
            {
                return NotFound();
            }
            return View(companyDetailsViewModel);
        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: Companies/Edit/1
        public async Task<IActionResult> Edit(int? id, string fromController, string fromAction)
        {
            ViewBag.FromController = fromController;
            ViewBag.FromAction = fromAction;

            if (id == null)
            {
                return BadRequest();
            }
            CompanyDetailsViewModel companyDetailsViewModel = await _businessServices.GetCompanyDetailsAsync(id.Value);
            if (companyDetailsViewModel == null)
            {
                return NotFound();
            }
            await CreateStateSelectListAsync(companyDetailsViewModel.State);
            return View(companyDetailsViewModel);
        }

        //--------------------------------------------------------------------------------------------------------------
        // POST: Companies/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Address,City,State,PostalCode")] CompanyDetailsViewModel companyDetailsViewModel, string fromController, string fromAction)
        {

            ViewBag.FromController = fromController;
            ViewBag.FromAction = fromAction;

            if (id != companyDetailsViewModel.ID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _businessServices.UpdateCompanyAsync(companyDetailsViewModel);
                    string controller = (string.IsNullOrEmpty(fromController) ? "Companies" : fromController );
                    string action = (string.IsNullOrEmpty(fromAction) ? "Details" : fromAction);
                    RouteValueDictionary routeValues = ("details,edit".Contains((action ?? "").ToLower()) ? new RouteValueDictionary() { { "id", companyDetailsViewModel.ID } } : null);
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

            await CreateStateSelectListAsync(companyDetailsViewModel.State);
            return View(companyDetailsViewModel);

        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: Companies/Create
        public async Task<IActionResult> Create()
        {
            await CreateStateSelectListAsync(null);
            return View();
        }

        //--------------------------------------------------------------------------------------------------------------
        // POST: Companies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsRecruiter,Address,City,State,PostalCode")] CompanyDetailsViewModel companyDetailsViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    int newID = await _businessServices.CreateCompanyAsync(companyDetailsViewModel);
                    return RedirectToAction("Details", "Companies", new RouteValueDictionary() { { "id", newID } });
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

            await CreateStateSelectListAsync(companyDetailsViewModel.State);
            return View(companyDetailsViewModel);

        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: Companies/Delete/1
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                await _businessServices.DeleteCompanyAsync(id.Value);
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
        /// Creates a list to be used as the set of choices for the State field and puts it into the ViewBag
        /// </summary>
        /// <param name="selectedState"></param>
        private async Task CreateStateSelectListAsync(object selectedState = null)
        {

            // Text for default select list item
            ViewBag.SelectListDefaultItemText = _uiSettings.SelectListDefaultItemText;

            // State list
            IEnumerable<StateListViewModel> stateListViewModels = await _businessServices.GetStateListAsync();
            ViewBag.StateList = new SelectList(stateListViewModels, "Abbreviation", "Abbreviation", selectedState);

        }

    }

}
