using System;
using System.Collections.Generic;
using NSoup;
using NSoup.Nodes;
using System.Collections;
using System.Configuration;
using WebGather.Model;
using Common;
using AccountLogin.Ali;
using Common.Enum;

namespace GetSupplier
{
    public class GetSupplierInfo
    {
        Tools.Helper.HttpHelper httpHelper = new Tools.Helper.HttpHelper("UTF-8");
        public Queue<string> MsgQueue = new Queue<string>();//保存信息的队列
        protected object obj = new object();
        public string SavePath { get; set; }
        private string imgUrl = ConfigurationManager.AppSettings["ImgUrl"]?.ToString() ?? "";

        #region 抓取CompanyProfile
        public bool? SaveCompanyProfile(string companyUrl, ref Hashtable hashTable)
        {
            try
            {
                string html = httpHelper.Get(string.Format("{0}/company_profile.html", companyUrl)).ResultHtml;
                if (string.IsNullOrEmpty(html))
                    return false;
                if (html.Contains("<title>509 unused</title>") || html.Contains("<h1>unused</h1>") || html.Contains("The server encountered an internal error or misconfiguration and was unable to complete your request."))
                    return null;
                Document doc = NSoupClient.Parse(html);
                return FetchCompanyProfile(doc, ref hashTable);
            }
            catch (Exception e)
            {
                GatherTools.WriteErrorLog("GetSupplierInfo.SaveCompanyProfile()-" + e.Message);
                FileAccessLog.SaveBugInFile(e.ToString());
                return false;
            }
        }

        private bool FetchCompanyProfile(Document doc, ref Hashtable CompanyField)
        {
            var trs = doc.Select("table.content-table tr");
            //基本信息
            foreach (var tr in trs)
            {
                string roleName = tr.Attr("data-role");
                var roleVal = tr.Select("td.col-value").First;
                if (!string.IsNullOrEmpty(roleName) && roleVal != null)
                {
                    //特殊字段
                    if (roleName == "companyMainMarket")
                    {
                        string s = string.Empty;
                        var topMarkets = roleVal.Select("a.market-item");
                        foreach (var item in topMarkets)
                        {
                            var spans = item.Select("span");
                            if (spans.Count == 2)
                            {
                                s += spans[0].Text() + " " + spans[1].Text() + ",";
                            }
                        }
                        CompanyField.Add("companyMainMarket", s.TrimEnd(','));
                        continue;
                    }
                    CompanyField.Add(roleName, roleVal.Text().Trim());
                }
            }
            //保存公司Logo
            GetCompanyLogo(doc, CompanyField);
            //保存认证信息
            GetCompanyVerifyInfo(doc, CompanyField);
            //统一图片列表
            List<Alibaba_CompanyImages> imgList = new List<Alibaba_CompanyImages>();
            //保存公司图片
            GetCompanyImgs(doc, imgList);
            //保存生成流程图片
            GetProductFlowImgs(doc, imgList);

            CompanyField.Add("ImgList", imgList);
            return true;
        }

        private void GetCompanyLogo(Document doc, Hashtable CompanyField)
        {
            var companyLogo = doc.Select("td.company-logo img");
            if (companyLogo.Count > 0)
            {
                CompanyField.Add("companyLogo", "https:" + companyLogo.Attr("src"));
            }
        }

        private void GetCompanyVerifyInfo(Document doc, Hashtable CompanyField)
        {
            var verify = doc.Select("div.top-verify-info");
            if (verify.Count > 0)
            {
                CompanyField.Add("isverify", true);
                CompanyField.Add("verifyInfo", verify.First.Text());
                CompanyField.Add("verifyGroup", verify.Select("span.verification-title")?.First.Text() ?? "");
                CompanyField.Add("verifyLogo", "https:" + verify.Select("img.verify-logo").Attr("src"));
            }
        }

