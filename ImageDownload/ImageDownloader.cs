using Common;
using Common.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Tools.Tool;

namespace ImageDownload
{
    public class ImageDownloader
    {
        public string SavePath { get; set; }
        private string imgUrl = ConfigurationManager.AppSettings["ImgUrl"]?.ToString() ?? "";

        //下载公司图片
        public string DownloadCompanyPic(byte imgType, string imgPath)
        {
            string folderName = string.Empty;
            string fileName = GatherTools.GetFileName(imgPath);
            switch (imgType)
            {
                case (byte)Enums.CompanyImg.CompanyImg:
                    folderName = "companyImg";
                    break;
                case (byte)Enums.CompanyImg.CompanyLogo:
                    folderName = "companylogo";
                    break;
                case (byte)Enums.CompanyImg.ContactLogo:
                    folderName = "contactlogo";
                    break;
                case (byte)Enums.CompanyImg.ProductionFlowImg:
                    folderName = "productionflow";
                    break;
                default:
                    return "";
            }
            return Tools.Usual.Utils.DownLoadPicture(imgPath, fileName, SavePath, folderName);
        }

        //下载产品图片
        public string DownloadProductPic(byte imgType, string imgPath, string imgTitle)
        {
            string folderName = string.Empty;
            string fileName = string.Empty;
            switch (imgType)
            {
                case (byte)Enums.ProductImg.ProductImg:
                    folderName = "product";
                    fileName = GetFileNameByTitle(imgPath, imgTitle);
                    break;
                case (byte)Enums.ProductImg.DetailImg:
                    folderName = "prodetail";
                    fileName = GatherTools.GetFileName(imgPath);
                    break;
                default:
                    return "";
            }
            return DownLoadPic(imgPath, fileName, SavePath, folderName);
        }

        #region 辅助方法

        //根据标题获取【文件名】
        public string GetFileNameByTitle(string imgPath, string title)
        {
            return Tools.Usual.Utils.ConverUrl(title) + "_" + Tools.Usual.Utils.GetDataShortRandom() + new Random().Next(10, 100) + "." + Tools.Usual.Utils.GetFileExt(imgPath);
        }

        /// <summary>
        /// 下载产品图片
        /// 按复数分钟区分文件夹
        /// </summary>
        /// <param name="url"></param>
        /// <param name="picName"></param>
        /// <param name="savePath"></param>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public string DownLoadPic(string url, string picName, string savePath, string folderName)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                    return string.Empty;
                string dirPath = string.Empty;

                //每5分钟数一个文件夹
                int minute = DateTime.Now.Minute % 5 == 0 ? DateTime.Now.Minute : (int)Math.Ceiling((decimal)(DateTime.Now.Minute / 5)) * 5;
                dirPath = string.Format("\\{0}\\{1}\\{2}\\{3}\\{4}\\{5}\\", folderName, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, minute);
                if (savePath.EndsWith("\\") || savePath.EndsWith("/"))
                {
                    savePath = savePath.Substring(0, savePath.Length - 1);
                }
                string sp = string.Format(@"{1}{0}", dirPath, savePath);
                if (!Directory.Exists(sp))
                    Directory.CreateDirectory(sp);

                string path = string.Format("{0}{1}", dirPath, picName);
                bool length = Tools.Usual.Utils.DownLoadFileAsync(url, sp + "\\" + picName, "https://www.alibaba.com");
                if (length)
                    return path;
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        ///  <summary> 
        /// 获取指定驱动器的空间总大小(单位为GB) 
        ///  </summary> 
        ///  <param name="str_HardDiskName">只需输入代表驱动器的字母即可 </param> 
        ///  <returns> </returns> 
        public long GetHardDiskSpace(string str_HardDiskName)
        {
            long totalSize = new long();
            str_HardDiskName = str_HardDiskName + ":\\";
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.Name == str_HardDiskName)
                {
                    totalSize = drive.TotalSize / (1024 * 1024 * 1024);
                    break;
                }
            }
            return totalSize;
        }

        ///  <summary> 
        /// 获取指定驱动器的剩余空间总大小(单位为GB) 
        ///  </summary> 
        ///  <param name="str_HardDiskName">只需输入代表驱动器的字母即可 </param> 
        ///  <returns> </returns> 
        public long GetHardDiskFreeSpace(string str_HardDiskName)
        {
            long freeSpace = new long();
            str_HardDiskName = str_HardDiskName + ":\\";
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.Name == str_HardDiskName)
                {
                    freeSpace = drive.TotalFreeSpace / (1024 * 1024 * 1024);
                    break;
                }
            }
            return freeSpace;
        }
        #endregion
    }
}
