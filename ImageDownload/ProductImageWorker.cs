using Common.Enum;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using Tools.Tool;
using WebGather.Model;
using WorkerSpace;

namespace ImageDownload
{
    public class ProductImageWorker : BaseWorker<Alibaba_ProImages>
    {
        public ImageDownloader downLoader = new ImageDownloader();
        protected Mutex locker = new Mutex();
        public int proId = 0;

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
            List<Alibaba_ProImages> imgList = null;
            if (proId == 0)
            {
                imgList = Bll.BllAlibaba_ProductImages.Query(o => o.IsDown == false, o => o.ID, "asc", o => new { o.ID, o.ImageUrl, o.IsDown, o.LocalImagePath, o.ProductName, o.Type, o.Pid, o.DownType }, this.ThreadCount * 1000);
            }
            else
            {
                imgList = Bll.BllAlibaba_ProductImages.Query(o => o.IsDown == false && o.Pid == proId, o => o.ID, "asc", o => new { o.ID, o.ImageUrl, o.IsDown, o.LocalImagePath, o.ProductName, o.Type, o.Pid, o.DownLoadTime });
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
            Alibaba_ProImages model = GetData;
            if (model == null)
                return;
            DisplayMessage(string.Format("处理-{0}", model.ImageUrl));
            if (model.Pid > 0)
            {
                string localPath = downLoader.DownloadProductPic((byte)model.Type, model.ImageUrl, model.ProductName);
                if (localPath != "" && localPath.Length > 10)
                {
                    model.LocalImagePath = localPath;
                    model.DownLoadTime = DateTime.Now;
                    model.IsDown = true;
                    locker.WaitOne();
                    int i = Bll.BllAlibaba_ProductImages.Update(model);
                    if (i > 0)
                    {
                        //替换详情页图片路径
                        //if (model.Type == (int)Enums.ProductImg.DetailImg)
                        //{
                        //    var proModel = Bll.BllAlibaba_ProInfo.FirstSelect(o => o.Id == (int)model.Pid, o => new { o.Id, o.Detail });
                        //    if (proModel != null)
                        //    {
                        //        proModel.Detail = proModel.Detail.Replace(model.ImageUrl.Replace("https:", "").Replace("http:", ""), localPath);
                        //        i = Bll.BllAlibaba_ProInfo.Update(proModel);
                        //        if (i == 0)
                        //        {
                        //            Log.WritePur(string.Format("产品详情页图片更新失败，产品Id：{0}", model.Pid));
                        //        }
                        //    }
                        //}

                        //更新封面图片
                        if (model.Type == (int)Enums.ProductImg.ProductImg && model.DownType == (int)Enums.ImgType.Img200)
                        {
                            var proModel = Bll.BllAlibaba_ProInfo.FirstSelect(o => o.Id == (int)model.Pid, o => new { o.Id, o.FirstPic });
                            if (proModel != null && string.IsNullOrEmpty(proModel.FirstPic))
                            {
                                proModel.FirstPic = localPath;
                                i = Bll.BllAlibaba_ProInfo.Update(proModel, o => o.Id == proModel.Id);
                                if (i == 0)
                                {
                                    Log.WritePur(string.Format("产品封面图更新失败，产品Id：{0}", model.Pid));
                                }
                            }
                        }
                        this.SuccessedCount++;
                        DisplayMessage("下载成功，更新图片信息成功！！");
                    }
                    else
                    {
                        this.FailCount++;
                        Log.WritePur(string.Format("图片信息更新失败，图片Id：{0}", model.ID));
                        DisplayMessage(string.Format("图片信息更新失败！！图片Id：{0}", model.ID));
                    }
                    locker.ReleaseMutex();
                }
                else
                {
                    this.FailCount++;
                    Log.WritePur(string.Format("图片下载失败，图片Id：{0}", model.ID));
                    DisplayMessage(string.Format("图片下载失败！！图片Id：{0}", model.ID));
                }
            }
            else
            {
                this.FailCount++;
                DisplayMessage("产品不存在！！跳过...");
            }
        }
    }
}
