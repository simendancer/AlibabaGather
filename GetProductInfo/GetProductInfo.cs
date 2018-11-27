using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetProductInfo
{
    public partial class GetProductInfo : Form
    {
        GetProductInfoWorker worker = new GetProductInfoWorker();

        public GetProductInfo()
        {
            InitializeComponent();
        }

        private void GetProductInfo_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            TxtLastID.Text = "1";
            worker.IsExit = true;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            this.Text = "AlibabaSpider：根据产品列表抓取产品信息";
            BtnStart.Enabled = false;
            BtnReset.Enabled = false;
            worker.IsExit = false;
            worker.downLoader.SavePath = ConfigurationManager.AppSettings["ImgPath"].ToString();
            worker.CompanyId = TxtCompanyId.Text.ToInt32();
            int sleepTime = TxtSpeed.Text.ToInt32();
            if (sleepTime > 0) worker.SleepTime = sleepTime;

            int threadCount = TxtLastID.Text.ToInt32();
            if (threadCount > 1)
                worker.StartWork(threadCount);
            else
                worker.StartWork();
            worker.DisplayMessage = ShowMessage;

            TxtLastID.Enabled = false;
            TxtCompanyId.Enabled = false;
        }

        //重置按钮
        private void BtnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定重置所有供应商列表吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (Bll.BllAlibaba_CompanyInfo.RestForAll())
                    ShowMessage("重置成功！");
            }
        }

        //终止按钮
        private void btnStop_Click(object sender, EventArgs e)
        {
            worker.IsExit = true;
            BtnStart.Enabled = true;
            BtnReset.Enabled = true;
            TxtLastID.Enabled = true;
            TxtCompanyId.Enabled = true;
        }

        private void GetProductInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!worker.IsExit)
            {
                MessageBox.Show("请先终止程序！");
                e.Cancel = true;
                return;
            }
        }

        private void btnOpenLog_Click(object sender, EventArgs e)
        {
            WorkerSpace.LogHelper.OpenLogFolder(() =>
            {
                MessageBox.Show("文件夹不存在！");
            });
        }

        #region 辅助方法
        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="msg"></param>
        public void ShowMessage(string msg)
        {
            msg = string.Format("[{0}]-{1}", DateTime.Now.ToShortTimeString(), msg);
            if (ListBox_msg.InvokeRequired)
            {
                Action<string> action = m =>
                {
                    int cnt = ListBox_msg.Items.Count;
                    if (cnt > 200)
                    {
                        cnt = 0;
                        ListBox_msg.Items.Clear();
                    }
                    cnt++;
                    ListBox_msg.Items.Add(m);
                    ListBox_msg.SelectedIndex = cnt - 1;
                };
                this.Invoke(action, msg);
            }
            else
            {
                int cnt = ListBox_msg.Items.Count;
                if (cnt > 25)
                {
                    cnt = 0;
                    ListBox_msg.Items.Clear();
                }
                cnt++;
                ListBox_msg.Items.Add(msg);
                ListBox_msg.SelectedIndex = cnt - 1;
            }

            msg = string.Format("成功:{0}", worker.SuccessedCount);
            if (lbSuccess.InvokeRequired)
            {
                Action<string> _action = m =>
                {
                    lbSuccess.Text = m;
                };
                this.Invoke(_action, msg);
            }
            else
            {
                lbSuccess.Text = msg;
            }
        }
        #endregion
    }
}
