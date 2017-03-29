using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBLL
{
    public partial interface IOu_UserInfoBLL
    {
        MODEL.Ou_UserInfo Login(string strName, string strPwd);
    }
}
