using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tools.Tool;

namespace WorkerSpace
{
    public abstract class BaseWorker<T>
    {
        #region 基本属性
        private Thread _singleThread = null;
        private Thread[] _multiThreads = null;
        private int _maxThreadCount = 0;
        private int _successedCount = 0;
        private int _failCount = 0;
        private bool _isExit = false;
        protected Mutex _locker = new Mutex();
        private int _sleepTime = 200;
        private int _threadCount = 0;
        /// <summary>
        /// 定义泛型数据队列
        /// </summary>        
        private ConcurrentQueue<T> _dataQueue = new ConcurrentQueue<T>();

        /// <summary>
        /// 最大线程数
        /// </summary>
        public int MaxThreadCount
        {
            get { return this._maxThreadCount; }
            set { this._maxThreadCount = value; }
        }

        /// <summary>
        /// 成功数量
        /// </summary>
        public int SuccessedCount
        {
            get { return this._successedCount; }
            set { this._successedCount = value; }
        }

        /// <summary>
        /// 失败次数
        /// </summary>
        public int FailCount
        {
            get { return this._failCount; }
            set { this._failCount = value; }
        }

        /// <summary>
        /// 退出标记
        /// </summary>
        public bool IsExit
        {
            get { return this._isExit; }
            set { this._isExit = value; }
        }

        /// <summary>
        /// 间隔时间(毫秒)
        /// </summary>
        public int SleepTime
        {
            get { return this._sleepTime; }
            set { this._sleepTime = value; }
        }

        /// <summary>
        /// 数据个数
        /// </summary>
        public int DataCount
        {
            get { return this._dataQueue.Count; }
        }

        /// <summary>
        /// 直接打印消息
        /// </summary>
        public Action<string> DisplayMessage;

        /// <summary>
        /// 当前线程数
        /// </summary>
        public int ThreadCount
        {
            get { return this._threadCount; }
            set { this._threadCount = value; }
        }

        #endregion

        /// <summary>
        /// 业务处理方法接口
        /// </summary>
        public abstract void WordProcess();
        /// <summary>
        /// 初始化数据
        /// </summary>
        public abstract void InitData();

        /// <summary>
        /// 单线程启动
        /// </summary>
        public virtual void StartWork()
        {
            this._threadCount = 1;
            this._singleThread = new Thread(new ThreadStart(InitWork));
            this._singleThread.Name = "#No.1#";
            this._singleThread.IsBackground = true;
            this._singleThread.Start();
        }

        /// <summary>
        /// 多线程启动
        /// </summary>
        /// <param name="count"></param>
        public virtual void StartWork(int count)
        {
            count = count < 1 ? 1 : count;
            _multiThreads = new Thread[count];
            this._threadCount = count;
            for (int i = 0; i < count; i++)
            {
                this._multiThreads[i] = new Thread(new ThreadStart(InitWork));
                this._multiThreads[i].IsBackground = true;
                this._multiThreads[i].Name = string.Format("#No.{0}#", (i + 1));
                this._multiThreads[i].Start();
            }
        }

        /// <summary>
        /// 线程初始化
        /// </summary>
        public void InitWork()
        {
            while (!this.IsExit)
            {
                try
                {
                    WordProcess();

                    Thread.Sleep(SleepTime);
                }
                catch (Exception ex)
                {
                    Log.WritePur(string.Format("{0}", ex.ToString()));
                }
            }
            DisplayMessage("程序终止!");
        }

        /// <summary>
        /// 取出一条数据
        /// </summary>
        public T GetData
        {
            get
            {
                this._locker.WaitOne();
                T temp = default(T);
                if (this._dataQueue.Count == 0)
                    InitData();
                if (this._dataQueue.Count > 0)
                    this._dataQueue.TryDequeue(out temp);
                this._locker.ReleaseMutex();
                return temp;
            }
        }
        /// <summary>
        ///加入数据队列
        /// </summary>
        /// <param name="data"></param>
        public void EnterData(T data)
        {
            this._dataQueue.Enqueue(data);
        }

        /// <summary>
        /// 暂停进程
        /// </summary>
        public void PauseThread()
        {
            if (this._threadCount > 0)
            {
                if (_singleThread != null && _singleThread.IsAlive)
                {
                    _singleThread.Suspend();
                    DisplayMessage("线程已挂起...");
                }
                if (_multiThreads != null && _multiThreads.Count() > 0)
                {
                    foreach (var thread in _multiThreads)
                    {
                        if (thread.IsAlive)
                        {
                            thread.Suspend();
                        }
                    }
                    DisplayMessage("线程已挂起...");
                }
            }
        }

        /// <summary>
        /// 继续进程
        /// </summary>
        public void ContinueThread()
        {
            if (this._threadCount > 0)
            {
                if (_singleThread != null && _singleThread.IsAlive && _singleThread.ThreadState == (ThreadState.Background | ThreadState.Suspended))
                {
                    _singleThread.Resume();
                    return;
                }
                if (_multiThreads != null && _multiThreads.Count() > 0)
                {
                    foreach (var thread in _multiThreads)
                    {
                        if (thread.IsAlive && thread.ThreadState == (ThreadState.Background | ThreadState.Suspended))
                        {
                            thread.Resume();
                        }
                    }
                }
            }
        }

        public virtual void CheckHttpResult(Tools.Helper.HttpHelper.HttpResult httpResult, string url, ref string pageHtml)
        {
            if (httpResult.StatusCode == 301 || httpResult.StatusCode == 302)
            {
                DisplayMessage(string.Format("页面重定向，状态码{0}，跳过...", httpResult.StatusCode));
                return;
            }
            if (httpResult.StatusCode != 200)
            {
                DisplayMessage(string.Format("请求失败，状态码{0}，跳过...", httpResult.StatusCode));
                LogHelper.WriteLog("Ali蜘蛛请求失败", httpResult.ExceptionMsg, url);
                return;
            }
            //获取页面HTML
            pageHtml = httpResult.ResultHtml;
            if (string.IsNullOrEmpty(pageHtml))
            {
                DisplayMessage("页面为空，跳过...");
                return;
            }
            if (pageHtml.Contains("<title>509 unused</title>") || pageHtml.Contains("<h1>unused</h1>") || pageHtml.Contains("The server encountered an internal error or misconfiguration and was unable to complete your request."))
            {
                DisplayMessage(string.Format("请求被封禁,线程{0}睡眠5分钟...", Thread.CurrentThread.Name));
                Thread.Sleep(1000 * 60 * 5); //睡眠5分钟
                return;
            }
        }
    }
}
