using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Commons
{
    public interface ILogger
    {
        void Info(string msg);
        void Trace(string msg);
        void Error(string msg);
        void Error(string msg, Exception err);
        void Fatal(string msg);
        void Fatal(string msg, Exception err);
    }
}
