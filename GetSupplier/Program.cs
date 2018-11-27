using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkerSpace;

namespace GetSupplier
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
                Application.Run(new GetSuppilerList());
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("Ali蜘蛛：获取供应商信息", e.Message, e.Source);
            }
        }
    }
}
