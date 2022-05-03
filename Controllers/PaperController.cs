using System;
using System.Collections.Generic;
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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }

        [Route("paper/Like/{paperId}/{authorId}")]
        public ActionResult Like(int paperId , int authorId)
        {
            authorPaperViewModel.Paper = dbContext.Papers.Where(p => p.Paper_id == paperId).FirstOrDefault();
            authorPaperViewModel.Author = dbContext.Authors.Where(a => a.Author_id == authorId).FirstOrDefault();

            if (authorPaperViewModel.Paper.Dislike.Contains(authorPaperViewModel.Author))
                authorPaperViewModel.Paper.Dislike.Remove(authorPaperViewModel.Author);

            authorPaperViewModel.Paper.Like.Add(authorPaperViewModel.Author);
            dbContext.SaveChanges(); 

            return View();  
        }

        [Route("paper/DisLike/{paperId}/{authorId}")]
        public ActionResult DisLike(int paperId , int authorId)
        {
            authorPaperViewModel.Paper = dbContext.Papers.Where(p => p.Paper_id == paperId).FirstOrDefault();
            authorPaperViewModel.Author = dbContext.Authors.Where(a => a.Author_id == authorId).FirstOrDefault();

            if (authorPaperViewModel.Paper.Like.Contains(authorPaperViewModel.Author))
                authorPaperViewModel.Paper.Like.Remove(authorPaperViewModel.Author);

            authorPaperViewModel.Paper.Dislike.Add(authorPaperViewModel.Author);
            dbContext.SaveChanges();

            return View();  
        }

        [Route("paper/Comment/{paperId}/{authorId}/{comment}")]
        public ActionResult Comment(int paperId , int authorId , string comment)
        { 
            DateTime dateTime = DateTime.Now;
            Models.Comment c = new Comment() {
                Author_id = authorId,
                Paper_id = paperId,
                Content = comment , 
                Time = dateTime 
            };  

            dbContext.Comments.Add(c);
            dbContext.SaveChanges();

            return View(); 
        }
    }
}