using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum
{
    public class Enums
    {
        public enum CompanyImg : byte
        {
            [Description("公司图")]
            CompanyImg = 1,
            [Description("生产流程图")]
            ProductionFlowImg = 2,
            [Description("公司Logo")]
            CompanyLogo = 3,
            [Description("联系人Logo")]
            ContactLogo = 4
        }

        public enum GatherResult : int
        {
            [Description("数据不完整")]
            Incomplete = -2,
            [Description("发生错误")]
            Error = -1,
            [Description("添加失败")]
            InsertFailed = 0,
            [Description("添加成功")]
            InsertSuccess = 1,
            [Description("更新成功")]
            UpdateSuccess = 2,
            [Description("更新失败")]
            UpdateFailed = 3,
            [Description("图片插入失败")]
            ImgFailed = 4
        }

        public enum ProductImg : byte
        {
            [Description("产品图片")]
            ProductImg = 1,
            [Description("产品详细页内容图片")]
            DetailImg = 2
        }

        public enum ImgType : byte
        {
            [Description("原图")]
            Img = 1,
            [Description("图片50*50")]
            Img50 = 2,
            [Description("图片200*200")]
            Img200 = 3,
            [Description("图片350*350")]
            Img350 = 4
        }

        public enum ProListGatherTag : int
        {
            [Description("未采集")]
            Begin = 0,
            [Description("采集中")]
            Working = 1,
            [Description("采集完成")]
            Finish = 2,
        }

        public enum CheckStatus : int
        {
            [Description("处理失败")]
            Fail = -1,
            [Description("未处理")]
            Wait = 0,
            [Description("处理成功")]
            Success = 1,
            [Description("图片未下载")]
            UnDownLoad = 2
        }
    }
}
