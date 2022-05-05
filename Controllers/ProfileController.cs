using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Research_Gate.ViewModels;
using Research_Gate.Models;
using System.Data.Entity;

namespace Research_Gate.Controllers
{
    public class ProfileController : Controller
    {
        private AuthorPaperViewModel authorPaperViewModel = new AuthorPaperViewModel();
        ResearchgateDBContext db = new ResearchgateDBContext();
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditProfile(string id)
        {
            var obj = db.Authors.Find(id);
            if(obj == null)
            {
                return HttpNotFound();  
            }
            return View(obj);
        }
        [HttpPost]
        public ActionResult EditProfile([Bind (Include = "Fname , Lname , University , Email , Department , Password ,Mobile, Img_path ")]Author user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EditProfile");   
            }
            return View("EditProfile");
        }

    }
}