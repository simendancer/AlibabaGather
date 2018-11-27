using Bll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetProductList
{
    public partial class GetProductList : Form
    {
        GetProductListWorker worker = new GetProductListWorker();

        public GetProductList()
        {
            this.InitializeComponent();
        }

        private void GetProductList_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.TxtLastID.Text = "1";
            this.worker.IsExit = true;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            this.Text = "AlibabaSpider：根据供应商列表抓取产品URL";
            this.BtnStart.Enabled = false;
            this.BtnReset.Enabled = false;
            this.worker.IsExit = false;
            int sleepTime = this.TxtSpeed.Text.ToInt32();
            bool flag = sleepTime > 0;
            if (flag)
            {
                this.worker.SleepTime = sleepTime;
            }
            int threadCount = this.TxtLastID.Text.ToInt32();
            if (threadCount > 1)
            {
                this.worker.StartWork(threadCount);
            }
            else
            {
                this.worker.StartWork();
            }
            this.worker.DisplayMessage = new Action<string>(this.ShowMessage);
            this.TxtLastID.Enabled = false;
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定重置所有供应商列表吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (BllAlibaba_CompanyInfo.RestForAll())
                {
                    this.ShowMessage("重置成功！");
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.worker.IsExit = true;
            this.BtnStart.Enabled = true;
            this.BtnReset.Enabled = true;
            this.TxtLastID.Enabled = true;
        }

        private void GetProductList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.worker.IsExit)
            {
                MessageBox.Show("请先终止程序！");
                e.Cancel = true;
            }
        }

        private void btnOpenLog_Click(object sender, EventArgs e)
        {
            WorkerSpace.LogHelper.OpenLogFolder(() =>
            {
                MessageBox.Show("文件夹不存在！");
            });
        }

        public void ShowMessage(string msg)
        {
            msg = string.Format("[{0}]-{1}", DateTime.Now.ToShortTimeString(), msg);
            bool invokeRequired = this.ListBox_msg.InvokeRequired;
            if (invokeRequired)
            {
                Action<string> action = delegate (string m)
                {
                    int cnt2 = this.ListBox_msg.Items.Count;
                    bool flag2 = cnt2 > 200;
                    if (flag2)
                    {
                        cnt2 = 0;
                        this.ListBox_msg.Items.Clear();
                    }
                    this.ListBox_msg.Items.Add(m);
                    cnt2++;
                    this.ListBox_msg.SelectedIndex = cnt2 - 1;
                };
                base.Invoke(action, new object[]
                {
                    msg
                });
            }
            else
            {
                int cnt = this.ListBox_msg.Items.Count;
                bool flag = cnt > 25;
                if (flag)
                {
                    cnt = 0;
                    this.ListBox_msg.Items.Clear();
                }
                this.ListBox_msg.Items.Add(msg);
                cnt++;
                this.ListBox_msg.SelectedIndex = cnt - 1;
            }
            msg = string.Format("供应商:{0} 产品:{1}", this.worker.SuccessedCount, this.worker.ProSuccessedCount);
            bool invokeRequired2 = this.lbSuccess.InvokeRequired;
            if (invokeRequired2)
            {
                Action<string> _action = delegate (string m)
                {
                    this.lbSuccess.Text = m;
                };
                base.Invoke(_action, new object[]
                {
                    msg
                });
            }
            else
            {
                this.lbSuccess.Text = msg;
            }
        }
    }
}
