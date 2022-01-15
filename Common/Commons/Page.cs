using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Common.Commons
{
    public class Page
    {

        /// <summary>
        /// 查看pdf頁面
        /// </summary>
        public static string PreviewPdf { get; } = "PreviewPdf";

        /// <summary>
        /// 審核完成頁面
        /// </summary>
        public static string SignResult { get; } = "SignResult";

        /// <summary>
        /// 發生錯誤頁面
        /// </summary>
        public static string Error { get; } = "Error";

    }
}