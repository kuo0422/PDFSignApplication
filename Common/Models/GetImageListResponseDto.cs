using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Models
{
    /// <summary>
    /// GetImageList 回應資料
    /// </summary>
    public class GetImageListResponseDto
    {
        /// <summary>
        /// 檔案ID 用來call下載檔案API
        /// </summary>
        public string DocumentID { get; set; }

        /// <summary>
        /// 總共幾頁
        /// </summary>
        public int Pages { get; set; }


    }
}