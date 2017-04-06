using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using stapolizeiuster_carmanager.Models;

namespace stapolizeiuster_carmanager.Controllers
{
    public class HomeController : Controller
    {
        private stapolizeiuster_carmanagerContext db = new stapolizeiuster_carmanagerContext();

        public ActionResult Index()
        {
            var datetime = DateTime.Now;
            return View(db.Plannings.Where(x => x.StartTime <= datetime && x.EndTime >= datetime).ToList());
        }
    }
}