using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Research_Gate.Controllers
{
    public class FileNameGenerator
    {
        public static string GenerateAuthorPhotoName(int AuthorId, string Extension)
        {
            Random random = new Random(5);
            int Fnumber = random.Next(1, 9);
            int Snumber = random.Next(9);
            string PhotoName = AuthorId.ToString() + Fnumber.ToString() + Snumber.ToString() +
                random.Next(0000, 9999).ToString() + (AuthorId + Fnumber + Snumber).ToString() + Extension;
            return PhotoName;
        }

        public static string GeneratePaperName(int PaperId, int AuthorId, string Extension)
        {
            return PaperId.ToString() + GenerateAuthorPhotoName(AuthorId, Extension);
        }
    }
}