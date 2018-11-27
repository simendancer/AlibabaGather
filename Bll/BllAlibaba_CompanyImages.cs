using Dos.DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGather.Model;

namespace Bll
{
    public class BllAlibaba_CompanyImages : Repository<WebGather.Model.Alibaba_CompanyImages>
    {
        public static IEnumerable<Alibaba_CompanyImages> GetListByCompanyId(int companyId)
        {
            return Db.Context.From<Alibaba_CompanyImages>().Where(o => o.Cid == companyId && o.IsDown == true).ToList();
        }
    }
}
