﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using stapolizeiuster_carmanager.Models;

namespace stapolizeiuster_carmanager.Controllers
{
    public class PlanningsController : Controller
    {
        private stapolizeiuster_carmanagerContext db = new stapolizeiuster_carmanagerContext();
        private static CarsController _carsController = new CarsController();
        private static StatesController _statesController = new StatesController();

        // GET: Plannings
        public ActionResult Index()
        {
            return View(db.Plannings.Include(x => x.Car).Include(x => x.State).ToList());
        }

        // GET: Plannings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planning planning = db.Plannings.Find(id);
            if (planning == null)
            {
                return HttpNotFound();
            }
            return View(planning);
        }

        // GET: Plannings/Create
        public ActionResult Create()
        {
           return View();
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
                if(planning.Car.Id > 0)
                    planning.Car = _carsController.GetSingleById(planning.Car.Id);
                if (planning.State.Id > 0)
                    planning.State = _statesController.GetSingleById(planning.State.Id);
                db.Plannings.Add(planning);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(planning);
        }

        // GET: Plannings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planning planning = db.Plannings.Find(id);
            if (planning == null)
            {
                return HttpNotFound();
            }
            return View(planning);
        }

        // POST: Plannings/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StartTime,EndTime")] Planning planning)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planning).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(planning);
        }

        // GET: Plannings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planning planning = db.Plannings.Find(id);
            if (planning == null)
            {
                return HttpNotFound();
            }
            return View(planning);
        }

        // POST: Plannings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Planning planning = db.Plannings.Find(id);
            db.Plannings.Remove(planning);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Get Data for DropDown
        public static IEnumerable<SelectListItem> FillCarsDropDown()
        {
            var list = new List<SelectListItem>();
            var items = _carsController.Get();

            foreach (var item in items)
            {
                list.Add(new SelectListItem() { Text = item.Description + " - " + item.Radio, Value = item.Id.ToString() });
            }
            return list;
        }

        public static IEnumerable<SelectListItem> FillStatesDropDown()
        {
            var list = new List<SelectListItem>();
            var items = _statesController.Get();

            foreach (var item in items)
            {
                list.Add(new SelectListItem() { Text = item.Name, Value = item.Id.ToString() });
            }
            return list;
        }
    }
}
