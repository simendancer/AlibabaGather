using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkerSpace;

namespace ImageDownload
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ProductImage());
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("图片处理", e.Message, e.Source);
            }
        }
    }
}
