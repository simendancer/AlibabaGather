using Common;
using Common.Enum;
using ImageDownload;
using NSoup;
using NSoup.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading;
using WebGather.Model;
using WorkerSpace;

namespace GetProductInfo
{
    public class GetProductInfoWorker : BaseWorker<Alibaba_ProGather>
    {
        public ImageDownloader downLoader = new ImageDownloader();
        protected Tools.Helper.HttpHelper httpHelper = new Tools.Helper.HttpHelper("UTF-8");
        protected readonly int Tag = ConfigurationManager.AppSettings["Tag"]?.ToString().ToInt32() ?? 0;
        public int CompanyId { get; set; }

        public override void InitData()
        {
            DisplayMessage(string.Format("{0}-{1}-获取数据...", DateTime.Now.ToShortTimeString(), Thread.CurrentThread.Name));
            IList<Alibaba_ProGather> proList = null;
            if (CompanyId > 0)
            {
                proList = Bll.BllAlibaba_ProductGather.GetListByCompanyId(CompanyId, Tag, this.ThreadCount * 50);
            }
            else
            {
                proList = Bll.BllAlibaba_ProductGather.GetList(Tag, this.ThreadCount * 50);
            }
            if (proList != null && proList.Count > 0)
            {
                foreach (var item in proList)
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
            Alibaba_ProGather CurrentData = GetData;
            if (CurrentData == null)
                return;

            //开始分析Html
            AnalyseHtml(CurrentData);
        }

        public void AnalyseHtml(Alibaba_ProGather gatherInfo)
        {
            DisplayMessage("分析:" + gatherInfo.ProductUrl);
            //发送请求
            string pageHtml = string.Empty;
            var httpResult = httpHelper.Get(gatherInfo.ProductUrl);

            //验证请求
            CheckHttpResult(httpResult, gatherInfo.ProductUrl, ref pageHtml);

            Hashtable ProductField = Hashtable.Synchronized(new Hashtable());

            //转换为Nsoup文档
            Document doc = NSoupClient.Parse(pageHtml);

            //匹配获取信息
            FetchProInfo(doc, (long)gatherInfo.AliGroupId, ref ProductField);
            FetchProAttrs(doc, ref ProductField);
            FetchProImages(doc, ref ProductField);

            //保存信息
            int res = SaveProductFields(gatherInfo, ProductField);

            ProductField.Clear();
            //更新标记
            Bll.BllAlibaba_ProductGather.UpdateTag(gatherInfo.ID, res);
        }

        //正则匹配文本中最后一个数字
        private long GetLastNumber(string s)
        {
            Match match = Regex.Match(s, "\\d+");
            var group = match.Groups;
            return group[group.Count - 1]?.Value.ToInt64() ?? 0;
        }

        //产品信息
        private void FetchProInfo(Document doc, long aliGroupId, ref Hashtable ProductField)
        {
            //产品标题
            string productName = doc.Select("h1.title span").First?.Text().Trim() ?? "";
            if (productName == "") return;
            ProductField.Add("ProductName", productName);

            //供应商分类名称
            var group = doc.Select(".ui-breadcrumb a").Last;
            if (group != null)
            {
                string url = group.Attr("href");
                if (aliGroupId > 0 && url.Contains(aliGroupId.ToString()))
                {
                    ProductField["AliGroupName"] = group.Text();
                }
            }

            //产品基本信息
            FetchProBasicInfo(doc, ref ProductField);

            //Packaging & Delivery
            FetchPackagingDelivery(doc, ref ProductField);
        }

        //产品基本信息
        private void FetchProBasicInfo(Document doc, ref Hashtable ProductField)
        {
            decimal maxPrice = 0;
            decimal minPrice = 0;
            var ths = doc.Select("table.btable th.J-brief-info-key");
            if (ths.Count > 0)
            {
                foreach (var th in ths)
                {
                    var key = th.Text().Trim();
                    string val = string.Empty;
                    //FOB Price 特殊结构处理
                    if (key == "FOB Price:")
                    {
                        var valSpan = th.NextElementSibling.Select(".J-brief-info-val span:not(.price)");
                        if (valSpan.Count > 0) minPrice = valSpan[0].Text().ToDecimal();
                        if (valSpan.Count > 1) maxPrice = valSpan[1].Text().ToDecimal();
                        continue;
                    }
                    else
                    {
                        val = th.NextElementSibling?.Text().Trim() ?? "";
                    }
                    if (key == "" || val == "") continue;
                    switch (key)
                    {
                        case "Min.Order Quantity:":
                            ProductField.Add("MoqStr", val);
                            ProductField.Add("Moq", val.Split(' ')[0]);
                            break;
                        case "Supply Ability:":
                            ProductField.Add("SupplyAbility", val);
                            break;
                        case "Port:":
                            ProductField.Add("Port", val);
                            break;
                        case "Payment Terms:":
                            ProductField.Add("PaymentTerms", val);
                            break;
                        default:
                            continue;
                    }
                }
            }
            ProductField.Add("MinPrice", minPrice);
            ProductField.Add("MaxPrice", maxPrice);

            //价格区间
            var priceItems = doc.Select("table.btable .pre-inquiry-quote-step-item");
            if (priceItems.Count > 0)
            {
                List<Tools.Json.PriceRange> priceRange = new List<Tools.Json.PriceRange>();
                foreach (var item in priceItems)
                {
                    int cnt = item.Select(".pre-inquiry-quantity-range").First?.Text().Replace(">=", "").Split('-')[0].Trim().ToInt32() ?? 0;
                    decimal price = item.Select(".pre-inquiry-price").First?.Text().ToDecimal() ?? 0;
                    if (cnt == 0 && price == 0) continue;
                    priceRange.Add(new Tools.Json.PriceRange()
                    {
                        price = price,
                        cnt = cnt
                    });
                }
                ProductField.Add("PriceRange", Tools.Tool.JsonHelper.Serialize(priceRange));
            }
        }

        //Packaging & Delivery
        private void FetchPackagingDelivery(Document doc, ref Hashtable ProductField)
        {
            var trs = doc.Select("div.detail-content table.table tr");
            if (trs.Count > 0)
            {
                foreach (var tr in trs)
                {
                    var key = tr.Select("th").First?.Text().Trim() ?? "";
                    string val = tr.Select("td").First?.Text().Trim() ?? "";
                    if (key == "" || val == "") continue;
                    switch (key)
                    {
                        case "Packaging Details:":
                            ProductField.Add("PackagingDetails", val);
                            break;
                        case "Delivery Detail:":
                            ProductField.Add("DeliveryDetail", val);
                            break;
                        default:
                            continue;
                    }
                }
            }
        }

        //产品属性
        private void FetchProAttrs(Document doc, ref Hashtable ProductField)
        {
            var lis = doc.Select("ul.attr-list li");
            if (lis != null && lis.Count > 0)
            {
                List<Tools.Json.AttrJson> attrList = new List<Tools.Json.AttrJson>();
                foreach (var li in lis)
                {
                    var name = li.Select("span.attr-name").First?.Attr("title") ?? "";
                    var value = li.Select("span.attr-value").First?.Attr("title") ?? "";
                    if (name != "" || value != "")
                    {
                        attrList.Add(new Tools.Json.AttrJson()
                        {
                            n = name,
                            v = value
                        });
                    }
                }
                ProductField.Add("AttrJson", Tools.Tool.JsonHelper.Serialize(attrList));
            }
        }

        //产品图片&产品详细
        private void FetchProImages(Document doc, ref Hashtable ProductField)
        {
            List<Alibaba_ProImages> imgList = new List<Alibaba_ProImages>();

            //产品图片
            var imgs = doc.Select("ul.inav img");
            if (imgs.Count > 0)
            {
                //有小图轮播图时
                foreach (var img in imgs)
                {
                    string src200 = img.Attr("src").Replace("_50x50.", "_200x200.");
                    string src = img.Attr("src").Replace("_50x50.", "|").Split('|')[0];
                    string alt = img.Attr("alt");
                    imgList.Add(new Alibaba_ProImages()
                    {
                        Pid = 0,
                        DownLoadTime = DateTime.Now,
                        DownType = (byte)Enums.ImgType.Img200,
                        Type = (byte)Enums.ProductImg.ProductImg,
                        ImageUrl = "https:" + src200,
                        LocalImagePath = "",
                        ProductName = alt,
                        IsDown = false
                    });
                    imgList.Add(new Alibaba_ProImages()
                    {
                        Pid = 0,
                        DownLoadTime = DateTime.Now,
                        DownType = (byte)Enums.ImgType.Img,
                        Type = (byte)Enums.ProductImg.ProductImg,
                        ImageUrl = "https:" + src,
                        LocalImagePath = "",
                        ProductName = alt,
                        IsDown = false
                    });
                }
            }
            else
            {
                //无小图轮播图时，直接取第一张350大图
                imgs = doc.Select("#J-image-icontent img");
                if (imgs.Count > 0)
                {
                    var img = imgs[0];
                    string src200 = img.Attr("src").Replace("_350x350.", "_200x200.");
                    string src = img.Attr("src").Replace("_350x350.", "|").Split('|')[0];
                    string alt = img.Attr("alt");
                    imgList.Add(new Alibaba_ProImages()
                    {
                        Pid = 0,
                        DownLoadTime = DateTime.Now,
                        DownType = (byte)Enums.ImgType.Img200,
                        Type = (byte)Enums.ProductImg.ProductImg,
                        ImageUrl = "https:" + src200,
                        LocalImagePath = "",
                        ProductName = alt,
                        IsDown = false
                    });
                    imgList.Add(new Alibaba_ProImages()
                    {
                        Pid = 0,
                        DownLoadTime = DateTime.Now,
                        DownType = (byte)Enums.ImgType.Img,
                        Type = (byte)Enums.ProductImg.ProductImg,
                        ImageUrl = "https:" + src,
                        LocalImagePath = "",
                        ProductName = alt,
                        IsDown = false
                    });
                }
            }

            //产品详细页内容图片处理
            DetailHandle(doc, ref ProductField, ref imgList);

            ProductField.Add("ImgList", imgList);
        }

        //产品详细内容处理
        private void DetailHandle(Document doc, ref Hashtable ProductField, ref List<Alibaba_ProImages> imgList)
        {
            var detail = doc.Select(".richtext-detail").First;
            if (detail != null)
            {
                var imgs = detail.Select("img[data-src]");
                if (imgs.Count > 0)
                {
                    foreach (var img in imgs)
                    {
                        var src = img.Attr("data-src");
                        var alt = img.Attr("data-alt");

                        imgList.Add(new Alibaba_ProImages()
                        {
                            Pid = 0,
                            DownLoadTime = DateTime.Now,
                            DownType = 0,
                            Type = (byte)Enums.ProductImg.DetailImg,
                            ImageUrl = "https:" + src,
                            LocalImagePath = "",
                            ProductName = alt,
                            IsDown = false
                        });

                        //替换src,alt属性内容
                        img.Attr("src", src).Attr("alt", alt);
                        //去除无用ali标签
                        img.RemoveAttr("data-src").RemoveAttr("data-alt").RemoveAttr("ori-height").RemoveAttr("ori-width").RemoveAttr("data-width").RemoveAttr("data-height");
                        //去除noscript标签
                        if (img.NextElementSibling.TagName().ToLower() == "noscript") img.NextElementSibling.Remove();
                    }
                }
                ProductField["Detail"] = detail.Html();
            }
        }

        //保存所有信息
        private int SaveProductFields(Alibaba_ProGather gatherInfo, Hashtable ProductField)
        {
            DisplayMessage("正在保存数据...");
            if (ProductField.Count < 3)
            {
                DisplayMessage("未抓取到完整信息...");
                return (int)Enums.GatherResult.Incomplete;
            }
            Alibaba_ProInfo proInfo = new Alibaba_ProInfo();
            try
            {
                proInfo.AliProductId = gatherInfo.AliProductId;
                proInfo.AliGroupId = gatherInfo.AliGroupId;
                proInfo.AliGroupName = GatherTools.ObjectConvertString(ProductField["AliGroupName"]);
                proInfo.FirstPic = string.Empty;
                proInfo.Pics = string.Empty;
                proInfo.ProNo = string.Empty;
                proInfo.CompanyId = gatherInfo.CompanyId;
                proInfo.Title = GatherTools.ObjectConvertString(ProductField["ProductName"]);
                proInfo.Detail = ProductField["Detail"]?.ToString() ?? "";
                proInfo.PriceRange = ProductField["PriceRange"]?.ToString() ?? "";
                proInfo.Attribute = ProductField["AttrJson"]?.ToString() ?? "";
                proInfo.Moq = GatherTools.ObjectConvertInt(ProductField["Moq"]);
                proInfo.MoqStr = GatherTools.ObjectConvertString(ProductField["MoqStr"]);
                proInfo.MaxPrice = GatherTools.ObjectConvertDecimal(ProductField["MaxPrice"]);
                proInfo.MinPrice = GatherTools.ObjectConvertDecimal(ProductField["MinPrice"]);
                proInfo.SupplyAbility = GatherTools.ObjectConvertString(ProductField["SupplyAbility"]);
                proInfo.Port = GatherTools.ObjectConvertString(ProductField["Port"]);
                proInfo.PaymentTerms = GatherTools.ObjectConvertString(ProductField["PaymentTerms"]);
                proInfo.PackagingDetails = GatherTools.ObjectConvertString(ProductField["PackagingDetails"]);
                proInfo.DeliveryDetail = GatherTools.ObjectConvertString(ProductField["DeliveryDetail"]);
                proInfo.AddTime = DateTime.Now;
                proInfo.Tag = (int)Enums.CheckStatus.Wait;
                int res = Bll.BllAlibaba_ProInfo.IsExists((long)gatherInfo.AliProductId);
                if (res > 0)
                {
                    //更新记录
                    DisplayMessage(gatherInfo.ProductUrl + "已经存在，开始更新");
                    res = Bll.BllAlibaba_ProInfo.Update(proInfo, o => o.Id == res);
                    if (res > 0)
                    {
                        //如果是图片未下载的产品，则重新下载
                        if (gatherInfo.Tag == (int)Enums.GatherResult.InsertFailed || gatherInfo.Tag == (int)Enums.GatherResult.ImgFailed)
                        {
                            List<Alibaba_ProImages> imglist = ProductField["ImgList"] as List<Alibaba_ProImages>;
                            if (imglist != null && imglist.Count > 0)
                            {
                                DisplayMessage("开始下载图片...");
                                //下载图片
                                int count = DownLoadImg(imglist, res, proInfo.Title);
                                if (count > 0)
                                {
                                    DisplayMessage("图片保存成功!!");
                                }
                                else
                                {
                                    DisplayMessage("图片保存失败!!");
                                    DisplayMessage("更新成功!!");
                                    Bll.BllAlibaba_ProInfo.Update(new Alibaba_ProInfo()
                                    {
                                        Tag = (int)Enums.CheckStatus.UnDownLoad
                                    }, o => o.Id == res);
                                    return (int)Enums.GatherResult.ImgFailed;
                                }
                            }
                        }
                        DisplayMessage("更新成功!!");
                        return (int)Enums.GatherResult.UpdateSuccess;
                    }
                    else
                    {
                        DisplayMessage("更新失败!!");
                        return (int)Enums.GatherResult.UpdateFailed;
                    }
                }
                else
                {
                    //插入记录
                    res = Bll.BllAlibaba_ProInfo.Insert(proInfo);
                    if (res > 0)
                    {
                        List<Alibaba_ProImages> imglist = ProductField["ImgList"] as List<Alibaba_ProImages>;
                        if (imglist != null && imglist.Count > 0)
                        {
                            DisplayMessage("开始下载图片...");
                            //下载图片
                            int count = DownLoadImg(imglist, res, proInfo.Title);
                            if (count > 0)
                            {
                                DisplayMessage("图片保存成功!!");
                            }
                            else
                            {
                                DisplayMessage("图片保存失败!!");
                                DisplayMessage("添加成功!!");
                                Bll.BllAlibaba_ProInfo.Update(new Alibaba_ProInfo()
                                {
                                    Tag = (int)Enums.CheckStatus.UnDownLoad
                                }, o => o.Id == res);
                                this.SuccessedCount++;
                                return (int)Enums.GatherResult.ImgFailed;
                            }
                        }
                        this.SuccessedCount++;
                        DisplayMessage("添加成功!!");
                        return (int)Enums.GatherResult.InsertSuccess;
                    }
                    else
                    {
                        DisplayMessage("添加失败!!");
                        return (int)Enums.GatherResult.InsertFailed;
                    }
                }
            }
            catch (Exception e)
            {
                GatherTools.WriteErrorLog("GetProductInfoWorker.SaveProductFields()-后" + e.Message);
                DisplayMessage(e.ToString());
                return (int)Enums.GatherResult.Error;
            }
        }

        private int DownLoadImg(List<Alibaba_ProImages> imglist, int pid, string title)
        {
            foreach (var item in imglist)
            {
                item.Pid = pid;
                item.ProductName = title;
                string localPath = downLoader.DownloadProductPic((byte)item.Type, item.ImageUrl, item.ProductName);
                if (localPath != "" && localPath.Length > 10)
                {
                    item.LocalImagePath = localPath;
                    item.DownLoadTime = DateTime.Now;
                    item.IsDown = true;
                    //如果当前是首图，则更新首图字段
                    if (item.Type == (int)Enums.ProductImg.ProductImg && item.DownType == (int)Enums.ImgType.Img200)
                    {
                        var proModel = Bll.BllAlibaba_ProInfo.FirstSelect(o => o.Id == (int)item.Pid, o => new { o.Id, o.FirstPic });
                        if (proModel != null && string.IsNullOrEmpty(proModel.FirstPic))
                        {
                            proModel.FirstPic = localPath;
                            Bll.BllAlibaba_ProInfo.Update(proModel, o => o.Id == proModel.Id);
                        }
                    }
                }
            }
            return Bll.BllAlibaba_ProductImages.Insert(imglist);
        }
    }
}