        private void GetCompanyImgs(Document doc, List<Alibaba_CompanyImages> imgList)
        {
            var companyImgs = doc.Select("div.company-image:not(.video-image) img");
            if (companyImgs.Count > 0)
            {
                foreach (var img in companyImgs)
                {
                    string url = "http:" + img.Attr("src").ToString();
                    //string localUrl = Tools.Usual.Utils.DownLoadPicture(url, GatherTools.GetFileName(url), SavePath, "company");
                    imgList.Add(new Alibaba_CompanyImages()
                    {
                        Cid = 0,
                        DownLoadTime = DateTime.Now,
                        DownType = 0,
                        Type = (byte)Enums.CompanyImg.CompanyImg,
                        ImageUrl = url,
                        LocalImagePath = "",//localUrl,
                        ImgTitle = "CompanyImage",
                        IsDown = false //!string.IsNullOrEmpty(localUrl)
                    });
                }
            }
        }

        private void GetProductFlowImgs(Document doc, List<Alibaba_CompanyImages> imgList)
        {
            var productFlowImgs = doc.Select("div.ui-slidebox-item img");
            if (productFlowImgs.Count > 0)
            {
                foreach (var img in productFlowImgs)
                {
                    string alt = img.Attr("alt").ToString();
                    string url = "https:" + img.Attr("src").ToString();
                    //string localUrl = Tools.Usual.Utils.DownLoadPicture(url, GatherTools.GetFileName(url), SavePath, "productflow");
                    imgList.Add(new Alibaba_CompanyImages()
                    {
                        Cid = 0,
                        DownLoadTime = DateTime.Now,
                        DownType = 0,
                        Type = (byte)Enums.CompanyImg.ProductionFlowImg,
                        ImageUrl = url,
                        LocalImagePath = "",//localUrl,
                        ImgTitle = alt,
                        IsDown = false //!string.IsNullOrEmpty(localUrl)
                    });
                }
            }
        }
        #endregion

        #region 抓取ProductionCapacity
        public bool? SaveProductCapacity(string companyUrl, ref Hashtable hashTable)
        {
            try
            {
                string html = httpHelper.Get(string.Format("{0}/company_profile/production_capacity.html", companyUrl)).ResultHtml;
                if (string.IsNullOrEmpty(html))
                    return false;
                if (html.Contains("<title>509 unused</title>") || html.Contains("<h1>unused</h1>") || html.Contains("The server encountered an internal error or misconfiguration and was unable to complete your request."))
                    return null;
                Document doc = NSoupClient.Parse(html);
                return FetchProductCapacity(doc, ref hashTable);
            }
            catch (Exception e)
            {
                GatherTools.WriteErrorLog("GetSupplierInfo.SaveProductCapacity()-" + e.Message);
                FileAccessLog.SaveBugInFile(e.ToString());
                return false;
            }
        }

        private bool FetchProductCapacity(Document doc, ref Hashtable CompanyField)
        {
            var trs = doc.Select("table.table tr");
            if (trs.Count > 0)
            {
                foreach (var tr in trs)
                {
                    var th = tr.Select("th").First;
                    var roleName = th?.Text() ?? "";
                    var roleVal = th?.NextElementSibling.Text().Trim() ?? "";
                    if (roleName == "" || roleVal == "")
                        continue;
                    switch (roleName)
                    {
                        case "Factory Size:":
                            CompanyField.Add("factorySize", roleVal);
                            break;
                        case "Factory Location:":
                            CompanyField.Add("factoryLocation", roleVal);
                            break;
                        case "No. of Production Lines:":
                            CompanyField.Add("numberOfProductionLines", roleVal);
                            break;
                        case "Contract Manufacturing:":
                            CompanyField.Add("contractManufacturing", roleVal);
                            break;
                        case "Annual Output Value:":
                            CompanyField.Add("annualOutputValue", roleVal);
                            break;
                        default:
                            continue;
                    }
                }
            }
            return true;
        }

        #endregion

