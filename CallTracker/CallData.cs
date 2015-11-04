using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallTracker
{
    public class CallData
    {
            public string Call_id { get; set; }
            public string Cust_id { get; set; }
            public string User_id { get; set; }
            public string Call_day { get; set; }
            public int Time { get; set; }
            public bool Available { get; set; }
        public CallData(string call_id, string cust_id, string user_id, string call_day, int time, bool available)
        {
            this.Call_id = call_id;
            this.Cust_id = cust_id;
            this.User_id = user_id;
            this.Call_day = call_id;
            this.Time = time;
            this.Available = available;
        }
    }
}