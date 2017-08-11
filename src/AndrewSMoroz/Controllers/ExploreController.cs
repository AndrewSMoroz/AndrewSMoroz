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

        //TODO: Implement POST setup method that creates a MapSession and puts it into the session
        //      Don't forget about the cross site forgery token attribute for POST methods

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
