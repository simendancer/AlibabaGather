using Dos.DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class BllAlibaba_ProClass : Repository<WebGather.Model.Alibaba_ProClass>
    {
        public static bool RestForAll()
        {
            var res = Db.Context.FromSql("update Alibaba_ProClass set CurrentPage=0");
            return res.ExecuteNonQuery() > 0;
        }
    }
}
