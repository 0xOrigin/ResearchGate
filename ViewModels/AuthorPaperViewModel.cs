using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Research_Gate.Models;

namespace Research_Gate.ViewModels
{
    public class AuthorPaperViewModel
    {
        public Author Author { get; set; }
        public Paper Paper { get; set; }
    }
}