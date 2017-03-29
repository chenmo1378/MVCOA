using MVCOA.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MVCOA.Logic
{
    public class HomeController:Controller
    {
        /// <summary>
        /// 测试方法，读取 所有的 权限数据
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //1.要调用业务层 对象 获取 权限集合
            List<MODEL.Ou_Permission> list = OperateContext.Current.BLLSession.IOu_PermissionBLL.GetListBy(p => p.pIsDel == false);
            //2.加载视图
            return View(list);
        }
    }
}
