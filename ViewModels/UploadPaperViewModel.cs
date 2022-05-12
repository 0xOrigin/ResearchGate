using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Research_Gate.Models;

namespace Research_Gate.ViewModels
{
    public class UploadPaperViewModel
    {
        public static IEnumerable<Author> Authors { get; set; }

        public string[] SelectedAuthors { get; set; }

        public Paper Paper { get; set; }
    }
}