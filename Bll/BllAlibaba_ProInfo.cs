using Dos.DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGather.Model;

namespace Bll
{
    public class BllAlibaba_ProInfo : Repository<Alibaba_ProInfo>
    {
        public static int IsExists(long aliProId)
        {
            var list = Query(o => o.AliProductId == aliProId, o => o.Id, "desc", o => o.Id, 1);
            if (list != null && list.Count > 0)
                return list[0].Id;
            return 0;
        }

        public static List<Alibaba_ProInfo> GetHasDownImgProList()
        {
            return Query(o => o.Detail.Contains("https://"), o => o.Id, "desc", o => new { o.Id, o.Detail });
        }
    }
}
