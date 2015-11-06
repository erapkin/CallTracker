using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CallTracker.Models
{
    public partial class CallRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long call_id { get; set; }
        public System.DateTime call_day { get; set; }
        public System.DateTime call_time { get; set; }
        public bool available { get; set; }
        public string contact_id { get; set; }
        public string phone { get; set; }
        public string Email { get; set; }
        public static string Foo()
        {
            return ("morning");
        }
    }
}
