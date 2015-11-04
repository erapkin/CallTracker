using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CallTracker.Models;

namespace CallTracker.Controllers
{
    public class CallRecordsController : ApiController
    {
        private CallTrackerContext db = new CallTrackerContext();
        List<CallData> morningList = new List<CallData>();
        List<CallData> earlyAfternoonList = new List<CallData>();
        List<CallData> midDayList = new List<CallData>();
        List<CallData> eveningList = new List<CallData>();
        
        // GET: api/CallRecords
        public IQueryable<CallRecord> GetCallRecords()
        {
            return db.CallRecords;
        }

        // GET: api/CallRecords/5
        [ResponseType(typeof(CallRecord))]
        public async Task<IHttpActionResult> GetCallRecord(long id)
        {
            CallRecord callRecord = await db.CallRecords.FindAsync(id);
            if (callRecord == null)
            {
                return NotFound();
            }

            return Ok(callRecord);
        }

        // PUT: api/CallRecords/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCallRecord(long id, CallRecord callRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != callRecord.call_id)
            {
                return BadRequest();
            }

            db.Entry(callRecord).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CallRecordExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CallRecords
        [ResponseType(typeof(CallRecord))]
        public async Task<IHttpActionResult> PostCallRecord(CallRecord callRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CallRecords.Add(callRecord);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = callRecord.call_id }, callRecord);
        }

        // DELETE: api/CallRecords/5
        [ResponseType(typeof(CallRecord))]
        public async Task<IHttpActionResult> DeleteCallRecord(long id)
        {
            CallRecord callRecord = await db.CallRecords.FindAsync(id);
            if (callRecord == null)
            {
                return NotFound();
            }

            db.CallRecords.Remove(callRecord);
            await db.SaveChangesAsync();

            return Ok(callRecord);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CallRecordExists(long id)
        {
            return db.CallRecords.Count(e => e.call_id == id) > 0;
        }
        public void StripTime(string call_id, string cust_id, string user_id, string call_day, DateTime call_time, bool available)
        {
            string timeString = call_time.ToString("HH");
            int time = Int32.Parse(timeString);
            if (available == true)
            {
                AddCallsToList(call_id, cust_id, user_id, call_day, time, available);
            }
        }

        public void AddCallsToList(string call_id, string cust_id, string user_id, string call_day, int time, bool available)
        {
            CallData call = new CallData(call_id, cust_id, user_id, call_day, time, available);

            int caseSwitch = time;

            switch (caseSwitch)
            {
                case 7:
                case 8:
                case 9:
                    morningList.Add(call);
                    break;
                case 10:
                    earlyAfternoonList.Add(call);
                    break;
                case 11:
                    earlyAfternoonList.Add(call);
                    break;
                case 12:
                    earlyAfternoonList.Add(call);
                    break;
                case 13:
                    midDayList.Add(call);
                    break;
                case 14:
                    midDayList.Add(call);
                    break;
                case 15:
                    midDayList.Add(call);
                    break;
                case 16:
                    eveningList.Add(call);
                    break;
                case 17:
                    eveningList.Add(call);
                    break;
                case 18:
                    eveningList.Add(call);
                    break;
                case 19:
                default:
                    eveningList.Add(call);
                    break;
            }
        }

        public void CompareLists()
        {
            List<List<CallData>> allLists = new List<List<CallData>>();
            allLists.Add(morningList);
            allLists.Add(midDayList);
            allLists.Add(earlyAfternoonList);
            allLists.Add(eveningList);

            var desc = from list in allLists orderby list descending select list;

            foreach (List<CallData> list in desc)
            {
                Console.WriteLine(list);
            }
        }
    }
}