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

namespace CallTracker.Controllers
{
    public class CallRecordsController : Controller
    {
        List<CallData> AllCalls = new List<CallData>();
        List<CallData> morningList = new List<CallData>();
        List<CallData> earlyAfternoonList = new List<CallData>();
        List<CallData> midDayList = new List<CallData>();
        List<CallData> eveningList = new List<CallData>();

        private CallTrackerContext db = new CallTrackerContext();
        
        // GET: CallRecords
        public async Task<ActionResult> Index(string search)
        {
            var temp = from t in db.CallRecords
                       select t;

            if (!String.IsNullOrEmpty(search))
            {
                temp = temp.Where(s => s.contact_id.Contains(search));
                
            } 

            return View (await temp.ToListAsync());
           //return View(await db.CallRecords.ToListAsync());
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: CallRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "call_id,call_day,call_time,available,contact_id,phone,Email")] CallRecord callRecord)
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

        public string DetermineBestTime()
        {
            var morningBlock = Convert.ToInt32( db.CallRecords.SqlQuery("Select Count(*) From CallRecords Where DatePart(hour, call_time) > 5 AND DatePart(hour, call_time) < 9 AND available = 1") );
            var noonBlock = Convert.ToInt32( db.CallRecords.SqlQuery("Select Count(*) From CallRecords Where DatePart(hour, call_time) >9 AND DatePart(hour, call_time) < 13 AND available = 1") );
            var afternoonBlock = Convert.ToInt32( db.CallRecords.SqlQuery("Select Count(*) From CallRecords Where DatePart(hour, call_time) > 13 AND DatePart(hour, call_time) < 17 AND available = 1") );
            var eveningBlock = Convert.ToInt32( db.CallRecords.SqlQuery("Select Count(*) From CallRecords Where DatePart(hour, call_time) > 17 AND DatePart(hour, call_time) < 21 AND available = 1") );

            string bestTime;

            if (morningBlock > noonBlock 
                && morningBlock > afternoonBlock
                && morningBlock > eveningBlock) 
            { 
                return "Morning Block";
            }
            else if (noonBlock > morningBlock 
                && noonBlock > afternoonBlock
                && noonBlock > eveningBlock) 
            {
                return "Noon Block" ;
            }
            else if (afternoonBlock > morningBlock 
                && afternoonBlock > noonBlock
                && afternoonBlock > eveningBlock) 
            {
                return "Afternoon Block" ;
            }
            else
            {
               return "Evening Block" ;
            }

        }

        //public void StripTime(string call_id, string cust_id, string user_id, string call_day, DateTime call_time, bool available)
        //{
        //   string timeString = call_time.ToString("HH");
        //   int time = Int32.Parse(timeString);
        //   if (available == true)
        //    {
        //       AddCallsToList(call_id, cust_id, user_id, call_day, time, available);
        //    }
        //}

        //public void AddCallsToList(string call_id, string cust_id, string user_id, string call_day, int time, bool available)
        //{ 
        //    CallData call = new CallData(call_id, cust_id, user_id, call_day, time, available);
           
        //    int caseSwitch = time;

        //    switch(caseSwitch)
        //    {
        //       case 7:
        //       case 8:
        //       case 9:
        //           morningList.Add(call);
        //           break;
        //       case 10:
        //       case 11:
        //       case 12:
        //           earlyAfternoonList.Add(call);
        //           break;
        //       case 13:
        //       case 14:
        //       case 15:
        //           midDayList.Add(call);
        //           break;
        //       case 16:
        //       case 17:
        //       case 18:
        //       case 19:
        //       default:
        //           eveningList.Add(call);
        //           break;
        //    }
        // }

        // public string CompareLists(string bestTimeBlock)
        // {
        //    List<int> counts = new List<int>();
        //    int mornCount = morningList.Count();
        //    int midCount = midDayList.Count();
        //    int earlyAfCount = earlyAfternoonList.Count();
        //    int eveningCount = eveningList.Count();

        //    counts.Add(mornCount);
        //    counts.Add(midCount);
        //    counts.Add(earlyAfCount);
        //    counts.Add(eveningCount);

        //    counts.Sort();
        //    counts.Reverse();

        //    int blockWithMostCalls = counts[0]; 

        //    if (blockWithMostCalls == mornCount)
        //    {
        //        return "Morning block is best time to reach them";
        //    }
        //    else if (blockWithMostCalls == midCount)
        //    {
        //        return "Mid day block is best time to reach them";
        //    }
        //    else if (blockWithMostCalls == earlyAfCount)
        //    {
        //        return "Early Afternnon block is best time to reach them";
        //    }
        //    else if (blockWithMostCalls == eveningCount)
        //    {
        //        return "Evening Block is best time to reach them";
        //    }
        //    else
        //    {
        //        return "Best time currently unknown, not enough data";
        //    }
      
   }
}


