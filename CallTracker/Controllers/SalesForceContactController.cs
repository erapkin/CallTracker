using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Salesforce.Force;
using WebApplication9.Models;

namespace WebApplication9.Models
{
    public class SalesForceContactController : Controller
    {
        // GET: /Contact/  (SALES FORCE)
        
        public async Task<ActionResult> Index(string searchString)
        {
            //searchString = "Moda3";
            var accessToken = Session["AccessToken"].ToString();
            var apiVersion = Session["ApiVersion"].ToString();
            var instanceUrl = Session["InstanceUrl"].ToString();

            var client = new ForceClient(instanceUrl, accessToken, apiVersion);
            var contacts = await client.QueryAsync<SalesForceContactViewModel>("SELECT Id, FirstName, LastName, Title, Phone, Email From Contact WHERE FirstName LIKE'%" +searchString+ "%' OR LastName LIKE'%"+searchString+"%'");

            return View(contacts.records);
        }
    }
}

//id, name, description FROM Account WHERE name = '"+searchString+"'"