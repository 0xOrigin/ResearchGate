using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Research_Gate.Models;
using Research_Gate.ViewModels;

namespace Research_Gate.Controllers
{
    public class PaperController : Controller
    {
        private AuthorPaperViewModel authorPaperViewModel = new AuthorPaperViewModel();
        private UploadPaperViewModel uploadPaperViewModel = new UploadPaperViewModel();
        private ResearchgateDBContext dbContext = new ResearchgateDBContext();

        // GET: Paper
        public ActionResult Index(int? id)
        {
            if (id == null)
                return View("PageNotFound");

            authorPaperViewModel.Paper = dbContext.Papers.SingleOrDefault(c => c.Paper_id == id);
            if (authorPaperViewModel.Paper == null)
                return View("PageNotFound");

            if (Session["id"] != null)
            {
                int authorID = (int)Session["id"];
                authorPaperViewModel.Author = dbContext.Authors.SingleOrDefault(x => x.Author_id == authorID);
            }

            return View(authorPaperViewModel);
        }

        [HttpGet]
        public ActionResult Upload()
        {
            if (Session["id"] == null)
                return RedirectToAction("Login", "Account");


            int authorID = (int)Session["id"];

            UploadPaperViewModel.Authors = dbContext.Authors.Where(x => x.Author_id != authorID).ToList();
            
            uploadPaperViewModel.Paper = new Paper
            {
                File = "default"
            };

            return View(uploadPaperViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(UploadPaperViewModel model, HttpPostedFileBase file)
        {
            model.Paper.Publish_date = DateTime.Now;
            
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    ModelState.AddModelError("File", "You must upload paper file.");
                   
                    return View(uploadPaperViewModel);
                }

                dbContext.Papers.Add(model.Paper);
                dbContext.SaveChanges();

                int paperId = model.Paper.Paper_id;
                
                int authorID = (int)Session["id"];
                
                
                string paperExtension = System.IO.Path.GetExtension(file.FileName);
                string paperName = Controllers.FileNameGenerator.GeneratePaperName(paperId, authorID, paperExtension);
                string path = Server.MapPath(Controllers.FileUtility.GetPaperFile(paperName));

                file.SaveAs(path);
                
                dbContext.Papers.SingleOrDefault(p => p.Paper_id == paperId).File = paperName;
                

                model.Paper.Participation.Add(dbContext.Authors.SingleOrDefault(x => x.Author_id == authorID));

                if (model.SelectedAuthors != null)
                {
                    foreach (string authorId in model.SelectedAuthors)
                    {
                        int id = Int32.Parse(authorId);
                        authorPaperViewModel.Author = dbContext.Authors.SingleOrDefault(a => a.Author_id == id);
                        model.Paper.Participation.Add(authorPaperViewModel.Author);
                    }
                }

                dbContext.SaveChanges();

                return RedirectToAction("Index", new { id = paperId });
            }

            return RedirectToAction("Upload", "Paper");
        }

        [HttpPost]
        [Route("Paper/Like/{paperId}")]
        public ActionResult Like(int paperId)
        {
            int authorId = (int)Session["id"];
            authorPaperViewModel.Paper = dbContext.Papers.Where(p => p.Paper_id == paperId).FirstOrDefault();
            authorPaperViewModel.Author = dbContext.Authors.Where(a => a.Author_id == authorId).FirstOrDefault();

            if (authorPaperViewModel.Paper.Like.Contains(authorPaperViewModel.Author))
                authorPaperViewModel.Paper.Like.Remove(authorPaperViewModel.Author);
            else
            {
                if (authorPaperViewModel.Paper.Dislike.Contains(authorPaperViewModel.Author))
                    authorPaperViewModel.Paper.Dislike.Remove(authorPaperViewModel.Author);

                authorPaperViewModel.Paper.Like.Add(authorPaperViewModel.Author);
            }
            dbContext.SaveChanges();

            return PartialView("_Likes_Dislikes", authorPaperViewModel.Paper);
        }

        [HttpPost]
        [Route("Paper/DisLike/{paperId}")]
        public ActionResult DisLike(int paperId)
        {
            int authorId = (int)Session["id"];
            authorPaperViewModel.Paper = dbContext.Papers.Where(p => p.Paper_id == paperId).FirstOrDefault();
            authorPaperViewModel.Author = dbContext.Authors.Where(a => a.Author_id == authorId).FirstOrDefault();

            if (authorPaperViewModel.Paper.Dislike.Contains(authorPaperViewModel.Author))
                authorPaperViewModel.Paper.Dislike.Remove(authorPaperViewModel.Author);
            else
            {
                if (authorPaperViewModel.Paper.Like.Contains(authorPaperViewModel.Author))
                    authorPaperViewModel.Paper.Like.Remove(authorPaperViewModel.Author);

                authorPaperViewModel.Paper.Dislike.Add(authorPaperViewModel.Author);
            }

            dbContext.SaveChanges();

            return PartialView("_Likes_Dislikes", authorPaperViewModel.Paper);
        }

        [HttpPost]
        public ActionResult Comment(int paperId, string comment)
        {
            int authorId = (int)Session["id"];

            if (comment != null)
            {
                Comment c = new Comment()
                {
                    Author_id = authorId,
                    Paper_id = paperId,
                    Content = comment,
                    Time = DateTime.Now,
                    Author = dbContext.Authors.FirstOrDefault(a => a.Author_id == authorId)
                };

                dbContext.Comments.Add(c);
                dbContext.SaveChanges();
            }

            authorPaperViewModel.Paper = dbContext.Papers.SingleOrDefault(p => p.Paper_id == paperId);

            IEnumerable<Comment> comments = authorPaperViewModel.Paper.Comments.OrderByDescending(c => c.Time).ToList();
            
            return PartialView("_Comments", comments);
        }

        [HttpPost]
        public ActionResult EditComment(int commentId, string newComment)
        {
            Comment editedComment = dbContext.Comments.Where(c => c.Comment_id == commentId).FirstOrDefault();

            if (newComment != null && newComment != editedComment.Content)
            {
                editedComment.Content = newComment;
                editedComment.Time = DateTime.Now;
                dbContext.SaveChanges();
            }

            authorPaperViewModel.Paper = dbContext.Papers.SingleOrDefault(p => p.Paper_id == editedComment.Paper_id);

            IEnumerable<Comment> comments = authorPaperViewModel.Paper.Comments.OrderByDescending(c => c.Time).ToList();

            return PartialView("_Comments", comments);
        }

        [HttpPost]
        [Route("Paper/DeleteComment/{commentId}")]
        public ActionResult DeleteComment(int commentId)
        {
            Comment comment = dbContext.Comments.Where(c => c.Comment_id == commentId).FirstOrDefault();

            int paperId = comment.Paper_id;
            dbContext.Comments.Remove(comment);
            dbContext.SaveChanges();

            authorPaperViewModel.Paper = dbContext.Papers.SingleOrDefault(p => p.Paper_id == paperId);

            IEnumerable<Comment> comments = authorPaperViewModel.Paper.Comments.OrderByDescending(c => c.Time).ToList();

            return PartialView("_Comments", comments);
        }

    }
}
