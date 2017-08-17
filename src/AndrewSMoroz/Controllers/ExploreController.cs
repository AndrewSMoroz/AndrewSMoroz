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
        public IActionResult ProcessCommand(string CommandText)
        {

            MapSession currentMapSession = null;
            try
            {

                // Make sure there is a valid MapSession object in the session
                currentMapSession = HttpContext.Session.Get<MapSession>(_uiSettings.KeyCurrentMapSession);
                if (currentMapSession == null)
                {
                    throw new Exception("Valid map session not found.");
                }

                // Parse the command text input
                string userInput = "";
                string[] userInputTokens = null;

                userInput = CommandText;
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    return View(new List<string>());
                }

                // Put the first word into the RequestedAction property; and the rest, if any, into RequestedActionTarget
                currentMapSession.MapState.RequestedAction = null;
                currentMapSession.MapState.RequestedActionTarget = null;
                userInputTokens = userInput.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                currentMapSession.MapState.RequestedAction = userInputTokens[0].ToUpper();
                if (userInputTokens.Length > 1)
                {
                    currentMapSession.MapState.RequestedActionTarget = string.Join(" ", userInputTokens, 1, userInputTokens.Length - 1).ToUpper();
                }

                // Process the command and put the new MapState object into the session
                currentMapSession.MapState = _businessManager.ProcessAction(currentMapSession);
                //currentMapSession.MapState.ActionResultMessages.Insert(0, "");
                //currentMapSession.MapState.ActionResultMessages.Insert(0, "> " + (CommandText ?? "").ToUpper());
                HttpContext.Session.Set<MapSession>(_uiSettings.KeyCurrentMapSession, currentMapSession);
                ViewBag.CommandText = (CommandText ?? "").ToUpper();

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }

            return View(currentMapSession.MapState.ActionResultMessages);

        }

    }

}
