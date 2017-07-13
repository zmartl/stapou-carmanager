using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

using stapolizeiuster_carmanager.Models;

namespace stapolizeiuster_carmanager.Controllers
{
    [Authorize]
    public class PlanningsController : BaseController
    {
        private static readonly CarsController _carsController = new CarsController();
        private static readonly StatesController _statesController = new StatesController();
        private readonly stapolizeiuster_carmanagerContext db = new stapolizeiuster_carmanagerContext();

        // GET: Plannings
        public ActionResult Index()
        {
            ViewBag.Name = GetUserNamePrinicpals();
            return View(db.Plannings.Include(x => x.Car).Include(x => x.State).Where(x => x.EndTime > DateTime.Now).ToList());
        }

        // GET: Plannings/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.Name = GetUserNamePrinicpals();
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
            ViewBag.Name = GetUserNamePrinicpals();
            ViewBag.Cars = new SelectList(db.Cars, "Car.Id", "Car.Radio");

            return View(new Planning {StartTime = startTime, EndTime = endTime});
        }

        // POST: Plannings/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateHttpAntiForgeryToken]
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

                TimeSpan startTimeSpan = new TimeSpan(0, 0, 0);
                TimeSpan endTimeSpan = new TimeSpan(23, 59, 59);

                planning.StartTime = planning.StartTime.Date + startTimeSpan;
                planning.EndTime = planning.EndTime.Date + endTimeSpan;

                db.Plannings.Add(planning);
                db.SaveChanges();
                return RedirectToAction("Index", new {message = "createSuccess"});
            }

            return View(planning);
        }

        // GET: Plannings/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Name = GetUserNamePrinicpals();
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
        [ValidateHttpAntiForgeryToken]
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
        [ValidateHttpAntiForgeryToken]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.Name = GetUserNamePrinicpals();

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
            var items = _carsController.Get();

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

        public int GetCountPlannedPlanningsByState(IEnumerable<Planning> plannings, string state)
        {
            var count = plannings.Count(x => x.State.Name.Equals(state));
            return count;
        }

        public IEnumerable<Planning> GetPlannedPlannings(DateTime starTime, DateTime endTime)
        {
            var plannedPlannings = db.Plannings.Where(
                        x =>
                            x.StartTime <= starTime && starTime <= x.EndTime ||
                            x.StartTime <= endTime && endTime <= x.EndTime ||
                            starTime <= x.StartTime && endTime >= x.EndTime).ToList();

            return plannedPlannings;
        }
    }
}