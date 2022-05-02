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
        private static IEnumerable<Author> authors;
        public ActionResult Index()
        {
            return View(authors);
        }

        [Route("Search/SearchByName/{name}")]
        public ActionResult SearchByName(string name)
        {
            authors = dbContext.Authors.Where(a => a.Fname.Equals(name)).ToList();
            return RedirectToAction("Index");
        }

        [Route("Search/SearchByEmail/{Email}")]
        public ActionResult SearchByEmail(string Email)
        {

            authors = dbContext.Authors.Where(a => a.Email.Equals(Email)).ToList();
            return RedirectToAction("Index");
        }



        [Route("Search/SearchByUniversity/{university}")]
        public ActionResult SearchByUniversity(string university)
        {
            authors = dbContext.Authors.Where(a => a.University.Equals(university)).ToList();
            return RedirectToAction("Index");
        }
    }
}