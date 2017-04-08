using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

using stapolizeiuster_carmanager.Models;

namespace stapolizeiuster_carmanager.Controllers
{
    public class CarsController : Controller
    {
        private readonly stapolizeiuster_carmanagerContext db = new stapolizeiuster_carmanagerContext();

        // GET: Cars
        public ActionResult Index()
        {
            return View(db.Cars.ToList());
        }

        // GET: Cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var car = db.Cars.Find(id);
            if (car == null)
                return HttpNotFound();
            return View(car);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,Radio")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index", new {message = "createSuccess"});
            }

            return View(car);
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var car = db.Cars.Find(id);
            if (car == null)
                return HttpNotFound();
            return View(car);
        }

        // POST: Cars/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Radio")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new {message = "editSuccess"});
            }
            return View(car);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (db.Plannings.Any(x => x.Car.Id == id))
                return RedirectToAction("Index", new {message = "deleteConflict"});
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var car = db.Cars.Find(id);
            if (car == null)
                return HttpNotFound();
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var car = db.Cars.Find(id);
            db.Cars.Remove(car);

            db.SaveChanges();
            return RedirectToAction("Index", new {message = "deleteSuccess"});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }

        public IEnumerable<Car> GetAvailableCars(DateTime startTime, DateTime endTime, Car car)
        {
                   
            var plannedCars = db.Plannings.Where(
                        x =>
                            x.StartTime <= startTime && startTime <= x.EndTime ||
                            x.StartTime <= endTime && endTime <= x.EndTime ||
                            startTime <= x.StartTime && endTime >= x.EndTime).ToList();
            var allCars = db.Cars.ToList();

            if (car == null)
                car = new Car {Id = 0};

            foreach (var item in plannedCars)
                if (allCars.Contains(item.Car) && item.Car.Id != car.Id)
                    allCars.Remove(item.Car);

            return allCars;
        }
    }
}