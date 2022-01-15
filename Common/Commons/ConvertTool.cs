using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Common.Commons
{
    public class ConvertTool
    {

        private ILogger _logger { get; set; }
        public ConvertTool(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// pdf轉base64
        /// </summary>
        /// <param name="fileName">檔案相對路徑</param>
        /// <returns></returns>
        public string PdfToBase64(string fileName)
        {

            byte[] pdfBytes = File.ReadAllBytes(HttpContext.Current.Server.MapPath(fileName));
            string pdfBase64 = Convert.ToBase64String(pdfBytes);

            return pdfBase64;
        }

        /// <summary>
        /// 檔案轉ByteArray
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public byte[] FileToByteArray(string filePath)
        {

            byte[] filebyte = null;

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // Create a byte array of file stream length
                byte[] bytes = System.IO.File.ReadAllBytes(filePath);

                //Read block of bytes from stream into the byte array
                fs.Read(bytes, 0, System.Convert.ToInt32(fs.Length));

                //Close the File Stream
                fs.Close();

                filebyte = bytes;
            }

            return filebyte;
        }

        /// <summary>
        /// 字串轉物件
        /// </summary>
        public T StringToObject<T>(string model)
            where T : new()
        {
            try
            {
                //取得服務結果
                var TransInfo = JsonConvert.DeserializeObject<T>(model);
                return TransInfo;
            }
            catch (Exception ex)
            {
                _logger.Error("字串轉物件-失敗", ex);
                return default;
            }
        }

        /// <summary>
        /// 物件轉字串
        /// </summary>
        public string ObjectToString<T>(T responseModel)
            where T : new()
        {
            try
            {
                var Result = JsonConvert.SerializeObject(responseModel);

                return Result;
            }
            catch (Exception ex)
            {
                _logger.Error("物件轉字串-失敗", ex);
                return default;
            }
        }

    }
}