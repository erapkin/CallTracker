using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;    

namespace CallTracker.Models
{
    public class CallRecordMetadata
    {
        [Display(Name= "Call Day")]
        public System.DateTime call_day;

        [Display(Name = "Call Time")]
        public System.DateTime call_time;

        [Display(Name = "Contact Was Available")]
        public bool available;
    }
}