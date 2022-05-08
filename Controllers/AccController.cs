using Research_Gate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Research_Gate.Controllers
{
    public class AccController : Controller
    {
        private ResearchgateDBContext dbContext = new ResearchgateDBContext();
        // GET: Acc

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Models.Author author)
        {
            if(ModelState.IsValid)
            {
                dbContext.Authors.Add(author);
                dbContext.SaveChanges();
            }
            return View(author);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
      
        [HttpPost]
        public ActionResult Login(string email , string password)
        {
            if (ModelState.IsValid)
            {
                Models.Author author = dbContext.Authors.Where(a => a.Email == email.Trim() && a.Password == password.Trim()).FirstOrDefault();
                if(author != null)
                {
                    Session["id"] = author.Author_id;
                    Session["Fname"] = author.Fname;
                    return RedirectToAction("Index", "Profile", new {id = Session["id"] });
                }
            }
            return View(email,password);
        }
    }
}