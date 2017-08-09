using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace AndrewSMoroz.Controllers
{

    [RequireHttps]
    public class ExploreController : Controller
    {

        //--------------------------------------------------------------------------------------------------------------
        public IActionResult Setup()
        {

            //TODO: Show dropdown containing available maps

            return View();

        }

        //TODO: Put session key for CurrentMapSession into config file
        //TODO: Implement POST setup method that creates a MapSession and puts it into the session
        //      Don't forget about the cross site forgery token attribute for POST methods

        //HttpContext.Session.Set<MapSession>(SessionKeyDate, DateTime.Now);
        //var date = HttpContext.Session.Get<MapSession>(SessionKeyDate);

    }

}
