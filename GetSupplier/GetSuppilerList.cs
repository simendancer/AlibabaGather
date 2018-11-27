using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using NSoup;
using NSoup.Nodes;
using NSoup.Select;
using WebGather.Model;

namespace GetSupplier
{
    public partial class GetSuppilerList : Form
    {
        bool IsExit = true; //是否程序退出标记
        Thread thread = null;
        private int sleepTime = 200;
        private DateTime AppStartTime { get; set; }//程序开启时间
        private int CurrentPage = 0;
        private int MaxPageCount = 1;
        private int SuccessCount = 0;
        private int ClassId = 0;

        public GetSuppilerList()
        {
            InitializeComponent();
        }

        private void GetSuppiler_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            AppStartTime = new DateTime(2000, 1, 1);
            TxtLastID.Text = Setting.LastCatelogID.ToString();
        }

        //开始按钮
        private void BtnStart_Click(object sender, EventArgs e)
        {
            this.Text = "AlibabaSpider：根据产品分类抓取供应商URL";
            IsExit = false;
            sleepTime = Common.GatherTools.ConvertToInt(TxtSpeed.Text);
            AppStartTime = DateTime.Now;
            ClassId = Common.GatherTools.ConvertToInt(TxtLastID.Text);
            BtnStart.Enabled = false;
            Thread_init();//初始化线程并开始            
        }

        //初始化进程
        public void Thread_init()
        {
            thread = new Thread(OtherThreadWorking);
            thread.IsBackground = true;
            thread.Start();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定重置所有分类吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (Bll.BllAlibaba_ProClass.RestForAll())
                    ShowMessage("重置成功！");
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            BtnStart.Enabled = true;
            IsExit = true;
        }