        #region 抓取Contacts
        public bool? SaveContacts(string companyUrl, ref Hashtable hashTable)
        {
            try
            {
                string html = httpHelper.Get(string.Format("{0}/contactinfo.html", companyUrl)).ResultHtml;
                if (string.IsNullOrEmpty(html))
                    return false;
                if (html.Contains("<title>509 unused</title>") || html.Contains("<h1>unused</h1>") || html.Contains("The server encountered an internal error or misconfiguration and was unable to complete your request."))
                    return null;
                Document doc = NSoupClient.Parse(html);
                return FetchContacts(doc, ref hashTable);
            }
            catch (Exception e)
            {
                GatherTools.WriteErrorLog("GetSupplierInfo.SaveContacts()-" + e.Message);
                FileAccessLog.SaveBugInFile(e.ToString());
                return false;
            }
        }

        private bool FetchContacts(Document doc, ref Hashtable CompanyField)
        {
            //保存联系人信息
            GetContactInfo(doc, CompanyField);
            GetContactDetail(doc, CompanyField);
            GetCompanyContactInfo(doc, CompanyField);
            return true;
        }

        private void GetContactInfo(Document doc, Hashtable CompanyField)
        {
            var contactLogo = doc.Select("div.contact-picture img").First;
            if (contactLogo != null)
            {
                CompanyField.Add("contactLogo", "https:" + contactLogo.Attr("src"));
            }
            var contactInfo = doc.Select("div.contact-info").First;
            if (contactInfo != null)
            {
                CompanyField.Add("contactName", contactInfo.Select("h1.name").First?.Text().Trim() ?? "");
                var dts = contactInfo.Select("dl.dl-horizontal dt");
                if (dts.Count > 0)
                {
                    foreach (var dt in dts)
                    {
                        string roleName = dt.Text();
                        string roleVal = dt.NextElementSibling.Text();
                        if (roleName == "" || roleVal == "")
                            continue;
                        switch (roleName)
                        {
                            case "Department:":
                                CompanyField.Add("department", roleVal);
                                break;
                            case "Job Title:":
                                CompanyField.Add("jobTitle", roleVal);
                                break;
                            default:
                                continue;
                        }
                    }
                }
            }
        }

        private void GetContactDetail(Document doc, Hashtable CompanyField)
        {
            //隐藏信息
            //var dtsHide = doc.Select("div.sensitive-info dt");

            //公开信息
            var dts = doc.Select("div.public-info dt");
            if (dts.Count > 0)
            {
                foreach (var dt in dts)
                {
                    string roleName = dt.Text();
                    string roleVal = dt.NextElementSibling.Text();
                    if (roleName == "" || roleVal == "")
                        continue;
                    switch (roleName)
                    {
                        case "Address:":
                            CompanyField.Add("address", roleVal);
                            break;
                        case "Zip:":
                            CompanyField.Add("zip", roleVal);
                            break;
                        case "Country/Region:":
                            CompanyField.Add("country", roleVal);
                            break;
                        case "Province/State:":
                            CompanyField.Add("province", roleVal);
                            break;
                        case "City:":
                            CompanyField.Add("city", roleVal);
                            break;
                        default:
                            continue;
                    }
                }
            }
        }

        private void GetCompanyContactInfo(Document doc, Hashtable CompanyField)
        {
            var trs = doc.Select("table.company-info-data tr");
            if (trs.Count > 0)
            {
                foreach (var tr in trs)
                {
                    var th = tr.Select("th").First;
                    var roleName = th?.Text() ?? "";
                    var roleVal = th?.NextElementSibling.Text().Trim() ?? "";
                    if (roleName == "" || roleVal == "")
                        continue;
                    switch (roleName)
                    {
                        case "Company Name:":
                            CompanyField.Add("companyName", roleVal);
                            break;
                        case "Operational Address:":
                            CompanyField.Add("operationalAddress", roleVal);
                            break;
                        case "Website:":
                            CompanyField.Add("website", roleVal);
                            break;
                        case "Website on alibaba.com:":
                            //CompanyField.Add("contractManufacturing", roleVal);
                            break;
                        default:
                            continue;
                    }
                }
            }
        }

