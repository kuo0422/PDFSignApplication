using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFSignApplication.Repository
{
    /// <summary>
    /// 由序號取得圖片資訊
    /// </summary>
    public interface IGetImageList
    {
        List<GetImageListResponseDto> Get(string caseNumber);
    }
}
