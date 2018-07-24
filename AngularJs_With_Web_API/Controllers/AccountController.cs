using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin.Security;
using System.Threading.Tasks;
using AngularJs_With_Web_API.Models;
using System.Text;
using System.Security.Cryptography;
//using System.Data.Entity.Core.Objects;
using System.Data.Objects;

namespace AngularJs_With_Web_API.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        /*private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

         public ApplicationSignInManager SignInManager
         {
             get
             {
                 return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
             }
             private set
             {
                 _signInManager = value;
             }
         }

         public ApplicationUserManager UserManager
         {
             get
             {
                 return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
             }
             private set
             {
                 _userManager = value;
             }
         }*/

        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOff()
        {
            Session.Clear();
            Session.Abandon();
            return View("Login");
        }


        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (returnUrl == "/Account/LogOff")
            {
                Session.Clear();
                Session.Abandon();
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (callcenterEntities db = new callcenterEntities())
            {
                ObjectParameter Output = new ObjectParameter("responseMessage", typeof(string));
                db.uspValidateUser(model.Username, model.Password, Output);

                if (Convert.ToString(Output.Value).Contains("Success"))
                {
                    var validuser = db.userinfoes.FirstOrDefault(i => i.LoginName == model.Username);
                    Session["UserID"] = validuser.UserId;
                    Session["LoginName"] = validuser.LoginName;
                    Session["UserName"] = validuser.UserName;
                    if (validuser.IsAdmin)
                        Session["IsAdmin"] = true;
                    else
                        Session["IsAdmin"] = false;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", Convert.ToString(Output.Value));
                    return View(model);
                }
            }


            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            //var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        return RedirectToLocal(returnUrl);
            //    case SignInStatus.LockedOut:
            //        return View("Lockout");
            //    case SignInStatus.RequiresVerification:
            //        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
            //    case SignInStatus.Failure:
            //    default:
            //        ModelState.AddModelError("", "Invalid login attempt.");
            //        return View(model);
            //}
        }



        public string GenerateHash(string input, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            // SHA256Managed sHA256ManagedString = new SHA256Managed();
            SHA512Managed shaM = new SHA512Managed();
            byte[] hash = shaM.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

    }
}
