using Dos.DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGather.Model;

namespace Bll
{
    public class BllUser_Info : Repository<WebGather.Model.User_Info>
    {
        public static bool UpdateImgs(int companyId, string companyImg, string companylogo, string contactlogo, string productflowImg)
        {
            var userInfo = new User_Info();
            if (!string.IsNullOrEmpty(companyImg))
                userInfo.CompanyPic = companyImg;

            if (!string.IsNullOrEmpty(companylogo))
                userInfo.Logo = companylogo;

            if (!string.IsNullOrEmpty(contactlogo))
                userInfo.ContactPic = contactlogo;

            if (!string.IsNullOrEmpty(productflowImg))
                userInfo.ProductionFlowPic = productflowImg;

            return Db.Context.Update(userInfo, o => o.IsGather == true && o.Id == companyId) > 0;
        }

        public static IEnumerable<User_Info> GetAllList()
        {
            return Db.Context.From<User_Info>().ToList();
        }
    }
}
