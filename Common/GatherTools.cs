using System;
using System.IO;

namespace Common
{
    public class GatherTools
    {
        /// <summary>
        /// 写错误日志
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteErrorLog(string msg)
        {
            Tools.Tool.Log.WritePur(msg);
        }

        public static string ObjectConvertString(object obj)
        {
            try
            {
                return Tools.Usual.Utils.RemoveHtml(Convert.ToString(obj));
            }
            catch (Exception e)
            {
                WriteErrorLog("ObjectConvertString()-" + e.Message);
                return string.Empty;
            }
        }
        public static int ObjectConvertInt(object obj)
        {
            try
            {
                return int.Parse(obj.ToString());
            }
            catch (Exception e)
            {
                WriteErrorLog("ObjectConvertInt()-" + e.Message);
                return 0;
            }
        }

        public static long ObjectConvertLong(object obj)
        {
            try
            {
                return long.Parse(obj.ToString());
            }
            catch (Exception e)
            {
                WriteErrorLog("ObjectConvertInt()-" + e.Message);
                return 0;
            }
        }

        public static bool ObjectConvertBoolean(object obj)
        {
            try
            {
                return bool.Parse(obj.ToString());
            }
            catch (Exception e)
            {
                WriteErrorLog("ObjectConvertBoolean()-" + e.Message);
                return false;
            }
        }

        public static decimal ObjectConvertDecimal(object obj)
        {
            try
            {
                return decimal.Parse(obj.ToString());
            }
            catch (Exception e)
            {
                WriteErrorLog("ObjectConvertDecimal()-" + e.Message);
                return 0;
            }
        }

        public static int ConvertToInt(string str)
        {
            int i = 0;
            if (int.TryParse(str, out i))
                return i;
            return 0;
        }

        public static bool ConvertToBool(string str)
        {
            bool i = false;
            if (bool.TryParse(str, out i))
                return i;
            return false;
        }

        public static byte ConvertToByte(string str)
        {
            byte i = 0;
            if (byte.TryParse(str, out i))
                return i;
            return 0;
        }

        public static string GetFileName(string fileName)
        {
            return Tools.Usual.Common.GetDataShortRandom() + Path.GetExtension(fileName).ToLower();
        }

        public static string GetVerifyLogoName(string logoName)
        {
            if (string.IsNullOrEmpty(logoName))
                return "";
            return Math.Abs(logoName.Trim2().ToLower().GetHashCode()) + ".gif";
        }
    }
}
