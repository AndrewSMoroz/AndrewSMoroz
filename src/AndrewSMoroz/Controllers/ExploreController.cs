using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using AndrewSMoroz.Services;
using ExploreObjects.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using AndrewSMoroz.ViewModels.ExploreViewModels;
using Microsoft.AspNetCore.Routing;

namespace AndrewSMoroz.Controllers
{

    [RequireHttps]
    public class ExploreController : Controller
    {

        private readonly IExploreBusinessManager _businessManager;
        private readonly ExploreUISettings _uiSettings;

        //--------------------------------------------------------------------------------------------------------------
        public ExploreController(IExploreBusinessManager businessManager, IOptions<ExploreUISettings> uiSettings)
        {
            _businessManager = businessManager;
            _uiSettings = uiSettings.Value;
        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: Explore/Setup
        [HttpGet]
        public async Task<IActionResult> Setup()
        {
            SetupViewModel vm = new SetupViewModel();
            await CreateSelectListsAsync(null);
            return View(vm);

        }

        //--------------------------------------------------------------------------------------------------------------
        // POST: Explore/Setup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Setup([Bind("MapID")] SetupViewModel setupViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (setupViewModel.MapID == 1) { throw new Exception("Well, that didn't work..."); }
                    //TODO: Use the ViewModel instead of the ViewBag for the list of Maps.
                    //      Make the view select the one in the MapID field.
                    //      Use LINQ to set it to the first one in the GET (or if there's an invalid one in the POST), the posted one in the POST
                    //TODO: Create MapSession object and put it in the session
                    //TODO: Run the LOOK command on it first
                    //TODO: The MapSession object may be the model for the Play view
                    //int newID = await _businessServices.CreateCompanyAsync(companyDetailsViewModel);
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    await CreateSelectListsAsync(null);
                    return View(setupViewModel);
                }
            }

            return RedirectToAction("Play");

        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: Explore/ProcessCommand
        public async Task<IActionResult> Play([Bind("CommandText")] PlayViewModel playViewModel)
        {

            return View();

        }

        //TODO: Implement POST setup method that creates a MapSession and puts it into the session
        //HttpContext.Session.Set<MapSession>(SessionKeyDate, DateTime.Now);
        //var date = HttpContext.Session.Get<MapSession>(SessionKeyDate);

        //--------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Creates list to be used as the set of choices for the Maps field and puts it into the ViewBag
        /// </summary>
        private async Task CreateSelectListsAsync(object selectedMapID = null)
        {
            IEnumerable<Map> maps = await _businessManager.GetMapListAsync();
            ViewBag.MapList = new SelectList(maps, "Id", "Name", selectedMapID);
        }

    }

}
