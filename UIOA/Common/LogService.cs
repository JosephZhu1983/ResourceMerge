using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIOA.Common
{

    /// <summary>
    /// 日志操作类
    /// </summary>
    public class LogService
    {
        private static string logPath = string.Empty;
        /// <summary>
        /// 保存日志的文件夹
        /// </summary>
        public static string LogPath
        {
            get
            {
                if (logPath == string.Empty)
                {
                    logPath = AppDomain.CurrentDomain.BaseDirectory + @"log\";
                }
                return logPath;
            }
            set { logPath = value; }
        }

        private static string logFielPrefix = string.Empty;
        /// <summary>
        /// 日志文件前缀
        /// </summary>
        public static string LogFielPrefix
        {
            get { return logFielPrefix; }
            set { logFielPrefix = value; }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        public static void WriteLog(string logType, string msg)
        {
            try
            {
                System.IO.StreamWriter sw = System.IO.File.AppendText(LogPath + DateTime.Now.ToString("yyyyMMdd") + "_" + logType + ".Log");
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss: ") + msg);
                sw.Close();
            }
            catch
            { }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        public static void WriteLog(LogServiceType logType, string msg)
        {
            WriteLog(logType.ToString(), msg);
        }
    }

    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogServiceType
    {
        Info,
        Trace,
        Warning,
        Error,
        SQL
    }

}
