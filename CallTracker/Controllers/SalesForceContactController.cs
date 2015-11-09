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
            
            var accessToken = Session["AccessToken"].ToString();
            var apiVersion = Session["ApiVersion"].ToString();
            var instanceUrl = Session["InstanceUrl"].ToString();

            var client = new ForceClient(instanceUrl, accessToken, apiVersion);
            var contacts = await client.QueryAsync<SalesForceContactViewModel>("SELECT Id, FirstName, LastName, Title, Phone, Email From Contact WHERE FirstName LIKE'%" + searchString + "%' OR LastName LIKE'%" + searchString + "%'");

            return View(contacts.records);
        }

        public async Task<ActionResult> IndexFromAccount(string accountId)
        {

            var accessToken = Session["AccessToken"].ToString();
            var apiVersion = Session["ApiVersion"].ToString();
            var instanceUrl = Session["InstanceUrl"].ToString();

            var client = new ForceClient(instanceUrl, accessToken, apiVersion);
            var contacts = await client.QueryAsync<SalesForceContactViewModel>("SELECT Id, FirstName, LastName, Title, Phone, Email From Contact WHERE AccountId= '" + accountId + "'");

            return View(contacts.records);
        }
        public ActionResult Create(string contactId)
        {
            return RedirectToAction("Create", "CallRecords", new { contactId = contactId});
        }

        public ActionResult ViewHistory(string contactId)
        {
            return RedirectToAction("Index", "CallRecords", new { contactId = contactId });
        }
    }
}
