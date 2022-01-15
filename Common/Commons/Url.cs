using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Common.Commons
{
    public class Url
    {
        public static string GetImageList { get; } = ConfigurationManager.AppSettings["GetImageListUrl"];
        public static string GetPageFile { get; } = ConfigurationManager.AppSettings["GetPageFileUrl"];
    }
}