using AngularJs_With_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularJs_With_Web_API.Controllers
{

    public class UsersOpsController : Controller
    {
        //
        // GET: /UsersOps/
        [AuthorizationFilter]
        public ActionResult Index()
        {
            if (Convert.ToString(Session["IsAdmin"]) == "True")
            {
                return View();
            }
            else
                return RedirectToAction("Index", "Home");
            //{
            //    using (callcenterEntities db = new callcenterEntities())
            //    {

            //    }
            //}

        }

    }
}
