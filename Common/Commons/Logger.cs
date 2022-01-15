using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Common.Commons
{
    public class Logger : ILogger
    {
        private readonly NLog.Logger _logger;
        private string _guid;
        private string _guidMessage { get; set; }
        private string _errorMessage { get; set; }

        public Logger()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _guid = Guid.NewGuid().ToString();
            _guidMessage = $"GUID：{_guid}";
            _errorMessage = $"\r\n錯誤訊息：";
        }

        private void SetProperties(MethodBase m, LogEventInfo theEvent)
        {
            theEvent.Properties["guid"] = _guid;
            theEvent.Properties["methodname"] = String.Format("{0}.{1}", m.ReflectedType.FullName, m.Name);
        }

        #region Debug
        public void Debug(string msg)
        {
            // Get call stack
            StackTrace stackTrace = new StackTrace();
            LogEventInfo theEvent = new LogEventInfo(LogLevel.Debug, "", msg);
            SetProperties(stackTrace.GetFrame(1).GetMethod(), theEvent);
            _logger.Log(theEvent);
        }

        #endregion

        #region Info
        public void Info(string msg)
        {
            // Get call stack
            StackTrace stackTrace = new StackTrace();
            LogEventInfo theEvent = new LogEventInfo(LogLevel.Info, "", msg);
            SetProperties(stackTrace.GetFrame(1).GetMethod(), theEvent);
            _logger.Log(theEvent);
        }

        #endregion

        #region Warn
        public void Warn(string msg, MethodBase m)
        {
            //_logger.Warn(msg);
            LogEventInfo theEvent = new LogEventInfo(LogLevel.Warn, "", msg);
            SetProperties(m, theEvent);
            _logger.Log(theEvent);
        }

        #endregion

        #region Trace
        public void Trace(string msg)
        {
            // Get call stack
            StackTrace stackTrace = new StackTrace();
            LogEventInfo theEvent = new LogEventInfo(LogLevel.Trace, "", msg);
            SetProperties(stackTrace.GetFrame(1).GetMethod(), theEvent);
            _logger.Log(theEvent);
        }

        #endregion

        #region Error
        public void Error(string msg)
        {
            LogEventInfo theEvent = new LogEventInfo
            {
                LoggerName = "",
                Level = LogLevel.Error,
                Message = _guidMessage + _errorMessage + msg
            };

            // Get call stack
            StackTrace stackTrace = new StackTrace();
            SetProperties(stackTrace.GetFrame(1).GetMethod(), theEvent);
            LogManager.Configuration.Variables["subject"] = $"({_guid}){msg}";
            _logger.Log(theEvent);
        }

        public void Error(string msg, Exception err)
        {
            LogEventInfo theEvent = new LogEventInfo
            {
                LoggerName = "",
                Level = LogLevel.Error,
                Message = _guidMessage + _errorMessage + msg,
                Exception = err
            };

            // Get call stack
            StackTrace stackTrace = new StackTrace();
            SetProperties(stackTrace.GetFrame(1).GetMethod(), theEvent);

            LogManager.Configuration.Variables["subject"] = $"({_guid}){msg}";
            _logger.Log(theEvent);
        }
        #endregion

        #region Fatal
        public void Fatal(string msg)
        {
            LogEventInfo theEvent = new LogEventInfo
            {
                LoggerName = "",
                Level = LogLevel.Fatal,
                Message = _guidMessage + _errorMessage + msg,
            };

            // Get call stack
            StackTrace stackTrace = new StackTrace();
            SetProperties(stackTrace.GetFrame(1).GetMethod(), theEvent);

            LogManager.Configuration.Variables["subject"] = $"({_guid}){msg}";
            _logger.Log(theEvent);
        }

        public void Fatal(string msg, Exception err)
        {
            LogEventInfo theEvent = new LogEventInfo
            {
                LoggerName = "",
                Level = LogLevel.Fatal,
                Message = _guidMessage + _errorMessage + msg,
                Exception = err
            };

            // Get call stack
            StackTrace stackTrace = new StackTrace();
            SetProperties(stackTrace.GetFrame(1).GetMethod(), theEvent);

            LogManager.Configuration.Variables["subject"] = $"({_guid}){msg}";
            _logger.Log(theEvent);
        }
        #endregion

    }
}