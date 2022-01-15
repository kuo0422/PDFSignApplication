using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PDFSignApplication.ViewModels.Sign
{
    public class LoginViewModel
    {
        /// <summary>
        /// Post Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 檔案代號
        /// </summary>
        public string CaseNumber { get; set; }

        /// <summary>
        /// 聲明要呈現的文字
        /// </summary>
        public List<TxtFormatViewModel> TxtFormat { get; set; }

    }
}