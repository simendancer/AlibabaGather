using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Dos.DataAccess.Base;
using WebGather.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestOrmPageSpeed()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int page = 10;
            int pagesize = 20;

            var p = Db.Context.From<Alibaba_ProGather>()
                .Where(o => o.ID > 10)
                .OrderByDescending(o => o.ID);
            int records = p.Count();
            var list = p.Page(pagesize, page).ToList();

            watch.Stop();
            long time = watch.ElapsedMilliseconds;
        }

        [TestMethod]
        public void TestStringSimilarity()
        {
            string str1 = "111111asdad5431111111";
            string str2 = "1111111111111asdasda1232132";

            Tools.Tool.StringSimilarityTools tool = new Tools.Tool.StringSimilarityTools(str1, str2);
            tool.Compute();
            var result = tool.ComputeResult;
        }

        [TestMethod]
        public void TestGetList()
        {
            List<Alibaba_ProGather> prolist = new List<Alibaba_ProGather>();
            var list = Bll.BllAlibaba_ProductGather.GetAll();
            prolist.AddRange(list);
            int count = prolist.Count;
        }

        /// <summary>
        /// 清除产品图片下载记录
        /// </summary>
        [TestMethod]
        public void ReturnProDetailImg()
        {
            //获取已下载图片的最大产品Id
            //long maxPid = Bll.BllAlibaba_ProductImages.FirstSelect(o => o.IsDown == true, o => o.Pid, o => o.Pid, "desc")?.Pid ?? 0;
            //if (maxPid > 0)
            //{
            //获取已下载详情页内容图片的的产品
            var list = Bll.BllAlibaba_ProInfo.Query(o => o.Id < 41, o => o.Id, "asc", o => new { o.Id, o.Detail });
            foreach (var item in list)
            {
                var imglist = GetImageUrlListFromHtml(item.Detail).ToList();
                var imgs = Bll.BllAlibaba_ProductImages.GetDetailImgListByProId(item.Id).ToList();
                for (int i = 0; i < imglist.Count; i++)
                {
                    item.FirstPic = "";
                    item.Pics = "";
                    item.Detail = item.Detail.Replace(imglist[i], imgs[i].ImageUrl.Replace("https:", "").Replace("http:", ""));
                }
            }
            Bll.BllAlibaba_ProInfo.Update(list);
            //}
        }

        public static string[] GetImageUrlListFromHtml(string htmlText)
        {
            // 定义正则表达式用来匹配 img 标签 
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串 
            MatchCollection matches = regImg.Matches(htmlText);
            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表 
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["imgUrl"].Value;
            return sUrlList;
        }
    }
}
