using Dos.DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGather.Model;

namespace Bll
{
    public class BllAlibaba_ProductGather : Repository<WebGather.Model.Alibaba_ProGather>
    {
        public static bool IsExists(long aliProId)
        {
            var list = Query(o => o.AliProductId == aliProId, o => o.ID, "desc", o => o.ID, 1);
            if (list != null && list.Count > 0)
                return true;
            return false;
        }

        public static bool UpdateTag(int id, int tag)
        {
            return Update(new Alibaba_ProGather() { ID = id, Tag = tag }) > 0;
        }

        public static IList<Alibaba_ProGather> GetList(int tag, int top)
        {
            int min = (int)Db.Context.Min<Alibaba_ProGather>(Alibaba_ProGather._.ID, o => o.Tag == tag);
            var list = Db.Context.From<Alibaba_ProGather>()
                .Where(o => o.Tag == tag && o.ID >= min)
                .Select(o => new { o.ID, o.Tag, o.ProductUrl, o.CompanyId, o.AliProductId, o.AliGroupId })
                .Top(top)
                .ToList();
            return list.ToList();
        }

        public static IList<Alibaba_ProGather> GetListByCompanyId(int companyId, int tag, int top)
        {
            var list = Db.Context.From<Alibaba_ProGather>()
                .Where(o => o.CompanyId == companyId && o.Tag == tag)
                .Select(o => new { o.ID, o.Tag, o.ProductUrl, o.CompanyId, o.AliProductId, o.AliGroupId })
                .OrderBy(o => o.ID)
                .Top(top)
                .ToList();
            return list.ToList();
        }
    }
}
