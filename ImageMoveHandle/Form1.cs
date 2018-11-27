using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageMoveHandle
{
    public partial class Form1 : Form
    {
        readonly string imgUrl = ConfigurationManager.AppSettings["ImgUrl"].ToString2();
        readonly string gatherImgUrl = ConfigurationManager.AppSettings["gatherImgUrl"].ToString2();
        readonly string gatherImgPath = ConfigurationManager.AppSettings["gatherImgPath"].ToString2();
        readonly string imgSavePath = ConfigurationManager.AppSettings["imgSavePath"].ToString2();

        public Form1()
        {
            InitializeComponent();
            Main();
        }

        public void Main()
        {
            var list = Bll.BllUser_Info.GetAllList();
            foreach (var item in list)
            {
                if (item.GatherId.HasValue)
                    SaveCompanyImg(item.GatherId.Value, item.Id);
            }
            MessageBox.Show("复制已完成！");
            this.Close();
        }

        public void SaveCompanyImg(int gatherCompanyId, int companyId)
        {
            string folderName = string.Empty;
            var imgList = Bll.BllAlibaba_CompanyImages.GetListByCompanyId(gatherCompanyId).ToList();

            string companyImg = string.Empty;
            string productionFlowImg = string.Empty;

            string companyLogo = string.Empty;
            string contactLogo = string.Empty;

            string url = string.Empty;
            string picName = string.Empty;

            //公司图片,生产流程图片
            if (imgList != null && imgList.Count > 0)
            {
                foreach (var img in imgList)
                {
                    if (string.IsNullOrEmpty(img.LocalImagePath))
                        continue;
                    img.LocalImagePath = gatherImgPath + img.LocalImagePath;

                    picName = GetImgName(img.LocalImagePath);
                    switch (img.Type)
                    {
                        case (int)Tools.Enums.Spider.CompanyImg.CompanyImg: 
                            folderName = "cimg\\company";
                            url = MoveSpiderImg(folderName, img.LocalImagePath, picName, true, GetImgNameSmall(picName), 75, 75);
                            companyImg += url + ",";
                            break;
                        case (int)Tools.Enums.Spider.CompanyImg.ProductionFlowImg:
                            folderName = "cimg\\productionflow";
                            url = MoveSpiderImg(folderName, img.LocalImagePath, picName, false, "", 0, 0);
                            productionFlowImg += url + ",";
                            break;
                    }
                }
            }

            //公司logo,联系人logo
            var gatherCompanyModel = Bll.BllAlibaba_CompanyInfo.First(o => o.id == gatherCompanyId);
            if (gatherCompanyModel != null)
            {
                if (!string.IsNullOrEmpty(gatherCompanyModel.CompanyLogo))
                {
                    gatherCompanyModel.CompanyLogo = gatherImgPath + gatherCompanyModel.CompanyLogo.Replace(@"\", "/");
                    folderName = "cimg\\companylogo";
                    picName = GetImgName(gatherCompanyModel.CompanyLogo);
                    url = MoveSpiderImg(folderName, gatherCompanyModel.CompanyLogo, picName, false, "", 0, 0);
                    companyLogo = url;
                }

                if (!string.IsNullOrEmpty(gatherCompanyModel.ContactLogo))
                {
                    gatherCompanyModel.ContactLogo = gatherImgPath + gatherCompanyModel.ContactLogo.Replace(@"\", "/");
                    folderName = "cimg\\contactlogo";
                    picName = GetImgName(gatherCompanyModel.ContactLogo);
                    url = MoveSpiderImg(folderName, gatherCompanyModel.ContactLogo, picName, false, "", 0, 0);
                    contactLogo = url;
                }
            }
            //更新所有图片信息
            Bll.BllUser_Info.UpdateImgs(companyId, companyImg.TrimEnd(','), companyLogo, contactLogo, productionFlowImg.TrimEnd(','));
        }

        protected string GetImgName(string filepath)
        {
            return Guid.NewGuid().ToString().Replace("-", "") + "." + Tools.Usual.Utils.GetFileExt(filepath);
        }

        protected string GetImgNameSmall(string filename)
        {
            int index = filename.LastIndexOf('.');
            return filename.Substring(0, index) + "_s" + filename.Substring(index);
        }

        public string MoveSpiderImg(string folderName, string spiderImgPath, string picName, bool createSmallPic, string smallPicName, int width, int height)
        {
            try
            {
                string setPath = imgSavePath;//存放新图片的绝对路径

                FileInfo file = new FileInfo(spiderImgPath);
                if (file.Exists)
                {
                    //创建保存路径,  例：/product/17/4/21/
                    string dirPath = string.Empty;
                    dirPath = string.Format("\\{0}\\{1}\\{2}\\{3}\\", folderName, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                    //去除配置路径最后的/
                    if (setPath.EndsWith("\\") || setPath.EndsWith("/"))
                        setPath = setPath.Substring(0, setPath.Length - 1);

                    //完整磁盘保存路径, 例：E://WorkerSpace/Mogout/源代码/Mogout.CRM/img/product/17/4/21/
                    string savePath = string.Format(@"{0}{1}", setPath, dirPath);
                    if (!Directory.Exists(savePath))
                        Directory.CreateDirectory(savePath);

                    //完整磁盘文件路径, 例：E://WorkerSpace/Mogout/源代码/Mogout.CRM/img/product/17/4/21/xxxxxx_121313.jpg
                    string path = Path.Combine(savePath, picName);
                    FileInfo fileSave = new FileInfo(path);
                    if (!fileSave.Exists)
                        file.CopyTo(path);

                    //生成小图
                    if (createSmallPic)
                    {
                        //完整磁盘文件路径, 例：E://WorkerSpace/Mogout/源代码/Mogout.CRM/img/product/17/4/21/xxxxxx_121313_s.jpg
                        path = Path.Combine(savePath, smallPicName);
                        fileSave = new FileInfo(path);
                        if (!fileSave.Exists)
                            Tools.Tool.ImageTools.CreateSmallImage(file.FullName, path, width, height);
                    }
                    //返回完整路径，例：http://img.mogout.com/product/17/4/21/xxxxxx_121313.jpg
                    return imgUrl + dirPath.Replace(@"\", @"/") + picName;
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
