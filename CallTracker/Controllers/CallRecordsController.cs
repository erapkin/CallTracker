using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CallTracker.Models;
using Salesforce.Force;
using WebApplication9.Models;

namespace CallTracker.Controllers
{
    public class CallRecordsController : Controller
    {
        
        private CallTrackerContext db = new CallTrackerContext();

        // GET: CallRecords
        public async Task<ActionResult> Index()
        {
            return View(await db.CallRecords.ToListAsync());
        }

        // GET: CallRecords/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CallRecord callRecord = await db.CallRecords.FindAsync(id);
            if (callRecord == null)
            {
                return HttpNotFound();
            }
            return View(callRecord);
        }

        // GET: CallRecords/Create
        [HttpGet]
        public ActionResult Create(string contactId)
        {
            if (contactId != null)
            {
                ViewBag.ContactId = contactId;
                
            }
              return View();
        }

        // POST: CallRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "call_id,call_day,call_time,contact_id,available,phone,Email")] CallRecord callRecord)
        {
            if (ModelState.IsValid)
            {                
                db.CallRecords.Add(callRecord);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
           

            return View(callRecord);
        }

        // GET: CallRecords/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CallRecord callRecord = await db.CallRecords.FindAsync(id);
            if (callRecord == null)
            {
                return HttpNotFound();
            }
            return View(callRecord);
        }

        // POST: CallRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "call_id,call_day,call_time,available,contact_id,phone,Email")] CallRecord callRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(callRecord).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(callRecord);
        }

        // GET: CallRecords/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CallRecord callRecord = await db.CallRecords.FindAsync(id);
            if (callRecord == null)
            {
                return HttpNotFound();
            }
            return View(callRecord);
        }

        // POST: CallRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            CallRecord callRecord = await db.CallRecords.FindAsync(id);
            db.CallRecords.Remove(callRecord);
            await db.SaveChangesAsync();
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
