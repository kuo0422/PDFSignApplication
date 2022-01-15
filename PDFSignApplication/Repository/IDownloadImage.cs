using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PDFSignApplication.Repository
{
    /// <summary>
    /// 取得圖片檔
    /// </summary>
    public interface IDownloadImage
    {
        byte[] Get(string documentID, int pageIdx, string fileName);
    }
}