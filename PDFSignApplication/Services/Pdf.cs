using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Common.Models;
using PDFSignApplication.Repository;
using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using Newtonsoft.Json;

namespace PDFSignApplication.Services
{
    /// <summary>
    /// PDF相關作業
    /// </summary>
    public class Pdf
    {

        /// <summary>
        /// 呼叫API取得檔案資訊
        /// </summary>
        private IGetImageList _getImageList { get;}

        /// <summary>
        /// 呼叫API取得檔案
        /// </summary>
        private IDownloadImage _downloadImage { get; }

        public Pdf()
        {
            _getImageList = new GetImageList();
            _downloadImage = new DownloadImage();

        }

        public Pdf(IGetImageList getImageList, IDownloadImage downloadImage)
        {
            _getImageList = getImageList;
            _downloadImage = downloadImage;

        }

        /// <summary>
        /// 取得pdf的byte array
        /// </summary>
        /// <param name="token"></param>
        /// <returns>byte[]格式檔案</returns>
        public byte[] GetPdf(string CaseNumber)
        {
            //取得圖片檔
            //呼叫API取得DocumentID、Page
            List<GetImageListResponseDto> Document = _getImageList.Get(CaseNumber);
            byte[] fileBtye = null;
            if (Document != null && Document.Count != 0)
            {
                //呼叫API取得檔案
                fileBtye = _downloadImage.Get(Document[0].DocumentID, 1, $"{CaseNumber}.pdf");
            }

            //(測試用)存pdf檔
            //File.WriteAllBytes($"D:/00-Data/{CaseNumber}.pdf", fileBtye);

            return fileBtye;
        }


        /// <summary>
        /// 取得jpeg檔案
        /// </summary>
        /// <param name="token"></param>
        /// <returns>byte[]格式檔案</returns>
        public byte[] GetJpegsAndCombineToPdf(string CaseNumber)
        {

            //呼叫API取得DocumentID、Page
            List<GetImageListResponseDto> Document = _getImageList.Get(CaseNumber);

            List<byte[]> JpgBytes = new List<byte[]>();

            byte[] fileBtye = null;
            if (Document != null && Document.Count != 0)
            {
                for(int page = 1; page <= Document[0].Pages; page++)
                {
                    //呼叫API取得檔案
                    fileBtye = _downloadImage.Get(Document[0].DocumentID, page, $"{CaseNumber}-{page}.jpeg");
                    JpgBytes.Add(fileBtye);
                }
                
            }

            //for (int i = 0; i < JpgBytes.Count; i++)
            //{
            //    //(測試用)存檔
            //    System.IO.File.WriteAllBytes($"D:/00-Data/{CaseNumber}-{i + 1}.jpg", JpgBytes[i]);
            //}

            //將圖片檔們轉成pdf的byte array
            byte[] pdfBytes = ImagesByteArrayToPdf(JpgBytes);

            //(測試用)存pdf檔
            //File.WriteAllBytes($"D:/00-Data/{CaseNumber}.pdf", pdfBytes);

            return pdfBytes;

        }

        

        /// <summary>
        /// 多張 images byte array to pdf
        /// </summary>
        /// <param name="JpgBytes">圖片們</param>
        /// <returns></returns>
        private byte[] ImagesByteArrayToPdf(List<byte[]> JpgBytes)
        {
            using (MemoryStream ms = new MemoryStream())
            using (PdfDocument pdf = new PdfDocument(new PdfWriter(ms).SetSmartMode(true)))
            {

                //Create A Document
                Document document = new Document(pdf, PageSize.A4);

                //Add Page
                foreach (var jpg in JpgBytes)
                {
                    ImageData imagedata = ImageDataFactory.Create(jpg);
                    iText.Layout.Element.Image coverImage = new iText.Layout.Element.Image(imagedata);

                    document.Add(coverImage);
                }

                //document.Add(new iText.Layout.Element.AreaBreak()); //多加空白頁

                document.Close();

                // Close pdf
                pdf.Close();

                // Return array
                return ms.ToArray();
            }
        }
    }
}