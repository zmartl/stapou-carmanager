using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using stapolizeiuster_carmanager.Models;
using System.Security.Claims;
using System.IdentityModel.Services;

namespace stapolizeiuster_carmanager.Controllers
{
    public class HomeController : Controller
    {
        private stapolizeiuster_carmanagerContext db = new stapolizeiuster_carmanagerContext();

        public ActionResult Index()
        {
            var nameClaim = ClaimsPrincipal.Current.FindFirst("name");

            if (nameClaim != null && !string.IsNullOrEmpty(nameClaim.Value))
                ViewBag.Name = nameClaim.Value;

            var datetime = DateTime.Now;
            return View(db.Plannings.Where(x => x.StartTime <= datetime && x.EndTime >= datetime).ToList());
        }

        public ActionResult Login(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !this.Url.IsLocalUrl(returnUrl))
            {
                returnUrl = "/";
            }

            // you can use this for the 'authParams.state' parameter
            // in Lock, to provide a return URL after the authentication flow.
            ViewBag.State = "ru=" + HttpUtility.UrlEncode(returnUrl);

            return this.View();
        }

        public ActionResult Logout()
        {
            // Clear the session cookie
            FederatedAuthentication.SessionAuthenticationModule.SignOut();

            // Redirect to Auth0's logout endpoint
            return RedirectToAction("Index");
        }
    }
}