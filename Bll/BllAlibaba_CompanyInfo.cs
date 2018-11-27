using Dos.DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGather.Model;

namespace Bll
{
    public class BllAlibaba_CompanyInfo : Repository<Alibaba_CompanyInfo>
    {
        /// <summary>
        /// 判断是否存在记录，返回Id
        /// </summary>
        /// <param name="companyUrl"></param>
        /// <returns></returns>
        public static int IsExists(string companyUrl)
        {
            var list = Query(o => o.CompanyUrl == companyUrl, o => o.id, "desc", o => o.id, 1);
            if (list != null && list.Count > 0)
                return list[0].id;
            return 0;
        }

        public static bool RestForAll()
        {
            var res = Db.Context.FromSql("update Alibaba_CompanyInfo set CurrentPage=0");
            return res.ExecuteNonQuery() > 0;
        }

        public static bool UpdateTag(int id, int tag)
        {
            return Update(new Alibaba_CompanyInfo() { id = id, Tag = tag }) > 0;
        }
    }
}
