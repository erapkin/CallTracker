using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CallTracker.Models;

namespace CallTracker.Controllers
{
    public class CallRecordsController : Controller
    {
        private CallTracker_beDataEntities db = new CallTracker_beDataEntities();

        // GET: CallRecords
        public ActionResult Index()
        {
            var callRecords = db.CallRecords.Include(c => c.UserAccount);
            return View(callRecords.ToList());
        }

        // GET: CallRecords/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CallRecord callRecord = db.CallRecords.Find(id);
            if (callRecord == null)
            {
                return HttpNotFound();
            }
            return View(callRecord);
        }

        // GET: CallRecords/Create
        public ActionResult Create()
        {
            ViewBag.user_id = new SelectList(db.UserAccounts, "user_id", "user_name");
            return View();
        }

        // POST: CallRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "call_id,cust_id,user_id,call_day,call_time,available,last_name")] CallRecord callRecord)
        {
            if (ModelState.IsValid)
            {
                db.CallRecords.Add(callRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.user_id = new SelectList(db.UserAccounts, "user_id", "user_name", callRecord.user_id);
            return View(callRecord);
        }

        // GET: CallRecords/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CallRecord callRecord = db.CallRecords.Find(id);
            if (callRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(db.UserAccounts, "user_id", "user_name", callRecord.user_id);
            return View(callRecord);
        }

        // POST: CallRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "call_id,cust_id,user_id,call_day,call_time,available,last_name")] CallRecord callRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(callRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.user_id = new SelectList(db.UserAccounts, "user_id", "user_name", callRecord.user_id);
            return View(callRecord);
        }

        // GET: CallRecords/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CallRecord callRecord = db.CallRecords.Find(id);
            if (callRecord == null)
            {
                return HttpNotFound();
            }
            return View(callRecord);
        }

        // POST: CallRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CallRecord callRecord = db.CallRecords.Find(id);
            db.CallRecords.Remove(callRecord);
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
    }
}
