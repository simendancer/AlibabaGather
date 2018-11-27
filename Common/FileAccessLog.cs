using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class FileAccessLog
    {
        static string _directory = Environment.CurrentDirectory + "\\log\\";
        public static void SaveBugInFile(string filename, string errorString)
        {
            try
            {
                if (!Directory.Exists(_directory))
                    Directory.CreateDirectory(_directory);
                File.AppendAllText(string.Format("{1}/{0}.txt", filename, _directory), Environment.NewLine + "\n\n" + string.Format("{0}--{1}", errorString + Environment.NewLine, DateTime.Now) + "\n" + Environment.NewLine);
            }
            catch
            {
                File.AppendAllText(string.Format("D:/{0}.txt", filename), errorString);
            }
        }


        /// <summary>
        /// 保存错误日志
        /// </summary>
        /// <param name="msg"></param>
        public static void SaveBugInFile(string msg)
        {
            SaveBugInFile(DateTime.Now.Date.ToLongDateString() + "Log", msg);
        }

    }
}
