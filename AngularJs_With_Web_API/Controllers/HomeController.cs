using AngularJs_With_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularJs_With_Web_API.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [AuthorizationFilter]
        public ActionResult Index()
        {
            return View();
        }

    }
}
