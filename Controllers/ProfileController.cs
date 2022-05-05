using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Research_Gate.ViewModels;
using Research_Gate.Models;

namespace Research_Gate.Controllers
{
    public class ProfileController : Controller
    {
        private AuthorPaperViewModel authorPaperViewModel = new AuthorPaperViewModel();

        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult EditProfile(Author obj)
        {
            return View();
        }
        [HttpPost]
        public ActionResult EditProfile()
        {
            return View();
        }

    }
}