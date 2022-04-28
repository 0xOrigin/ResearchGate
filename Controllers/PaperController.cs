using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Research_Gate.ViewModels;

namespace Research_Gate.Controllers
{
    public class PaperController : Controller
    {
        private AuthorPaperViewModel authorPaperViewModel = new AuthorPaperViewModel();

        // GET: Paper
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Display()
        {
            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }

        public ActionResult Like()
        {
            return View();
        }

        public ActionResult DisLike()
        {
            return View();
        }

        public ActionResult Comment()
        {
            return View();
        }
    }
}