using Common.Commons;
using Common.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PDFSignApplication.Repository
{
    /// <summary>
    /// 由序號取得圖片資訊
    /// </summary>
    public class GetImageList : IGetImageList
    {
        public List<GetImageListResponseDto> Get(string caseNumber)
        {
            List<GetImageListResponseDto> result = null;
            
            string TemplateUrl = Url.GetImageList;
            TemplateUrl += "?CaseNumber={0}";
            string GetImageListUrl = string.Format(TemplateUrl, caseNumber);

            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json;charset=UTF-8");

            RestClient client = new RestClient(GetImageListUrl);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                result = (List<GetImageListResponseDto>)JsonConvert.DeserializeObject(response.Content, typeof(List<GetImageListResponseDto>));
            }
            
            return result;
        }
    }
}