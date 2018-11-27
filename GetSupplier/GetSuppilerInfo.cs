using AccountLogin.Ali;
using Common;
using Common.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using WebGather.Model;

namespace GetSupplier
{
    public partial class GetSuppilerInfo : Form
    {
        GetSupplierInfo Worker = new GetSupplierInfo();
        //存储公司地址
        protected Queue<Alibaba_CompanyGather> CompanyUrlQueue = new Queue<Alibaba_CompanyGather>();
        protected Hashtable CompanyField = Hashtable.Synchronized(new Hashtable());
        protected int QueueMaxCount = 10; //允许队列最大数值
        protected int QueueMinCount = 8; //队列的最小长度
        protected int CurrentDequeueNum = 0; //当前出队编号
        protected int LogTag = 0;//记录日志计时器
        protected DateTime AppStartTime = new DateTime(2000, 1, 1);
        protected bool IsBeginGather = false;//是否开始获取数据
        protected bool IsExit = false;
        protected Thread[] ThreadOne = null;
        protected int SuccessCount = 0;
        protected int FailCount = 0;

        public GetSuppilerInfo()
        {
            InitializeComponent();
            InitAliClient();//初始化 AliClient，相当于打开一个浏览器，并设置一个空的cookie
        }

        #region 主体部分

        //检查队列是否可以出队
        public bool IsCanDeQueue()
        {
            if (CompanyUrlQueue.Count == 0)
                return false;
            return true;
        }

        //队列出队
        public Alibaba_CompanyGather CompanyUrlDequeue()
        {
            if (IsCanDeQueue())
                return CompanyUrlQueue.Dequeue();
            return null;
        }

        private void GetSuppilerInfo_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            this.Text = "Ali蜘蛛：抓取企业信息-指定TAG ->" + ConfigurationManager.AppSettings["Tag"]?.ToLower() ?? "0";
            TxtSavePath.Text = ConfigurationManager.AppSettings["ImgPath"].ToString();
        }

        //程序退出
        private void GetSuppilerInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            Worker.AddMessage("程序退出中...");
            IsExit = true;
            IsBeginGather = false;
            //窗体关闭后，阻止后台线程继续
            while (CurrentDequeueNum > 0)
            {
                try
                {
                    Bll.BllAlibaba_CompanyGather.UpdateTag(CurrentDequeueNum, (int)Enums.GatherResult.InsertFailed);
                    if (IsCanDeQueue())
                    {
                        CurrentDequeueNum = CompanyUrlDequeue().ID;
                    }
                    else
                        CurrentDequeueNum = -1;
                }
                catch (Exception ex)
                {
                    GatherTools.WriteErrorLog("GetSuppilerInfo_FormClosing()-" + ex.Message);
                }
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (TxtSavePath.Text != null && !string.IsNullOrEmpty(TxtSavePath.Text))
            {
                Worker.SavePath = TxtSavePath.Text;//传递图片保存路径
                BtnStart.Enabled = false;
                AppStartTime = DateTime.Now;
                IsBeginGather = true;
                Thread_Init();
            }
            else
                MessageBox.Show("请选择路径！！");
        }

        public void Thread_Init()
        {
            ThreadOne = new Thread[GatherTools.ConvertToInt(ConfigurationManager.AppSettings["ThreadCount"])];
            Worker.AddMessage("正在初始化线程....");
            for (int i = 0; i < ThreadOne.Length; i++)
            {
                ThreadOne[i] = new Thread(new ThreadStart(MainThreadWorking));
                ThreadOne[i].IsBackground = true;
                ThreadOne[i].Start();
            }

            Thread th = new Thread(new ThreadStart(DisplayMessage));
            th.IsBackground = true;
            th.Start();
        }

        //工作（主线程）
        public void MainThreadWorking()
        {
            while (!IsExit)
            {
                try
                {
                    //获取公司列表
                    GetCompanyUrlList();

                    if (!IsCanDeQueue())
                        IsExit = true;

                    //保证HashTable线程安全
                    lock (CompanyField.SyncRoot)
                    {
                        //队列出队
                        CompanyWorking(CompanyUrlDequeue());
                    }

                    if (CompanyUrlQueue.Count < QueueMinCount && !IsExit)
                    {
                        IsBeginGather = true;
                    }
                }
                catch (Exception e)
                {
                    GatherTools.WriteErrorLog("MainThreadWorking()-" + e.Message);
                    FileAccessLog.SaveBugInFile(e.ToString());
                }
            }
            if (IsExit)
                Worker.AddMessage("程序已经停止...");
        }

        //获取公司地址列表
        protected void GetCompanyUrlList()
        {
            //首个线程获取公司列表
            if (IsBeginGather)
            {
                Worker.AddMessage("获取公司地址列表...");
                var companyList = Bll.BllAlibaba_CompanyGather.Query(o => o.Tag == GatherTools.ConvertToInt(ConfigurationManager.AppSettings["Tag"]));
                if (companyList == null || companyList.Count == 0)
                {
                    Worker.AddMessage("没有待抓取的公司...开始休眠5分钟！");
                    Thread.Sleep(1000 * 50);
                    IsBeginGather = true;
                    GetCompanyUrlList();
                }
                if (IsBeginGather)
                {
                    IsBeginGather = false;
                    foreach (var company in companyList)
                    {
                        try
                        {
                            CompanyUrlQueue.Enqueue(company);
                        }
                        catch (Exception e)
                        {
                            FileAccessLog.SaveBugInFile(e.ToString());
                        }
                    }

                    if (CompanyUrlQueue.Count == 0)
                        IsExit = true;
                    lbQueueCount.Text = string.Format("队列数量:{0}", CompanyUrlQueue.Count);
                }
            }
        }