        private void GetSuppiler_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsExit)
            {
                MessageBox.Show("请先终止程序！");
                e.Cancel = true;
                return;
            }
        }

        /// <summary>
        /// 获取分类分页格式
        /// </summary>
        /// <param name="classID"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        private string GetPageUrl(int classId, int pageIndex)
        {
            return string.Format("http://www.alibaba.com/trade/search?indexArea=company_en&n=50&category={0}&page={1}", classId, pageIndex);
        }

        /// <summary>
        /// 抓取公司地址并翻页
        /// </summary>
        /// <param name="ClsRow"></param>
        protected void GatherCompanyURL()
        {
            Alibaba_ProClass classMod = null;
            List<Alibaba_ProClass> classList = null;
            if (ClassId == 0)
            {
                ShowMessage("数据库读取分类数据....");
                classList = Bll.BllAlibaba_ProClass.Query(o => o.PageCount >= o.CurrentPage, o => o.ID, "asc", null, 1);
                if (classList == null || classList.Count < 1)
                {
                    IsExit = true;
                    ShowMessage("所有分类分析完成!");
                    return;
                }
                classMod = classList[0];
            }
            else
            {
                classMod = Bll.BllAlibaba_ProClass.First(o => o.AlibabaID == ClassId);
                if (classMod == null)
                {
                    IsExit = true;
                    ShowMessage("分类不存在!");
                    return;
                }
            }
            ShowMessage(string.Format("当前抓取分类：{0}", classMod.ClassName));
            CurrentPage = (int)classMod.CurrentPage;
            CurrentPage = CurrentPage > 0 ? CurrentPage : 1;
            MaxPageCount = 1;
            do
            {
                string url = GetPageUrl((int)classMod.AlibabaID, CurrentPage);
                //数据采集
                SaveCompanyURL(classMod, url);
                Thread.Sleep(sleepTime);
            }
            while (CurrentPage++ < MaxPageCount && !IsExit);

            //更新分类当前页
            var cMode = new Alibaba_ProClass();
            cMode.CurrentPage = CurrentPage;
            Bll.BllAlibaba_ProClass.Update(cMode, o => o.ID == classMod.ID);
        }

        /// <summary>
        /// 根据URL保存数据
        /// </summary>
        /// <param name="ClsRow"></param>
        /// <param name="url"></param>
        protected void SaveCompanyURL(Alibaba_ProClass clsMod, string url)
        {
            ShowMessage("分析:" + url);

            //发送请求
            var httpResult = new Tools.Helper.HttpHelper("UTF-8").Get(url);
            if (httpResult.StatusCode != 200)
            {
                ShowMessage(string.Format("跳过：{0}", url));
                return;
            }
            //获取页面HTML
            string pageHtml = string.Empty;
            pageHtml = httpResult.ResultHtml;
            if (string.IsNullOrEmpty(pageHtml))
            {
                ShowMessage(string.Format("跳过：{0}", url));
                return;
            }
            //转换为Nsoup文档
            Document doc = NSoupClient.Parse(pageHtml);
            if (MaxPageCount <= 1)
            {
                MaxPageCount = GetMaxPageCount(doc);

                //更新分类最大页
                var cMode = new Alibaba_ProClass();
                cMode.PageCount = MaxPageCount;
                Bll.BllAlibaba_ProClass.Update(cMode, o => o.ID == clsMod.ID);
            }
            Alibaba_CompanyGather model = null;
            var supplierList = GetSupplierList(doc);
            if (supplierList == null && supplierList.Count == 0)
            {
                ShowMessage("页面无数据！");
                IsExit = true;
            }
            foreach (var m in supplierList)
            {
                try
                {
                    model = new Alibaba_CompanyGather();
                    model.CompanyUrl = m.Select("h2.title a").Attr("href").Replace("/company_profile.html#top-nav-bar", string.Empty).Replace("#top-nav-bar", string.Empty);
                    if (string.IsNullOrEmpty(model.CompanyUrl))
                        continue;
                    //数据去重
                    if (chkIsExist.Checked && Bll.BllAlibaba_CompanyGather.IsExists(model.CompanyUrl))
                        continue;
                    model.ClassId = clsMod.ID;
                    model.ClassParPath = clsMod.ParPath;
                    model.Grade = 1270;
                    model.InsertTime = DateTime.Now;
                    model.CurrentPage = 0;
                    model.PageCount = 1;
                    model.Tag = 0;
                    model.Country = m.Select("img.flag")?.First?.NextElementSibling.Text() ?? "";

                    if (Bll.BllAlibaba_CompanyGather.Insert(model) > 0) SuccessCount++;
                }
                catch (Exception exp)
                {
                    Tools.Tool.Log.WritePur(exp.ToString());
                }
            }
            ShowMessage(string.Format("第{1}页匹配结果：{0}", supplierList.Count, CurrentPage));
        }

        /// <summary>
        /// 辅助线程
        /// </summary>
        public void OtherThreadWorking()
        {
            do
            {
                try
                {
                    GatherCompanyURL();
                }
                catch (Exception ex)
                {
                    Tools.Tool.Log.WritePur(ex.ToString());
                }
            }
            while (!IsExit);
            ShowMessage("线程停止工作!!");
        }

        /// <summary>
        /// 获取最大页数
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private int GetMaxPageCount(Document doc)
        {
            int a = 1;
            var maxBtn = doc.Select(".ui2-pagination-pages a.next").First;
            if (maxBtn != null && maxBtn.HasText && int.TryParse(maxBtn.PreviousElementSibling.Text(), out a) && a > 1)
                return a;
            else
                return 1;
        }

        /// <summary>
        /// 获取供应商列表
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private Elements GetSupplierList(Document doc)
        {
            var list = doc.Select("div.m-item");
            return list;
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
                    ListBox_msg.Items.Add(m);
                    cnt++;
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
                ListBox_msg.Items.Add(msg);
                cnt++;
                ListBox_msg.SelectedIndex = cnt - 1;
            }

            msg = string.Format("供应商:{0}", SuccessCount);
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
