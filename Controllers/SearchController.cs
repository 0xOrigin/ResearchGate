using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.WebPages;
using Research_Gate.Models;

namespace Research_Gate.Controllers
{
    public class SearchController : Controller
    {
        private ResearchgateDBContext dbContext = new ResearchgateDBContext();
        // GET: Search
        private static IEnumerable<Author> authors = new List<Author>();

        [HttpGet]
        public ActionResult Index()
        {

            return View(authors);
        }

        [HttpPost]
        [Route("Search/SearchByName/{name?}")]
        public ActionResult SearchByName(string name)
        {
            ClearResults();

            if (name != null && name.Trim() != "")
                authors = dbContext.Authors.Where(a => (a.Fname + a.Lname).Contains(name)).ToList();

            return PartialView("_Search", authors);
        }

        [HttpPost]
        [Route("Search/SearchByEmail/{Email?}")]
        public ActionResult SearchByEmail(string Email)
        {
            ClearResults();

            if (Email != null && Email.Trim() != "")
                authors = dbContext.Authors.Where(a => a.Email.Contains(Email)).ToList();
            
            return PartialView("_Search", authors);
        }

        [HttpPost]
        [Route("Search/SearchByUniversity/{university?}")]
        public ActionResult SearchByUniversity(string university)
        {
            ClearResults();

            if (university != null && university.Trim() != "")
                authors = dbContext.Authors.Where(a => a.University.Contains(university)).ToList();
            
            return PartialView("_Search", authors);
        }

        [HttpPost]
        [Route("Search/ClearResults")]
        public void ClearResults()
        {
            authors = new List<Author>();
        }
    }
}