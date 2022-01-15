using PDFSignApplication.ViewModels.Sign;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PDFSignApplication.Services
{
    /// <summary>
    /// txt相關作業
    /// </summary>
    public class Txt
    {
        /// <summary>
        /// 取得同意聲明書文字
        /// </summary>
        /// <returns></returns>
        public List<TxtFormatViewModel> DealTxt()
        {
            //讀檔
            var FilePath = HttpContext.Current.Server.MapPath(@"~/App_Data/Files/AgreeDeclaration.txt");
            var FileData = GetTxt(FilePath);

            //格式轉換
            var Result = new List<TxtFormatViewModel>();

            foreach (var data in FileData)
            {
                string[] d = data.Split('|');
                Result.Add(new TxtFormatViewModel { Txt = d[0], Location = d[1], Size= d[2], Padding = d[3] });
            }

            return Result;


        }

        /// <summary>
        /// 讀取檔案
        /// </summary>
        /// <param name="filePath">檔案路徑</param>
        /// <returns></returns>
        private List<string> GetTxt(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            return lines.ToList();
        }

    }
}