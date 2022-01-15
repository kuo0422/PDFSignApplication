
using PDFSignApplication.Services;
using PDFSignApplication.ViewModels.Sign;
using Common.Models.Sign;
using Common.Commons;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace PDFSignApplication.Controllers
{
    
    public class SignController : Controller
    {
        private ILogger _logger;
        private ConvertTool _convertTool;
        public Txt _txt { get; set; }

        public SignController()
        {
            _logger = new Logger();
            _convertTool = new ConvertTool(_logger);
            _txt = new Txt();
        }


        // GET: Claim
        public ActionResult Index()
        {
           
            return View();
        }

        /// <summary>
        /// 首頁
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ActionResult Login(string token)
        {
            try
            {
                _logger.Info($"[Login]token={token}");

                #region cache資料抓取同意聲明書文字
                ObjectCache Cache = MemoryCache.Default;
                string CacheName = "AgreeDeclaration";
                bool isNotSet = Cache[CacheName] == null;

                if (isNotSet)
                {
                    CacheItemPolicy policy = new CacheItemPolicy();
                    policy.SlidingExpiration = TimeSpan.FromMinutes(60); // 若 60 分鐘內未呼叫此快取就會回收，若有呼叫則會再延長 60 分鐘
                    Cache.Add(new CacheItem(CacheName, _txt.DealTxt()), policy);
                }
                var TxtFormat = (List<TxtFormatViewModel>)Cache[CacheName];
                #endregion


                LoginViewModel loginViewModel = new LoginViewModel();
                loginViewModel.Url = Page.PreviewPdf;
                loginViewModel.TxtFormat = TxtFormat;

                return View(loginViewModel);
            }
            catch (Exception ex) 
            {
                _logger.Error("Login Page Error", ex);
                return View(Page.Error);
            }
        }

        /// <summary>
        /// 文件審核預覽頁面-方法1(viwer.html)
        /// </summary>
        /// <param name="previewPdfModel">檔案資訊</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PreviewPdf(PreviewPdfModel previewPdfModel)
        {
            try
            {
                _logger.Info($"[PreviewPdf]Model={_convertTool.ObjectToString<PreviewPdfModel>(previewPdfModel)}");

                if (previewPdfModel != null && String.IsNullOrEmpty(previewPdfModel.CaseNumber)) previewPdfModel.CaseNumber = "A0001";

                Session["CaseNumber"] = previewPdfModel.CaseNumber;

                ViewBag.SignResult = Page.SignResult;

                return View();
            }
            catch (Exception ex)
            {
                _logger.Error("PreviewPdf Page Error", ex);
                return View(Page.Error);
            }
        }

        /// <summary>
        /// 文件審核結果頁面
        /// </summary>
        /// <param name="signResultModel">文件審核結果</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SignResult(SignResultModel signResultModel)
        {
            try
            {
                _logger.Info($"[SignResult]Model={_convertTool.ObjectToString<SignResultModel>(signResultModel)}");

                SignResultViewModel data = new SignResultViewModel();
                
                if (signResultModel != null && signResultModel.ReturnCode == "S")
                {
                    data.Status = true;
                }
                else
                {
                    data.Status = false;
                }

                return View(Page.SignResult, data);
            }
            catch (Exception ex) 
            {
                _logger.Error("SignResult Page Error", ex);
                return View(Page.Error);
            }

        }


        /// <summary>
        /// 錯誤頁面
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ActionResult Error(string token = "")
        {
            _logger.Trace($"[Error]token={token}");

            ViewBag.Token = HttpUtility.UrlEncode(token);
            return View("~/Views/Shared/Error.cshtml");
        }


    }
}