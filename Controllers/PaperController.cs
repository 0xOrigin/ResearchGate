using System;
using System.Collections.Generic;
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
        private ResearchgateDBContext dbContext = new ResearchgateDBContext(); 

        // GET: Paper
        public ActionResult Index(int? id)
        {
            if (id == null)
                return View("PageNotFound");

            Paper paper = dbContext.Papers.SingleOrDefault(c => c.Paper_id == id);
            return View(paper);
        }

        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Upload(string title, List<int> authorsIds, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                DateTime publishDate = DateTime.Now;
                string paperName;
                string paperExtension;
                string path;
                int paperId;


                Paper paper = new Paper()
                {
                    Publish_date = publishDate,
                    Title = title,
                    Paper_path = "path"
                };
                dbContext.Papers.Add(paper);
                dbContext.SaveChanges();

                paperId = dbContext.Papers.ToList().Last().Paper_id;
                paperExtension = System.IO.Path.GetExtension(file.FileName);
                paperName = Controllers.FileNameGenerator.GeneratePaperName(paperId, authorsIds.First(), paperExtension);
                path = Server.MapPath(Controllers.FileUtility.GetPaperFile(paperName));
                file.SaveAs(path);
                dbContext.Papers.SingleOrDefault(p => p.Paper_id == paperId).Paper_path = paperName;
                authorPaperViewModel.Paper = dbContext.Papers.Where(p => p.Paper_id == paperId).FirstOrDefault();
                foreach (int authorId in authorsIds)
                {
                    authorPaperViewModel.Author = dbContext.Authors.Where(a => a.Author_id == authorId).FirstOrDefault();
                    authorPaperViewModel.Paper.Participation.Add(authorPaperViewModel.Author);
                }
                dbContext.SaveChanges();
                
                return RedirectToAction("Index", new { id = paperId });
            }
            else
            {
                return View();
            }
        }

        [Route("paper/Like/{paperId}/{authorId}")]
        public ActionResult Like(int paperId , int authorId)
        {
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

            return RedirectToAction("Index");
        }

        [Route("paper/DisLike/{paperId}/{authorId}")]
        public ActionResult DisLike(int paperId , int authorId)
        {
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

            return RedirectToAction("Index");
        }

        [Route("paper/Comment/{paperId}/{authorId}/{comment}")]
        public ActionResult Comment(int paperId, int authorId, string comment)
        {
            DateTime dateTime = DateTime.Now;
            if (comment != null)
            {
                Models.Comment c = new Comment()
                {
                    Author_id = authorId,
                    Paper_id = paperId,
                    Content = comment,
                    Time = dateTime
                };

                dbContext.Comments.Add(c);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [Route("paper/EditComment/{commentId}/{newComment}")]
        public ActionResult EditComment(int commentId, string newComment)
        {
            DateTime dateTime = DateTime.Now;

            Models.Comment editedComment = dbContext.Comments.Where(c => c.Comment_id == commentId).FirstOrDefault();

            if (newComment != null)
            {
                editedComment.Content = newComment;
                editedComment.Time = dateTime;
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [Route("paper/DeleteComment/{commentId}")]
        public ActionResult DeleteComment(int commentId)
        {
            Models.Comment comment = dbContext.Comments.Where(c => c.Comment_id == commentId).FirstOrDefault();

            dbContext.Comments.Remove(comment);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}