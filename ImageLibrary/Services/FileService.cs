using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageLibrary.Services
{
    internal class FileService
    {
        /// <summary>
        /// 取得檔案路徑
        /// </summary>
        /// <param name="DocumentID">文件ID</param>
        /// <param name="pageNum">頁數</param>
        /// <returns></returns>
        internal string GetFilePath(string DocumentID, int pageNum)
        {
            Dictionary<string, string> FileList = new Dictionary<string, string>();
            FileList.Add("1000001001", "~/App_Data/Images/1000001001.png");
            FileList.Add("1000002001", "~/App_Data/Images/1000002001.png");
            FileList.Add("1000002002", "~/App_Data/Images/1000002002.png");
            FileList.Add("1000003001", "~/App_Data/Images/1000003001.png");
            FileList.Add("1000003002", "~/App_Data/Images/1000003002.png");
            FileList.Add("1000003003", "~/App_Data/Images/1000003003.png");

            var FilePath = "";

            var FileKey = $"{DocumentID}{pageNum.ToString("000")}";

            if (FileList.ContainsKey(FileKey))
            {
                FilePath = FileList[FileKey];
            }

            return HttpContext.Current.Server.MapPath(FilePath);
        }
    }
}