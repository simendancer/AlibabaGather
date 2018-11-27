using Dos.DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGather.Model;

namespace Bll
{
    public class BllAlibaba_ProductImages : Repository<Alibaba_ProImages>
    {
        public static IEnumerable<Alibaba_ProImages> GetDetailImgListByProId(int gatherProId)
        {
            return Db.Context.From<Alibaba_ProImages>().Where(o => o.Pid == gatherProId && o.Type == 2).OrderBy(o => o.ID).ToList();
        }
    }
}
