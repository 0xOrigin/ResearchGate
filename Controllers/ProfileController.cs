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

        public ActionResult EditProfile(int? id)
        {
            if (id != null) {
                var obj = db.Authors.SingleOrDefault(x => x.Author_id == id);
                if (obj == null)
                {
                    return HttpNotFound();
                }
                AuthorPaperViewModel user = new AuthorPaperViewModel
                {
                     Author = obj
                };
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult EditProfile(AuthorPaperViewModel user , HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var obj = db.Authors.SingleOrDefault(x => x.Author_id == user.Author.Author_id);
                obj.Fname = user.Author.Fname;
                obj.Lname = user.Author.Lname;
                obj.Password = user.Author.Password;
                obj.Email = user.Author.Email;
                obj.Department = user.Author.Department;
                obj.University = user.Author.University;
                obj.Mobile = user.Author.Mobile;
                obj.Password = user.Author.Password;
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/images/profiles/"), pic);
                    file.SaveAs(path);
                    obj.Img_path = pic;
                }
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", new { id = obj.Author_id});
            }
            return View();
        }
    }
}