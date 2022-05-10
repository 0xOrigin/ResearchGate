using Research_Gate.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Research_Gate.Controllers
{
    public class AccountController : Controller
    {
        private ResearchgateDBContext dbContext = new ResearchgateDBContext();
        // GET: Acc

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Author author, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (dbContext.Authors.Count(x => x.Email == author.Email) > 0)
                {
                    ModelState.AddModelError("Email", "This email is already registered.");
                    return View(author);
                }

                author.Password = EncryptPassword.Encrypt(author.Password.Trim());
                author.ConfirmPassword = EncryptPassword.Encrypt(author.ConfirmPassword.Trim());

                author.Image = FileUtility.GetDefaultImageName();

                dbContext.Authors.Add(author);
                dbContext.SaveChanges();

                if (file != null)
                {
                    string imgExtension = System.IO.Path.GetExtension(file.FileName);
                    string imgName = FileNameGenerator.GenerateAuthorPhotoName(author.Author_id, imgExtension);
                    string path = FileUtility.GetAuthorImageStorePath(imgName);
                    
                    file.SaveAs(path);
                    author.Image = imgName;

                    dbContext.Entry(author).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }

                return RedirectToAction("Login");
            }

            return View(author);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            password = EncryptPassword.Encrypt(password.Trim());

            Author author = dbContext.Authors.Where(a => a.Email == email.Trim() && a.Password == password.Trim()).FirstOrDefault();
            
            if (ModelState.IsValid)
            {
                if (author != null)
                {
                    Session["id"] = author.Author_id;
                    Session["Fname"] = author.Fname;
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("Email", "Wrong Email or password.");

            return View(author);
        }

        public ActionResult LogOut()
        {
            Session.RemoveAll();
            return RedirectToAction("Login");
        }


    }
}

