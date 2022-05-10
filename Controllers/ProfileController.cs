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
        [HttpGet]
        public ActionResult Index(int? id)
        {
            if (id == null)
                return View("PageNotFound");

            authorPaperViewModel.Author = db.Authors.SingleOrDefault(author => author.Author_id == id);
            
            return View(authorPaperViewModel);
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
                    string imgExtension = System.IO.Path.GetExtension(file.FileName);
                    string imgName = Controllers.FileNameGenerator.GenerateAuthorPhotoName(obj.Author_id, imgExtension);
                    string path = Server.MapPath(Controllers.FileUtility.GetAuthorImage(imgName));
                    file.SaveAs(path);
                    obj.Image = imgName;
                }
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", new { id = obj.Author_id});
            }
            return View();
        }
    }
}