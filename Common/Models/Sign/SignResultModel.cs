using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Models.Sign
{
    /// <summary>
    /// 文件簽署結果
    /// </summary>
    public class SignResultModel
    {
        /// <summary>
        /// 執行結果
        /// S=執行成功
        /// F=執行失敗
        /// </summary>
        public string ReturnCode { get; set; }


        /// <summary>
        /// 結果訊息
        /// ReturnCode <>0才會有值
        /// 不須加入驗證碼的Hash字串
        /// </summary>
        public string ReturnCodeDesc { get; set; }

    }
}