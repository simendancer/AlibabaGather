using Bll;
using NSoup;
using NSoup.Nodes;
using NSoup.Select;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using Tools.Helper;
using Tools.Tool;
using WebGather.Model;
using WorkerSpace;
using static Tools.Helper.HttpHelper;

namespace GetProductList
{
    public class GetProductListWorker : BaseWorker<Alibaba_CompanyInfo>
    {
        private int sleepTime = 200;

        public int ProSuccessedCount { get; set; }

        public override void InitData()
        {
            this.DisplayMessage(string.Format("{0}-{1}-获取数据...", DateTime.Now.ToShortTimeString(), Thread.CurrentThread.Name));
            List<Alibaba_CompanyInfo> companyList = BllAlibaba_CompanyInfo.Query(o => o.PageCount >= o.CurrentPage, o => o.id, "asc", o => new { o.id, o.Tag, o.CompanyUrl, o.PageCount, o.CurrentPage }, base.ThreadCount * 10, null, null);
            if (companyList != null && companyList.Count > 0)
            {
                foreach (Alibaba_CompanyInfo item in companyList)
                {
                    base.EnterData(item);
                }
            }
            if (base.DataCount == 0)
            {
                this.DisplayMessage(string.Format("{0}-{1}-没有数据!", DateTime.Now.ToShortTimeString(), Thread.CurrentThread.Name));
                base.IsExit = true;
            }
        }

        public override void WordProcess()
        {
            Alibaba_CompanyInfo CurrentData = base.GetData;
            if (CurrentData != null)
            {
                this.AnalyseHtml(CurrentData);
            }
        }

        public void AnalyseHtml(Alibaba_CompanyInfo companyInfo)
        {
            int MaxPageCount = 1;
            this.DisplayMessage(string.Format("当前供应商：{0}", companyInfo.CompanyUrl));
            int CurrentPage = (int)companyInfo.CurrentPage;
            CurrentPage = CurrentPage > 0 ? CurrentPage : 1;
            MaxPageCount = 1;
            do
            {
                string url = this.GetPageUrl(companyInfo.CompanyUrl, CurrentPage);
                this.SaveProductURL(companyInfo, url, CurrentPage, ref MaxPageCount);
                Thread.Sleep(this.sleepTime);
            }
            while (CurrentPage++ < MaxPageCount && !base.IsExit);
            BllAlibaba_CompanyInfo.Update(new Alibaba_CompanyInfo { id = companyInfo.id, CurrentPage = CurrentPage });
            int successedCount = base.SuccessedCount;
            base.SuccessedCount = successedCount + 1;
        }

        protected void SaveProductURL(Alibaba_CompanyInfo companyModel, string url, int CurrentPage, ref int MaxPageCount)
        {
            this.DisplayMessage("分析:" + url);
            string pageHtml = string.Empty;
            HttpResult httpResult = new HttpHelper("UTF-8").Get(url);
            this.CheckHttpResult(httpResult, url, ref pageHtml);
            Document doc = NSoupClient.Parse(pageHtml);
            if (MaxPageCount <= 1)
            {
                MaxPageCount = this.GetMaxPageCount(doc);
                BllAlibaba_CompanyInfo.Update(new Alibaba_CompanyInfo { PageCount = MaxPageCount }, o => o.id == companyModel.id);
            }
            var productList = this.GetSupplierList(doc);
            if (productList == null && productList.Count == 0)
            {
                this.DisplayMessage("页面无数据！");
                base.IsExit = true;
            }

            Alibaba_ProGather model = null;
            foreach (var item in productList)
            {
                try
                {
                     model = new Alibaba_ProGather();
                    string proUrl = item.Select(".product-title a").Attr("href").Split('?')[0];
                    string[] ids = Regex.Match(proUrl, "(\\d+)-(\\d+)").Value.Split('-');
                    model.AliProductId = ids[0].ToInt64();
                    model.AliGroupId = ids.Length > 1 ? ids[1].ToInt64() : 0;
                    if (!BllAlibaba_ProductGather.IsExists((long)model.AliProductId))
                    {
                        model.ProductUrl = companyModel.CompanyUrl + proUrl;
                        model.CompanyId = companyModel.id;
                        model.Grade = 1290;
                        model.InsertTime = DateTime.Now;
                        model.Tag = 0;
                        model.MogoutId = 0;
                        if (BllAlibaba_ProductGather.Insert(model) > 0)
                        {
                            int proSuccessedCount = this.ProSuccessedCount;
                            this.ProSuccessedCount = proSuccessedCount + 1;
                        }
                    }
                }
                catch (Exception exp)
                {
                    Log.WritePur(exp.ToString());
                }
            }
            this.DisplayMessage(string.Format("第{1}页匹配结果：{0}", productList.Count, CurrentPage));
        }

        private string GetPageUrl(string companyUrl, int pageIndex)
        {
            return string.Format(companyUrl + "/productlist-{0}.html", pageIndex);
        }

        private int GetMaxPageCount(Document doc)
        {
            int a = 1;
            Element maxLabel = doc.Select("div.category-wrap label.ui-label").First;
            int result = 1;
            if (maxLabel != null && maxLabel.HasText)
            {
                string text = maxLabel.Text();
                string[] arr = text.Replace("of", "-").Replace("Page", "").Split('-');
                if (arr.Length == 2 && int.TryParse(arr[1].Trim(), out result) && result > 1)
                {
                    return result;
                }
            }
            return result;
        }

        private Elements GetSupplierList(Document doc)
        {
            return doc.Select("div.app-productsGalleryView .list-item");
        }
    }
}
