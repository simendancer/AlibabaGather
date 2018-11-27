using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Tools.Tool;
using WebGather.Model;

namespace ImageDownload
{
    public partial class ProductImage : Form
    {
        ProductImageWorker worker = new ProductImageWorker();

        public ProductImage()
        {
            InitializeComponent();
        }

        private void ProductImage_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            txtThreadNum.Text = "1";
            TxtSavePath.Text = ConfigurationManager.AppSettings["ImgPath"].ToString();
            worker.IsExit = true;
        }

        //程序退出
        private void ProductImage_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!worker.IsExit)
            {
                MessageBox.Show("请先终止程序！");
                e.Cancel = true;
                return;
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (TxtSavePath.Text != null && !string.IsNullOrEmpty(TxtSavePath.Text))
            {
                BtnStart.Enabled = false;

                worker.downLoader.SavePath = TxtSavePath.Text;//设置下载路径
                worker.proId = txtProductId.Text.ToInt32();//传递指定产品ID
                worker.DisplayMessage = ShowLog;
                worker.IsExit = false;

                int threadCount = txtThreadNum.Text.ToInt32();
                if (threadCount > 1)
                    worker.StartWork(threadCount);
                else
                    worker.StartWork();

                TxtSavePath.Enabled = false;
                txtProductId.Enabled = false;
                txtThreadNum.Enabled = false;
            }
            else
                MessageBox.Show("请填写路径！！");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            worker.IsExit = true;
            BtnStart.Enabled = true;
            txtProductId.Enabled = true;
            TxtSavePath.Enabled = true;
            txtThreadNum.Enabled = true;
        }

        private void btnOpenLog_Click(object sender, EventArgs e)
        {
            WorkerSpace.LogHelper.OpenLogFolder(() =>
            {
                MessageBox.Show("文件夹不存在！");
            });
        }

        #region 辅助方法
        public void ShowLog(string msg)
        {
            msg = string.Format("[{0}]-{1}", DateTime.Now.ToShortTimeString(), msg);
            if (MsgList.InvokeRequired)
            {
                Action<string> action = m =>
                {
                    int cnt = MsgList.Items.Count;
                    if (cnt > 200)
                    {
                        MsgList.Items.Clear();
                        cnt = 0;
                    }
                    cnt++;
                    MsgList.Items.Add(m);
                    MsgList.SelectedIndex = cnt - 1;
                };
                this.Invoke(action, msg);
            }
            else
            {
                int cnt = MsgList.Items.Count;
                if (cnt > 20)
                {
                    MsgList.Items.Clear();
                    cnt = 0;
                }
                cnt++;
                MsgList.Items.Add(msg);
                MsgList.SelectedIndex = cnt - 1;
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

            msg = string.Format("失败:{0}", worker.FailCount);
            if (lbFail.InvokeRequired)
            {
                Action<string> _action = m =>
                {
                    lbFail.Text = m;
                };
                this.Invoke(_action, msg);
            }
            else
            {
                lbFail.Text = msg;
            }
        }
        #endregion
    }
}
