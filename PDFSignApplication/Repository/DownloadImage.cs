using Common.Commons;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PDFSignApplication.Repository
{
    /// <summary>
    /// 取得圖片檔
    /// </summary>
    public class DownloadImage : IDownloadImage
    {
        public byte[] Get(string documentID, int pageIdx, string fileName)
        {
            byte[] result = null;

            string TemplateUrl = Url.GetPageFile;
            TemplateUrl += "?DocumentID={0}&pageNum={1}&fileName={2}";
            string DownloadImageUrl = string.Format(TemplateUrl, documentID, pageIdx, fileName);

            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json;charset=UTF-8");
            request.AddHeader("Connection", "Keep-Alive");

            RestClient client = new RestClient(DownloadImageUrl);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                byte[] fileBytes = response.RawBytes;
                result = fileBytes;
            }

            return result;
        }
    }
}