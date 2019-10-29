using NLog;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Configurations
{
    public class LoggerConfigurator
    {
        public string curDir = Directory.GetCurrentDirectory();
        public string envDir = Environment.CurrentDirectory;
        public Logger logger = NLogBuilder.ConfigureNLog(Path.Combine(AppDomain.CurrentDomain.RelativeSearchPath, "NLog.Config")).GetCurrentClassLogger();
    }
}
