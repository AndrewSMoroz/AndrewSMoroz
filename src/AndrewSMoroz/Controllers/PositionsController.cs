using AndrewSMoroz;
using AndrewSMoroz.CustomExceptions;
using AndrewSMoroz.Services;
using AndrewSMoroz.ViewModels.PositionViewModels;
using AndrewSMoroz.ViewModels.CompanyViewModels;
using AndrewSMoroz.ViewModels.ContactViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class PositionsController : Controller
    {

        private readonly IBusinessServices _businessServices;
        private readonly ContactsUISettings _uiSettings;

        //--------------------------------------------------------------------------------------------------------------
        public PositionsController(IBusinessServices businessServices, IOptions<ContactsUISettings> uiSettings)
        {
            _businessServices = businessServices;
            _uiSettings = uiSettings.Value;
        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: Positions
        public async Task<IActionResult> Index()
        {

            // If another controller put an error message in TempData, transfer that message to the ViewBag for display
            if (TempData[_uiSettings.KeyOperationErrorMessage] != null)
            {
                ViewBag.OperationErrorMessage = TempData[_uiSettings.KeyOperationErrorMessage].ToString();
            }

            IEnumerable<PositionListViewModel> positionListViewModels = await _businessServices.GetPositionListAsync();
            return View(positionListViewModels);

        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: Positions/Details/1
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            PositionDetailsViewModel positionDetailsViewModel = await _businessServices.GetPositionDetailsAsync(id.Value);
            if (positionDetailsViewModel == null)
            {
                return NotFound();
            }

            // If another controller put an error message in TempData, transfer that message to the ViewBag for display
            if (TempData[_uiSettings.KeyOperationErrorMessage] != null)
            {
                ViewBag.OperationErrorMessage = TempData[_uiSettings.KeyOperationErrorMessage].ToString();
            }

            return View(positionDetailsViewModel);
        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: Positions/Edit/1
        public async Task<IActionResult> Edit(int? id, string fromController, string fromAction)
        {
            ViewBag.FromController = fromController;
            ViewBag.FromAction = fromAction;

            if (id == null)
            {
                return BadRequest();
            }
            PositionDetailsViewModel positionDetailsViewModel = await _businessServices.GetPositionDetailsAsync(id.Value);
            if (positionDetailsViewModel == null)
            {
                return NotFound();
            }
            await CreateSelectListsAsync(positionDetailsViewModel.CompanyID, positionDetailsViewModel.ContactIDs);
            return View(positionDetailsViewModel);
        }

        //--------------------------------------------------------------------------------------------------------------
        // POST: Positions/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CompanyID,Title,Description,DatePosted,ContactIDs,RecruiterCompanyID,RecruiterContactIDs")] PositionDetailsViewModel positionDetailsViewModel, string fromController, string fromAction)
        {

            ViewBag.FromController = fromController;
            ViewBag.FromAction = fromAction;

            if (id != positionDetailsViewModel.ID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _businessServices.UpdatePositionAsync(positionDetailsViewModel);
                    string controller = (string.IsNullOrEmpty(fromController) ? "Positions" : fromController);
                    string action = (string.IsNullOrEmpty(fromAction) ? "Details" : fromAction);
                    RouteValueDictionary routeValues = ("details,edit".Contains((action ?? "").ToLower()) ? new RouteValueDictionary() { { "id", positionDetailsViewModel.ID } } : null);
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

            await CreateSelectListsAsync(positionDetailsViewModel.CompanyID, positionDetailsViewModel.ContactIDs);
            return View(positionDetailsViewModel);

        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: Positions/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await CreateSelectListsAsync(null);
            return View();
        }

        //--------------------------------------------------------------------------------------------------------------
        // POST: Positions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyID,ContactIDs,Title,Description,DatePosted")] PositionDetailsViewModel positionDetailsViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    int newID = await _businessServices.CreatePositionAsync(positionDetailsViewModel);
                    return RedirectToAction("Details", "Positions", new RouteValueDictionary() { { "id", newID } });
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

            await CreateSelectListsAsync(positionDetailsViewModel.CompanyID);
            return View(positionDetailsViewModel);

        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: Positions/Delete/1
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                await _businessServices.DeletePositionAsync(id.Value);
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
        // GET: Positions/ContactsForCompany/1
        /// <summary>
        /// This method returns a partial view representing a given company's contacts.
        /// It may be called via ajax.
        /// </summary>
        /// <param name="id">A company ID</param>
        /// <returns>Html consisting of one checkbox for each of the specified company's contacts</returns>
        public async Task<IActionResult> ContactsForCompany(int? id)
        {

            if (id == null)
            {
                return BadRequest();
            }
            IEnumerable<ContactListViewModel> contactListViewModels = await _businessServices.GetContactListForCompanyAsync(id.Value);
            if (contactListViewModels == null)
            {
                return NotFound();
            }
            return View(contactListViewModels);

        }

        //--------------------------------------------------------------------------------------------------------------
        public IActionResult Error()
        {
            return View();
        }

        //--------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Creates lists to be used as the sets of choices for the Company and Contacts fields and puts them into the ViewBag
        /// </summary>
        /// <param name="selectedCompanyID"></param>
        private async Task CreateSelectListsAsync(object selectedCompanyID = null, IEnumerable<int> selectedContactIDs = null)
        {

            // Text for default select list item
            ViewBag.SelectListDefaultItemText = _uiSettings.SelectListDefaultItemText;

            // Company list
            IEnumerable<CompanyListViewModel> companyListViewModels = await _businessServices.GetCompanyListAsync();
            ViewBag.CompanyList = new SelectList(companyListViewModels.Where(vm => vm.IsRecruiter == false), "ID", "Name", selectedCompanyID);   // Intended for select list

            // Recruiter Company list
            ViewBag.RecruiterCompanyList = new SelectList(companyListViewModels.Where(vm => vm.IsRecruiter == true), "ID", "Name", selectedCompanyID);   // Intended for select list

            // Contact list
            IEnumerable<ContactListViewModel> contactsListViewModels = await _businessServices.GetContactListAsync();
            ViewBag.ContactList = contactsListViewModels;                   // Intended for series of checkboxes, so no special class

        }

#region Original Scaffolded Code

        ////--------------------------------------------------------------------------------------------------------------
        //// GET: Positions/Create
        //public IActionResult Create()
        //{
        //    ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "ID");
        //    return View();
        //}

        ////--------------------------------------------------------------------------------------------------------------
        //// POST: Positions/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ID,CompanyID,Title,Description,DatePosted")] Position position)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(position);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "ID", position.CompanyID);
        //    return View(position);
        //}

        ////--------------------------------------------------------------------------------------------------------------
        //// GET: Positions/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var position = await _context.Positions.SingleOrDefaultAsync(m => m.ID == id);
        //    if (position == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "ID", position.CompanyID);
        //    return View(position);
        //}

        ////--------------------------------------------------------------------------------------------------------------
        //// POST: Positions/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID,CompanyID,Title,Description,DatePosted")] Position position)
        //{
        //    if (id != position.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(position);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PositionExists(position.ID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "ID", position.CompanyID);
        //    return View(position);
        //}

        ////--------------------------------------------------------------------------------------------------------------
        //// GET: Positions/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var position = await _context.Positions
        //        .Include(p => p.Company)
        //        .SingleOrDefaultAsync(m => m.ID == id);
        //    if (position == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(position);
        //}

        ////--------------------------------------------------------------------------------------------------------------
        //// POST: Positions/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var position = await _context.Positions.SingleOrDefaultAsync(m => m.ID == id);
        //    _context.Positions.Remove(position);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        ////--------------------------------------------------------------------------------------------------------------
        //private bool PositionExists(int id)
        //{
        //    return _context.Positions.Any(e => e.ID == id);
        //}

#endregion Original Scaffolded Code

    }

}
