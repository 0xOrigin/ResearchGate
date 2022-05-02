using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Research_Gate.Controllers
{
    public class FileNameGenerator
    {
        public static string AuthorPhotoName(int AuthorId)
        {
            Random random = new Random(5);
            return AuthorId.ToString() + random.Next(100000000, 999999999).ToString() + ".jpg";
        }

        public static string PaperPhotoName(int PaperId, int AuthorId)
        {
            return PaperId + AuthorPhotoName(AuthorId);
        }
    }
}