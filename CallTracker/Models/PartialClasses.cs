using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CallTracker.Models
{
    public class PartialClasses
    {
        [MetadataType(typeof(CallRecordMetadata))]
        public partial class CallRecord
        {
        }

    }
}