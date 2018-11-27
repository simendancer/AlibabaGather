using Dos.DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGather.Model;

namespace Bll
{
    public class BllAlibaba_CompanyGather : Repository<WebGather.Model.Alibaba_CompanyGather> 
    {
        public static bool IsExists(string companyUrl)
        {
            var list = Query(o => o.CompanyUrl == companyUrl, o => o.ID, "desc", o => o.ID, 1);
            if (list != null && list.Count > 0)
                return true;
            return false;
        }

        public static bool UpdateTag(int id, int tag)
        {
            return Update(new Alibaba_CompanyGather() { ID = id, Tag = tag }) > 0;
        }
    }
}
