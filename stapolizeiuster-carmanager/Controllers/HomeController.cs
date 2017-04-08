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
    [Authorize]
    public class HomeController : BaseController
    {
        private stapolizeiuster_carmanagerContext db = new stapolizeiuster_carmanagerContext();

        public ActionResult Index()
        {
            ViewBag.Name = GetUserNamePrinicpals();

            var datetime = DateTime.Now;
            return View(db.Plannings.Where(x => x.StartTime <= datetime && x.EndTime >= datetime).ToList());
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