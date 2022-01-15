using Common.Commons;
using Common.Models;
using ImageLibrary.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace ImageLibrary.Controllers
{
    [RoutePrefix("api/image")]
    public class ImageController : ApiController
    {
        private ILogger _logger;
        private ConvertTool _convertTool;
        private FileService _fileService;

        public ImageController()
        {
            _logger = new Logger();
            _convertTool = new ConvertTool(_logger);
            _fileService = new FileService();
        }

        [Route("GetImageList")]
        public List<GetImageListResponseDto> GetImageList(string CaseNumber)
        {
            var GetImageList = new List<GetImageListResponseDto>();

            switch (CaseNumber) 
            {
                case "A0001":
                    GetImageList.Add(new GetImageListResponseDto { DocumentID = "1000001", Pages = 1 });
                    break;

                case "A0002":
                    GetImageList.Add(new GetImageListResponseDto { DocumentID = "1000002", Pages = 2 });
                    break;

                case "A0003":
                    GetImageList.Add(new GetImageListResponseDto { DocumentID = "1000003", Pages = 3 });
                    break;

            }

            return GetImageList;
        }

        [Route("GetPageFile")]
        public HttpResponseMessage GetPageFile(string documentID, int pageNum, string fileName)
        {
            

            try
            {
                //取得檔案路徑
                string FilePath = _fileService.GetFilePath(documentID, pageNum);

                //檔案轉byte array
                var Filebyte = _convertTool.FileToByteArray(FilePath);


                var Result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(Filebyte)
                };

                Result.Content.Headers.ContentDisposition =
                    new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                    {
                        FileName = fileName
                    };

                Result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                return Result;

            }
            catch (Exception ex)
            {
                _logger.Error("調閱影像失敗", ex);
            }

            return new HttpResponseMessage(HttpStatusCode.InternalServerError);

        }

        
    }
}
