using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AndrewSMoroz.Controllers
{

    [RequireHttps]
    public class ExploreController : Controller
    {

        //--------------------------------------------------------------------------------------------------------------
        public IActionResult Setup()
        {
            return View();
        }

    }

}
