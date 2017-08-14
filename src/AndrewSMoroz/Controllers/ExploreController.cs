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
            vm.Maps = await _businessManager.GetMapListAsync();
            vm.MapID = 0;
            if (vm.Maps != null && vm.Maps.Any())
            {
                vm.MapID = vm.Maps.First().Id;
            }
            return View(vm);
        }

        //--------------------------------------------------------------------------------------------------------------
        // POST: Explore/Setup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Setup([Bind("MapID")] SetupViewModel setupViewModel)
        {

            try
            {

                MapSession currentMapSession = await _businessManager.GetInitialMapSessionAsync(setupViewModel.MapID);
                HttpContext.Session.Set<MapSession>(_uiSettings.KeyCurrentMapSession, currentMapSession);

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                setupViewModel.Maps = await _businessManager.GetMapListAsync();
                if (setupViewModel.Maps != null && setupViewModel.Maps.Any())
                {
                    if (setupViewModel.Maps.SingleOrDefault(m => m.Id == setupViewModel.MapID) == null)
                    {
                        setupViewModel.MapID = setupViewModel.Maps.First().Id;
                    }
                }
                return View(setupViewModel);
            }

            return RedirectToAction("Play");

        }

        //--------------------------------------------------------------------------------------------------------------
        // GET: Explore/Play
        public IActionResult Play()
        {

            MapSession currentMapSession = null;
            try
            {
                currentMapSession = HttpContext.Session.Get<MapSession>(_uiSettings.KeyCurrentMapSession);
                if (currentMapSession == null)
                {
                    throw new Exception("Valid map session not found.");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            return View(currentMapSession);

        }

        //--------------------------------------------------------------------------------------------------------------
        // POST: Explore/ProcessCommand
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult ProcessCommand(string CommandText)
        {

            //TODO: Read MapSession from session
            //TODO: Call appropriate command
            //TODO: Append valid directions onto each command result
            //TODO: Put new MapSession object in session

            List<string> temp = new List<string>();
            temp.Add("hi");
            temp.Add("there");

            return View(temp);

        }

    }

}
