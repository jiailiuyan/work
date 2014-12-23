using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using log4net;

namespace WorkCommon.Manager
{
    class Log4Wrap
    {
        const string LOG_FOLDER = "logs";
        const string LOG_CONFIG_FOLDER = "log4net_config";
        const string LOGGER_NAME = "CKLog";
        const string MaxSizeRollBackups = "10";
        const string MaxFileSize = "500KB";

        private static ILog logger = null;
        private static string configPath = string.Empty;
        private static string logfilename = string.Empty;
        private static CSLogger output;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static Log4Wrap()
        {
            try
            {
                string moduleName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                string moduleFolder = GlobalLog.Instance.UserCustomerConfigFloder;
                string filename = moduleName.Remove(0, moduleName.LastIndexOf("\\") + 1);
                string logConfigFolder = moduleFolder + "\\" + LOG_CONFIG_FOLDER;

                moduleName = moduleName.Remove(0, moduleName.LastIndexOf("\\") + 1);
                string logFolderFullPath = Path.Combine(GlobalLog.Instance.UserCustomerConfigFloder, LOG_FOLDER);
                logfilename = LOG_FOLDER + "\\" + moduleName + "_" + DateTime.Now.ToString("o") + ".log";
                logfilename = logfilename.Replace(":", ".");
                logfilename = Path.Combine(GlobalLog.Instance.UserCustomerConfigFloder, logfilename);

                if (!Directory.Exists(logConfigFolder))
                {
                    Directory.CreateDirectory(logConfigFolder);
                }

                if (Directory.Exists(logFolderFullPath) == false)
                {
                    Directory.CreateDirectory(logFolderFullPath);
                }

                configPath = logConfigFolder + "\\" + filename + ".xml";

                if (!File.Exists(configPath))
                {
                    CreateDefaultConfigFile(configPath);
                }
                else
                {
                    XElement root = XElement.Load(configPath);
                    root.Descendants("appender").First(n => n.Attribute("name").Value == "RollingFileAppender").Element("file").Attribute("value").Value = logfilename;
                    root.Save(configPath);
                }

                log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(configPath));
                logger = LogManager.GetLogger(LOGGER_NAME);


            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 公开Logger
        /// </summary>
        public static ILog Logger
        {
            get
            {
                return logger;
            }
        }

        /// <summary>
        /// 日志配置文件路径
        /// </summary>
        public static string ConfigPath
        {
            get
            {
                return configPath;
            }
        }

        private static void CreateDefaultConfigFile(string filename)
        {
            string configString = string.Empty;
            configString =
@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<configuration>
    <configSections>
        <section name=""log4net"" type=""log4net.Config.Log4NetConfigurationSectionHandler,log4net-net-1.2"" />
    </configSections>

    <log4net>
        <logger name=""" + LOGGER_NAME + @""">
            <level value=""INFO"" />
            <appender-ref ref=""RollingFileAppender"" />
            <appender-ref ref=""ConsoleAppender"" />
        </logger>

        <appender name=""ConsoleAppender""  type=""log4net.Appender.ConsoleAppender"" >
            <layout type=""log4net.Layout.PatternLayout"">
                <param name=""ConversionPattern""  value=""%date [%-5level] [Thrd:%thread] %l - %message%newline""/>
            </layout>
        </appender>

        <appender name=""RollingFileAppender"" type=""log4net.Appender.RollingFileAppender"">
            <file value=""" + logfilename + @""" />
            <appendToFile value=""true"" />
            <rollingStyle value=""Size"" />
            <maxSizeRollBackups value=""" + MaxSizeRollBackups + @""" />
            <maximumFileSize value=""" + MaxFileSize + @""" />
            <staticLogFileName value=""true"" />
            <layout type=""log4net.Layout.PatternLayout"">
                <conversionPattern value=""%date [%-5level] [Thrd:%thread] %l - %message%newline"" />
            </layout>
        </appender>
    </log4net>
</configuration>";
            try
            {
                StreamWriter sw = File.CreateText(filename);
                sw.Write(configString);
                sw.Close();
                sw.Dispose();
            }
            catch (Exception ex)
            {
                string info = ex.Message;
            }
        }
    }
}
