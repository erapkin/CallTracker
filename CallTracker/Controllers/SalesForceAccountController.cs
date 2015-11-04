using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Salesforce.Force;
using WebApplication9.Models;

namespace WebApplication9.Controllers
{
    public class SalesForceAccountController : Controller
    {
        // GET: /Accounts/  (SALES FORCE)
        
        public async Task<ActionResult> Index(string searchString)
        {
            //searchString = "Moda3";
            var accessToken = Session["AccessToken"].ToString();
            var apiVersion = Session["ApiVersion"].ToString();
            var instanceUrl = Session["InstanceUrl"].ToString();

            var client = new ForceClient(instanceUrl, accessToken, apiVersion);
            var accounts = await client.QueryAsync<SalesForceAccountViewModel>("SELECT id, name, description FROM Account WHERE name = '" + searchString + "'");

            return View(accounts.records);
        }
    }
}

//