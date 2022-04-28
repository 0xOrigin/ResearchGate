using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Research_Gate.Models;

namespace Research_Gate.Controllers
{
    public class SearchController : Controller
    {
        private ResearchgateDBContext dbContext = new ResearchgateDBContext();
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }
    }
}