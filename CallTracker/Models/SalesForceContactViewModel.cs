using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using CallTracker.Models;

namespace WebApplication9.Models
{
    public partial class SalesForceContactViewModel
    {
        public string AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; } //Business Phone
        public string Email { get; set; }
        public string Id { get; set; }

        public string String { get; set; }

    }
}