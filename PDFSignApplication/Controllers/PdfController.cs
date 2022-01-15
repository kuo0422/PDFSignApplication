using Common.Commons;
using PDFSignApplication.Repository;
using PDFSignApplication.Services;
using System;
using System.Web;
using System.Web.Mvc;

namespace PDFSignApplication.Controllers
{
    public class PdfController : Controller
    {
        private ILogger _logger;
        private ConvertTool _convertTool;

        public PdfController()
        {
            _logger = new Logger();
            _convertTool = new ConvertTool(_logger);
        }

        /// <summary>
        /// 取得PDF
        /// </summary>
        public ActionResult OpenPdf()
        {
            
            try
            {
                string casenumber = Convert.ToString(Session["CaseNumber"]);

                if (String.IsNullOrEmpty(casenumber)) return View("Error");


                Pdf Pdf = new Pdf();
                //var FileByte = Pdf.GetPdf(CaseNumber);
                var FileByte = Pdf.GetJpegsAndCombineToPdf(casenumber);

                if (FileByte != null)
                {
                    //回應File
                    var contentDispositionHeader = new System.Net.Mime.ContentDisposition
                    {
                        Inline = true,
                        FileName = DateTime.Now.ToString("yy-MM-dd-hhmmss") + ".pdf", //檔名
                        CreationDate = DateTime.Now
                    };
                    Response.Headers.Add("Content-Disposition", contentDispositionHeader.ToString());

                    return File(FileByte, System.Net.Mime.MediaTypeNames.Application.Pdf);
                }

                throw new Exception("not get file");
            }
            catch (Exception ex)
            {
                _logger.Error("取得pdf發生錯誤", ex);
                throw ex;
            }

        }

        
    }
}