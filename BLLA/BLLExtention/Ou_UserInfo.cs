using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLLA
{
    public partial class Ou_UserInfo:IBLL.IOu_UserInfoBLL
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="strPwd"></param>
        /// <returns></returns>
        public MODEL.Ou_UserInfo Login(string strName, string strPwd)
        {
            //1.调用业务层方法 根据登陆名查询
            MODEL.Ou_UserInfo usr = base.GetListBy(u => u.uLoginName == strName).Select(u => u.ToPOCO()).First();
            //2.判断是否登陆成功
            if (usr != null && usr.uPwd == Common.DataHelper.MD5(strPwd))
            {
                return usr;
            }
            return null;
        }
    }
}
