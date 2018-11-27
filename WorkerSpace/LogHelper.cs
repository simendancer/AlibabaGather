using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Tool;

namespace WorkerSpace
{
    public class LogHelper
    {
        private static readonly string LogPath = ConfigurationManager.AppSettings["LogPath"];

        /// <summary>
        /// 打开日志文件
        /// </summary>
        /// <param name="noExistAction"></param>
        public static void OpenLog(Action noExistAction)
        {
            string dirPath = PathHandle.GetFilePath(LogPath.TrimEnd('/') + "/");
            string filePath = dirPath + "log_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + ".log";
            if (!Directory.Exists(filePath))
            {
                noExistAction();
            }
            else
            {
                System.Diagnostics.Process.Start(filePath);
            }
        }

        /// <summary>
        /// 打开日志所在文件夹
        /// </summary>
        /// <param name="noExistAction"></param>
        public static void OpenLogFolder(Action noExistAction)
        {
            string dirPath = PathHandle.GetFilePath(LogPath.TrimEnd('/') + "/");
            if (!Directory.Exists(dirPath))
            {
                noExistAction();
            }
            else
            {
                System.Diagnostics.Process.Start(dirPath);
            }
        }

        /// <summary>
        /// 写入数据库错误日志
        /// </summary>
        /// <param name="e"></param>
        public static void WriteLog(Exception e)
        {
            WebGather.Model.Sys_Error model = new WebGather.Model.Sys_Error();
            model.Message = e.Message;
            model.Date = DateTime.Now;
            model.Thread = Environment.MachineName;
            model.Logger = e.TargetSite.Name;
            model.Source = e.Source;
            model.Level = "";
            Bll.BllSys_Error.Insert(model);
        }

        /// <summary>
        /// 写入数据库错误日志
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="msg"></param>
        /// <param name="source"></param>
        public static void WriteLog(string logger, string msg, string source)
        {
            WebGather.Model.Sys_Error model = new WebGather.Model.Sys_Error();
            model.Message = msg;
            model.Date = DateTime.Now;
            model.Thread = Environment.MachineName;
            model.Logger = logger;
            model.Source = source;
            model.Level = "";
            Bll.BllSys_Error.Insert(model);
        }
    }
}
