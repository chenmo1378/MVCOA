using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DALMSSQL
{
    public partial class Ou_UserInfoDAL:IDAL.IOu_UserInfoDAL
    {
        public MODEL.Ou_UserInfo Login(string loginName)
        {
            return base.GetListBy(u => u.uLoginName == loginName).FirstOrDefault();
        }
    }
}
