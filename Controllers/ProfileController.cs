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
            if(authorPaperViewModel.Author == null)
                return View("PageNotFound");

            return View(authorPaperViewModel);
        }

        public ActionResult EditProfile()
        {
            if (Session["id"] != null)
            {
                int id = (int)Session["id"];
                Author author = db.Authors.SingleOrDefault(x => x.Author_id == id);

                if (author == null)
                {
                    return View("PageNotFound");
                }
                
                return View(author);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(Author author , HttpPostedFileBase file)
        {
            string authorImage = db.Authors.AsNoTracking().SingleOrDefault(x => x.Author_id == author.Author_id).Image;

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string imgExtension = System.IO.Path.GetExtension(file.FileName);
                    string imgName = FileNameGenerator.GenerateAuthorPhotoName(author.Author_id, imgExtension);
                    string path = FileUtility.GetAuthorImageStorePath(imgName);
                    file.SaveAs(path);
                    DeleteImage(authorImage);
                    author.Image = imgName;
                }
                else
                {
                    author.Image = authorImage;
                }

                author.Password = EncryptPassword.Encrypt(author.Password.Trim());
                author.ConfirmPassword = EncryptPassword.Encrypt(author.ConfirmPassword.Trim());

                db.Entry(author).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", new { id = author.Author_id});
            }

            return View();
        }

        private void DeleteImage(string imageName)
        {
            if(imageName == FileUtility.GetDefaultImageName())
                return;

            string fullPath = Request.MapPath(FileUtility.GetAuthorImageRelativePath(imageName));
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
    }
}