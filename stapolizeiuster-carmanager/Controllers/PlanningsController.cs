using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

using stapolizeiuster_carmanager.Models;

namespace stapolizeiuster_carmanager.Controllers
{
    public class PlanningsController : Controller
    {
        private static readonly CarsController _carsController = new CarsController();
        private static readonly StatesController _statesController = new StatesController();
        private readonly stapolizeiuster_carmanagerContext db = new stapolizeiuster_carmanagerContext();

        // GET: Plannings
        public ActionResult Index()
        {
            return View(db.Plannings.Include(x => x.Car).Include(x => x.State).ToList());
        }

        // GET: Plannings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var planning = db.Plannings.Find(id);
            if (planning == null)
                return HttpNotFound();
            return View(planning);
        }

        // GET: Plannings/Create
        public ActionResult Create(DateTime startTime, DateTime endTime)
        {
            ViewBag.Cars =
                new SelectList(
                    db.Plannings.Where(
                        x =>
                            x.StartTime >= startTime && startTime >= x.EndTime ||
                            x.StartTime >= endTime && endTime >= x.EndTime ||
                            startTime >= x.StartTime && endTime <= x.EndTime).ToList(), "Car.Id", "Car.Radio");

            //var overlap =  new DateTime(2017,04,06,07,00,00) >= startTime && startTime >= new DateTime(2017, 04, 06, 11, 00, 00);
            //var overlap2 = new DateTime(2017, 04, 06, 07, 00, 00) >= endTime && endTime >= new DateTime(2017, 04, 06, 11, 00, 00);
            //var overlap3 = startTime >= new DateTime(2017, 04, 06, 07, 00, 00) && endTime <= new DateTime(2017, 04, 06, 11, 00, 00);


            return View(new Planning {StartTime = startTime, EndTime = endTime});
        }

        // POST: Plannings/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StartTime,EndTime,Car,State")] Planning planning)
        {
            if (ModelState.IsValid)
            {
                if (planning.Car.Id > 0)
                    planning.Car = db.Cars.SingleOrDefault(c => c.Id == planning.Car.Id);
                else
                    return RedirectToAction("Index", new {message = "createConflict"});
                if (planning.State.Id > 0)
                    planning.State = db.States.SingleOrDefault(s => s.Id == planning.State.Id);
                else
                    return RedirectToAction("Index", new {message = "createConflict"});

                db.Plannings.Add(planning);
                db.SaveChanges();
                return RedirectToAction("Index", new {message = "createSuccess"});
            }

            return View(planning);
        }

        // GET: Plannings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var planning = db.Plannings.Find(id);
            if (planning == null)
                return HttpNotFound();
            return View(planning);
        }

        // POST: Plannings/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StartTime,EndTime,Car,State")] Planning planning)
        {
            if (ModelState.IsValid)
            {
                if (planning.Car.Id > 0)
                    planning.Car = db.Cars.SingleOrDefault(c => c.Id == planning.Car.Id);
                else
                    return RedirectToAction("Index", new {message = "createConflict"});
                if (planning.State.Id > 0)
                    planning.State = db.States.SingleOrDefault(s => s.Id == planning.State.Id);
                else
                    return RedirectToAction("Index", new {message = "createConflict"});

                db.Entry(planning).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new {message = "editSuccess"});
            }
            return View(planning);
        }

        // GET: Plannings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var planning = db.Plannings.Find(id);
            if (planning == null)
                return HttpNotFound();
            return View(planning);
        }

        // POST: Plannings/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var planning = db.Plannings.Find(id);
            db.Plannings.Remove(planning);
            db.SaveChanges();
            return RedirectToAction("Index", new {message = "deleteSuccess"});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }

        //Get Data for DropDown
        public static IEnumerable<SelectListItem> FillCarsDropDown(DateTime startTime, DateTime endTime, Car car = null)
        {
            var list = new List<SelectListItem>();
            var items = _carsController.GetAvailableCars(startTime, endTime, car);

            foreach (var item in items)
                list.Add(new SelectListItem {Text = item.Description + " - " + item.Radio, Value = item.Id.ToString()});

            if (!list.Any())
                list.Add(new SelectListItem {Text = "Keine Fahrzeuge vorhanden", Value = "0", Disabled = true});

            return list;
        }

        public static IEnumerable<SelectListItem> FillStatesDropDown()
        {
            var list = new List<SelectListItem>();
            var items = _statesController.Get();

            foreach (var item in items)
                list.Add(new SelectListItem {Text = item.Name, Value = item.Id.ToString()});
            return list;
        }
    }
}