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
    }
}