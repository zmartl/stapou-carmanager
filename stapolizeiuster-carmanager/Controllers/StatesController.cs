using System;
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
    [Authorize]
    public class StatesController : BaseController
    {
        private stapolizeiuster_carmanagerContext db = new stapolizeiuster_carmanagerContext();

        // GET: States
        public ActionResult Index()
        {
            ViewBag.Name = GetUserNamePrinicpals();
            return View(db.States.ToList());
        }

        // GET: States/Create
        public ActionResult Create()
        {
            ViewBag.Name = GetUserNamePrinicpals();
            return View();
        }

        // POST: States/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateHttpAntiForgeryToken]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] State state)
        {
            if (ModelState.IsValid)
            {
                db.States.Add(state);
                db.SaveChanges();
                return RedirectToAction("Index", new { message = "createSuccess" });
            }

            return View(state);
        }

        // GET: States/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Name = GetUserNamePrinicpals();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            State state = db.States.Find(id);
            if (state == null)
            {
                return HttpNotFound();
            }
            return View(state);
        }

        // POST: States/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateHttpAntiForgeryToken]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] State state)
        {
            if (ModelState.IsValid)
            {
                db.Entry(state).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { message = "editSuccess" });
            }
            return View(state);
        }

        // GET: States/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.Name = GetUserNamePrinicpals();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            State state = db.States.Find(id);
            if (state == null)
            {
                return HttpNotFound();
            }
            return View(state);
        }

        // POST: States/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateHttpAntiForgeryToken]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(db.Plannings.Any(x => x.State.Id == id))
            {
                return RedirectToAction("Index", new { message = "deleteConflict" });
            }

            State state = db.States.Find(id);
            db.States.Remove(state);
            db.SaveChanges();
            return RedirectToAction("Index", new { message = "deleteSuccess" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public IEnumerable<State> Get()
        {
            return db.States.ToList();
        }

        public State GetSingleById(int id)
        {
            return db.States.FirstOrDefault(x => x.Id == id);
        }

    }
}