        //获取联系人隐藏联系方式
        public void GetContactHideInfo(AliClient aliClient)
        {
            string html = httpHelper.Get("https://szrider-travelling.en.alibaba.com/contactinfo.html").ResultHtml;
            if (string.IsNullOrEmpty(html))
                return;
            Document doc = NSoupClient.Parse(html);
            var accountId = doc.Select("a.view-contact-trigger").First?.Attr("data-account-id") ?? "";
            if (accountId != "")
            {
                string json = aliClient.Get(string.Format("https://szrider-travelling.en.alibaba.com/event/app/contactPerson/showContactInfo.htm?encryptAccountId={0}", accountId));
            }
        }
        #endregion

        #region 保存所有信息
        public int SaveCompanyField(string CurrentUrl, ref Hashtable CompanyField)
        {
            AddMessage("正在保存数据...");
            if (string.IsNullOrEmpty(CurrentUrl) || CurrentUrl == "" || CompanyField.Count < 3)
            {
                AddMessage("未抓取到完整信息...");
                return (int)Enums.GatherResult.Incomplete;
            }
            Alibaba_CompanyInfo companyInfo = new Alibaba_CompanyInfo();

            try
            {
                companyInfo.CompanyUrl = CurrentUrl;
                companyInfo.Tag = 0;
                companyInfo.PageCount = 0;
                companyInfo.CurrentPage = 0;
                companyInfo.AddTime = DateTime.Now;
                companyInfo.IsGather = true;
                string companyLogo = GatherTools.ObjectConvertString(CompanyField["companyLogo"]);
                companyInfo.CompanyLogo = companyLogo != "" ? Tools.Usual.Utils.DownLoadPicture(companyLogo, GatherTools.GetFileName(companyLogo), SavePath, "companylogo") : companyLogo;

                #region CompanyProfile
                companyInfo.IsVerify = GatherTools.ObjectConvertBoolean(CompanyField["isverify"]);
                companyInfo.VerifyGroup = GatherTools.ObjectConvertString(CompanyField["verifyGroup"]);
                companyInfo.VerifyInfo = GatherTools.ObjectConvertString(CompanyField["verifyInfo"]);
                string verifyLogo = GatherTools.ObjectConvertString(CompanyField["verifyLogo"]);
                companyInfo.VerifyLogo = verifyLogo != "" ? Tools.Usual.Utils.DownLoadPicture(verifyLogo, GatherTools.GetVerifyLogoName(companyInfo.VerifyGroup), SavePath, "verifylogo") : "";

                companyInfo.BusinessType = GatherTools.ObjectConvertString(CompanyField["companyBusinessType"]);
                companyInfo.Location = GatherTools.ObjectConvertString(CompanyField["companyLocation"]);
                companyInfo.MainProducts = GatherTools.ObjectConvertString(CompanyField["supplierMainProducts"]);
                companyInfo.Ownership = GatherTools.ObjectConvertString(CompanyField["assessmentOwnership"]);
                companyInfo.NumberOfEmployees = GatherTools.ObjectConvertString(CompanyField["companyNumberOfEmployees"]);
                companyInfo.TotalAnnualSalesVolume = GatherTools.ObjectConvertString(CompanyField["supplierTotalAnnualSalesVolume"]);
                companyInfo.EstablishedYear = GatherTools.ObjectConvertString(CompanyField["companyEstablishedYear"]);
                companyInfo.Certifications = GatherTools.ObjectConvertString(CompanyField["supplierManagementCertificatesName"]);
                companyInfo.ProductCertifications = GatherTools.ObjectConvertString(CompanyField["supplierProductCertificatesName"]);
                //top3 markets
                if (CompanyField.ContainsKey("companyMainMarket"))
                {
                    string markets = CompanyField["companyMainMarket"].ToString();
                    var list = markets.Split(',');
                    if (list.Length > 0) companyInfo.Top1Market = list[0];
                    if (list.Length > 1) companyInfo.Top2Market = list[1];
                    if (list.Length > 2) companyInfo.Top3Market = list[2];
                }
                #endregion

                #region ProductCapacity
                companyInfo.FactorySize = GatherTools.ObjectConvertString(CompanyField["factorySize"]);
                companyInfo.FactoryLocation = GatherTools.ObjectConvertString(CompanyField["factoryLocation"]);
                companyInfo.NumberOfProductionLines = GatherTools.ObjectConvertString(CompanyField["numberOfProductionLines"]);
                companyInfo.ContractManufacturing = GatherTools.ObjectConvertString(CompanyField["contractManufacturing"]);
                companyInfo.AnnualOutputValue = GatherTools.ObjectConvertString(CompanyField["annualOutputValue"]);
                #endregion

                #region ContactInfo
                string contactLogo = GatherTools.ObjectConvertString(CompanyField["contactLogo"]);
                companyInfo.ContactLogo = contactLogo != "" ? Tools.Usual.Utils.DownLoadPicture(contactLogo, GatherTools.GetFileName(contactLogo), SavePath, "contactlogo") : contactLogo;
                companyInfo.ContactName = GatherTools.ObjectConvertString(CompanyField["contactName"]);
                companyInfo.ContactDepartment = GatherTools.ObjectConvertString(CompanyField["department"]);
                companyInfo.ContactJobTitle = GatherTools.ObjectConvertString(CompanyField["jobTitle"]);
                companyInfo.Address = GatherTools.ObjectConvertString(CompanyField["address"]);
                companyInfo.Zip = GatherTools.ObjectConvertString(CompanyField["zip"]);
                companyInfo.CountryRegion = GatherTools.ObjectConvertString(CompanyField["country"]);
                companyInfo.ProvinceState = GatherTools.ObjectConvertString(CompanyField["province"]);
                companyInfo.City = GatherTools.ObjectConvertString(CompanyField["city"]);

                companyInfo.CompanyName = GatherTools.ObjectConvertString(CompanyField["companyName"]);
                companyInfo.OperationalAddress = GatherTools.ObjectConvertString(CompanyField["operationalAddress"]);
                companyInfo.Website = GatherTools.ObjectConvertString(CompanyField["website"]);
                #endregion

                int res = Bll.BllAlibaba_CompanyInfo.IsExists(CurrentUrl); //判断是否存在
                if (res > 0)
                {
                    //更新记录
                    companyInfo.id = res;
                    AddMessage(companyInfo.CompanyUrl + "已经存在，开始更新");
                    res = Bll.BllAlibaba_CompanyInfo.Update(companyInfo);
                    if (res > 0)
                    {
                        AddMessage(CurrentUrl + " 更新成功!");
                        return (int)Enums.GatherResult.UpdateSuccess;
                    }
                    else
                    {
                        AddMessage(CurrentUrl + " 更新失败!");
                        return (int)Enums.GatherResult.UpdateFailed;
                    }
                }
                else
                {
                    //插入记录
                    res = Bll.BllAlibaba_CompanyInfo.Insert(companyInfo);
                    if (res > 0)
                    {
                        List<Alibaba_CompanyImages> imglist = (List<Alibaba_CompanyImages>)CompanyField["ImgList"];
                        if (imglist != null && imglist.Count > 0)
                        {
                            imglist.ForEach(o => { o.Cid = res; });
                            Bll.BllAlibaba_CompanyImages.Insert(imglist);
                        }
                        return (int)Enums.GatherResult.InsertSuccess;
                    }
                    else
                    {
                        return (int)Enums.GatherResult.InsertFailed;
                    }
                }
            }
            catch (Exception e)
            {
                GatherTools.WriteErrorLog("CompanyInfo.SaveCompanyField()-后" + e.Message);
                AddMessage(e.ToString());
                return (int)Enums.GatherResult.Error;
            }
        }
        #endregion

        #region 辅助方法

        public void AddMessage(string msg)
        {
            lock (this.obj)
            {
                this.MsgQueue.Enqueue(string.Format("{0} - {1}", DateTime.Now, msg));
            }
        }

        public string GetMessage()
        {
            lock (this.obj)
            {
                if (this.MsgQueue.Count > 0)
                    return this.MsgQueue.Dequeue();
                return string.Empty;
            }
        }
        #endregion
    }
}
