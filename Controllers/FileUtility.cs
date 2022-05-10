using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.Remoting.Channels;
using System.Security.Policy;
using System.Web;
using System.Web.Configuration;
using System.Web.Management;
using Microsoft.Owin.Host.SystemWeb;

namespace Research_Gate.Controllers
{
    public static class FileUtility
    {
        private static readonly string RootPath = "~/Files/";
        private static readonly string PapersPath = RootPath + "Papers/";
        private static readonly string ImagesPath = RootPath + "Images/";
        private static readonly string AuthorsImagesPath = ImagesPath + "Authors/";

        private static readonly string DefaultAuthorImage = "profile_default.jpg";

        public static string GetPaperFile(string paperName)
        {
            return (PapersPath + paperName);
        }

        public static string GetAuthorImage(string imageName)
        {
            string imagePath = AuthorsImagesPath + imageName;
            
            if (!File.Exists(HttpContext.Current.Server.MapPath(imagePath)))
                imagePath = AuthorsImagesPath + DefaultAuthorImage;

            return imagePath;
        }

        public static string GetAuthorImageRelativePath(string imageName)
        {
            return AuthorsImagesPath + imageName; 
        }

        public static string GetPaperFileStorePath(string paperFile)
        {
            return HttpContext.Current.Server.MapPath(PapersPath + paperFile);
        }

        public static string GetAuthorImageStorePath(string authorImage)
        {
            return HttpContext.Current.Server.MapPath(AuthorsImagesPath + authorImage);
        }

        public static string GetDefaultImageName()
        {
            return DefaultAuthorImage;
        }

    }
}