        /// <summary>
        /// 公司相关信息抓取
        /// </summary>
        /// <param name="CurrentURL"></param>
        /// <returns></returns>
        private void CompanyWorking(Alibaba_CompanyGather model)
        {
            bool? isUnused = false;
            CurrentDequeueNum = model.ID;
            string CurrentURL = model.CompanyUrl;
            Worker.AddMessage("处理-" + CurrentURL);
            Worker.AddMessage("抓取公司基本信息...");
            isUnused = Worker.SaveCompanyProfile(CurrentURL, ref CompanyField);
            if (isUnused == null)
            {
                IsExit = true;
                Worker.AddMessage("无法访问，可能被封，程序已停机，请检查！！");
                return;
            }
            Worker.AddMessage("抓取工厂信息...");
            Worker.SaveProductCapacity(CurrentURL, ref CompanyField);
            Worker.AddMessage("抓取联系我们...");
            Worker.SaveContacts(CurrentURL, ref CompanyField);

            //保存公司信息                    
            int res = Worker.SaveCompanyField(CurrentURL, ref CompanyField);
            Worker.AddMessage("保存会员信息结果：" + res.ToString());
            if (res == (int)Enums.GatherResult.InsertSuccess || res == (int)Enums.GatherResult.UpdateSuccess)
            {
                SuccessCount++;
                lbSuccess.Text = string.Format("成功：{0}", SuccessCount);
                Worker.AddMessage(string.Format("{0} 成功！！", CurrentURL));
            }
            else if (res == (int)Enums.GatherResult.UpdateFailed)
            {
                FailCount++;
                lbFail.Text = string.Format("失败：{0}", FailCount);
                Worker.AddMessage(string.Format("{0} 更新失败!", CurrentURL));
            }
            else if (res == (int)Enums.GatherResult.Incomplete)
            {
                FailCount++;
                lbFail.Text = string.Format("失败：{0}", FailCount);
                Worker.AddMessage(string.Format("{0} 数据不完整，失败!", CurrentURL));
            }
            else
            {
                FailCount++;
                lbFail.Text = string.Format("失败：{0}", FailCount);
                Worker.AddMessage(string.Format("{0}有异常，失败！", CurrentURL));
            }
            Bll.BllAlibaba_CompanyGather.UpdateTag(CurrentDequeueNum, res); //更新Tag
            CompanyField.Clear();
        }
        #endregion

        #region 辅助方法

        public void DisplayMessage()
        {
            while (!IsExit)
            {
                while (Worker.MsgQueue.Count > 0)
                {
                    ShowLog(Worker.MsgQueue.Dequeue());
                }
                Thread.Sleep(1000);
            }
        }

        public void ShowLog(string msg)
        {
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
        }
        #endregion

        #region Ali帐号登录

        #region 属性声明
        AliClient _aliClient = null;
        public AliClient AliClient
        {
            get
            {
                return _aliClient;
            }
            set
            {
                _aliClient = value;
                if (_aliClient.AliLoginUser != null && _aliClient.AliLoginUser.person_data != null)
                {
                    UserName = _aliClient.AliLoginUser.person_data.login_id;
                }
            }
        }
        public string UserName
        {
            get
            {
                return (this.txtUserName.Text + "").Trim();
            }
            set
            {
                this.txtUserName.Text = value;
            }
        }
        public string Password
        {
            get
            {
                return (this.txtPassword.Text + "").Trim();
            }
            set
            {
                this.txtPassword.Text = value;
            }
        }
        private string CheckCode
        {
            get
            {
                return (this.txtCheckCode.Text + "").Trim();
            }
        }

        private int loginFailCount = 0;
        #endregion

        void InitAliClient()
        {
            //_aliClient = new AliClient { Cookie = new CookieContainer() };
            //AliPassporter.PrepareLogin(_aliClient);//无验证码登陆
        }

        /// <summary>
        /// 先把验证码加载出来，这里是偷懒
        /// 正确做法:
        /// 1.无验证码直接登录->成功
        /// 2.失败->刷新出验证码->登陆->成功
        /// </summary>
        void RefreshCheckCode()
        {
            Image img = AliPassporter.DoCheckCode(_aliClient);
            checkCodeImg.Image = img;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (_aliClient == null)
            {
                MessageBox.Show("登录模块未加载");
                return;
            }
            //登陆系统，异步方法防止页面假死
            var data = await AliPassporter.DoLoginAsync(_aliClient, UserName, Password, CheckCode);
            if (data)
            {
                //TODO
                Worker.GetContactHideInfo(AliClient);
                MessageBox.Show("登陆成功，此处可以进行其他操作了!");
            }
            else
            {
                //登陆失败3次，显示验证码
                loginFailCount++;
                if (loginFailCount >= 3)
                {
                    RefreshCheckCode();
                }
                MessageBox.Show("登陆失败!");
            }
        }

        private void checkCodeImg_Click(object sender, EventArgs e)
        {
            if (_aliClient == null)
            {
                MessageBox.Show("登录模块未加载");
                return;
            }
            RefreshCheckCode();
        }
        #endregion
    }
}
