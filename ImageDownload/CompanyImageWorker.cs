using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using WebGather.Model;
using WorkerSpace;

namespace ImageDownload
{
    public class CompanyImageWorker : BaseWorker<Alibaba_CompanyImages>
    {
        public ImageDownloader downLoader = new ImageDownloader();
        protected Mutex locker = new Mutex();
        public int companyId = 0;

        public override void InitData()
        {
            //判断磁盘容量
            long GB = downLoader.GetHardDiskFreeSpace(downLoader.SavePath.Substring(0, 1).ToString().ToUpper());
            if (GB <= 1)
            {
                DisplayMessage(string.Format("磁盘剩余容量只剩{0}GB，停止抓取！！", GB));
                IsExit = true;
            }

            DisplayMessage(string.Format("{0}-{1}-获取数据...", DateTime.Now.ToShortTimeString(), Thread.CurrentThread.Name));
            List<Alibaba_CompanyImages> imgList = null;
            if (companyId == 0)
            {
                imgList = Bll.BllAlibaba_CompanyImages.Query(o => o.IsDown == false, o => o.ID, "asc", o => new { o.ID, o.ImageUrl, o.IsDown, o.LocalImagePath, o.ImgTitle, o.Type, o.Cid, o.DownLoadTime }, this.ThreadCount * 1000);
            }
            else
            {
                imgList = Bll.BllAlibaba_CompanyImages.Query(o => o.IsDown == false && o.Cid == companyId, o => o.ID, "asc", o => new { o.ID, o.ImageUrl, o.IsDown, o.LocalImagePath, o.ImgTitle, o.Type, o.Cid, o.DownLoadTime });
            }
            if (imgList != null && imgList.Count > 0)
            {
                foreach (var item in imgList)
                    this.EnterData(item);
            }
            if (this.DataCount == 0)
            {
                DisplayMessage(string.Format("{0}-{1}-没有数据!", DateTime.Now.ToShortTimeString(), Thread.CurrentThread.Name));
                IsExit = true;
            }
        }

        public override void WordProcess()
        {
            Alibaba_CompanyImages model = GetData;
            if (model == null)
                return;
            DisplayMessage(string.Format("处理-{0}", model.ImageUrl));
            if (model.Cid > 0)
            {
                string localPath = downLoader.DownloadCompanyPic((byte)model.Type, model.ImageUrl);
                if (localPath != "" && localPath.Length > 10)
                {
                    model.LocalImagePath = localPath;
                    model.DownLoadTime = DateTime.Now;
                    model.IsDown = true;
                    locker.WaitOne();
                    int i = Bll.BllAlibaba_CompanyImages.Update(model);
                    locker.ReleaseMutex();
                    if (i > 0)
                    {
                        this.SuccessedCount++;
                        DisplayMessage("下载成功！！");
                    }
                    else
                    {
                        this.FailCount++;
                        DisplayMessage("更新失败！");
                    }
                }
                else
                {
                    this.FailCount++;
                    DisplayMessage("下载失败！");
                }
            }
            else
            {
                this.FailCount++;
                DisplayMessage("供应商不存在！");
            }
        }
    }
}
