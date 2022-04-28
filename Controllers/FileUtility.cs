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
    public class FileUtility
    {
        private static readonly string RootPath = AppDomain.CurrentDomain.BaseDirectory + "/Files/";
        private static readonly string PapersPath = RootPath + "Papers/";
        private static readonly string ImagesPath = RootPath + "Images/";
        private static readonly string AuthorsImagesPath = ImagesPath + "Authors/";

        public static StreamReader GetPaperFile(string paperName)
        {
            return new StreamReader(PapersPath + paperName);
        }

        public static StreamReader GetAuthorImage(string imageName)
        {
            return new StreamReader(AuthorsImagesPath + imageName);
        }

    }
}