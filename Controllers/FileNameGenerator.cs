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
            string randomNums = GenerateRandomNumbers();
            string PhotoName = AuthorId.ToString();

            for (int i = 0; i < randomNums.Length; i++)
            {
                PhotoName += randomNums[i];
            }

            PhotoName += (AuthorId + (int)randomNums[0] + (int)randomNums[1]).ToString() + Extension;

            return PhotoName;
        }

        public static string GeneratePaperName(int PaperId, int AuthorId, string Extension)
        {
            return PaperId.ToString() + GenerateAuthorPhotoName(AuthorId, Extension);
        }


        private static string GenerateRandomNumbers()
        {
            return new Random().Next(999999).ToString();
        }
    }
}