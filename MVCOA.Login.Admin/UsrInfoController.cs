using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MVCOA.Helper;

namespace MVCOA.Login.Admin
{
    public class UsrInfoController:Controller
    {
        #region 1.0 显示 用户列表视图
        /// <summary>
        /// 显示 用户列表视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        } 
        #endregion

        #region 1.1 加载 用户列表数据
        /// <summary>
        /// 加载 用户列表数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            //0.接收参数 page=1&rows=5
            int pageIndex = int.Parse(form["page"]);
            int pageSize = int.Parse(form["rows"]);

            //1.读取数据
            var rowCount = 0;
            var list = OperateContext.Current.BLLSession.IOu_UserInfoBLL.GetPagedList(pageIndex, pageSize, ref rowCount, u => u.uIsDel == false, u => u.uId, true).Select(u=>u.ToPOCO());
            //2.返回数据
            return Json(new MODEL.EasyUIModel.DataGridModel() { total = rowCount, rows = list });
        }
        #endregion
    }
}
