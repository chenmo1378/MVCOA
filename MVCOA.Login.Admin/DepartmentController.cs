using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MVCOA.Helper;
using Common.Attributes;

namespace MVCOA.Login.Admin
{
    /// <summary>
    /// 部门管理
    /// </summary>
    public class DepartmentController:Controller
    {
        #region 1.0 部门列表 视图 +Index()
        /// <summary>
        /// 部门列表 视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        } 
        #endregion

        #region 2.0 部门列表 数据 +Index() - get
        /// <summary>
        /// 部门列表 数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxRequest]
        public ActionResult Index(FormCollection form)
        {
            //0.接收参数 page=1&rows=5
            int pageIndex = int.Parse(form["page"]);
            int pageSize = int.Parse(form["rows"]);

            //1.读取数据
            var rowCount = 0;
            var listData = OperateContext.Current.BLLSession.IOu_DepartmentBLL.GetPagedList(pageIndex, pageSize, ref rowCount, d => d.depIsDel == false, d => d.depId, true).Select(d => d.ToPOCO()).ToList();
            //2.返回数据
            return Json(new MODEL.EasyUIModel.DataGridModel() { rows = listData, total = rowCount });
        }
        #endregion

        [HttpGet]
        [AjaxRequest]
        public ActionResult GetAllDepart()
        {
            var list = OperateContext.Current.BLLSession.IOu_DepartmentBLL.GetListBy(d => d.depIsDel == false).Select(u => u.ToPOCO());
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxRequest]
        public ActionResult SetUserRole()
        {
            try
            {
                //0.获取要修改部门的用户id
                int usrId = int.Parse(Request.Form["uId"]);
                //1.获取部门id
                int depId = int.Parse(Request.Form["depid"]);
                //2.获取角色ids
                string[] strRoleIds = Request.Form["rIds"].Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries);//1,2,3,4,
                //3.先跟新用户的部门id
                OperateContext.Current.BLLSession.IOu_UserInfoBLL.Modify(new MODEL.Ou_UserInfo() { uId = usrId, uDepId = depId }, "uDepId");
                //4.更新 用户 和 角色中间表
                //4.1查询 当前用户的 角色id
                var listOldRole = OperateContext.Current.BLLSession.IOu_UserRoleBLL.GetListBy(r => r.urUId == usrId).Select(r => r.urRId).ToList();
                //4.2拿到 新的 角色id
                var listNewRole = strRoleIds.ToList();
                //4.3两个集合比较，去掉重复元素
                for (int i = listOldRole.Count() - 1; i >= 0; i--)
                {
                    int oldRoleId = listOldRole[i];
                    if (listNewRole.Contains(oldRoleId.ToString()))
                    {
                        listOldRole.Remove(oldRoleId);
                        listNewRole.Remove(oldRoleId.ToString());
                    }
                }
                //4.4新增新角色
                listNewRole.ForEach(newR =>
                {
                    OperateContext.Current.BLLSession.IOu_UserRoleBLL.Add(new MODEL.Ou_UserRole() { urUId = usrId, urRId = int.Parse(newR), urAddtime = DateTime.Now, urIsDel = false });
                });

                //4.5删除旧角色
                listOldRole.ForEach(oldR =>
                {
                    OperateContext.Current.BLLSession.IOu_UserRoleBLL.DelBy(ur => ur.urUId == usrId && ur.urRId == oldR);
                });
            }
            catch (Exception ex)
            {
                return OperateContext.Current.RedirectAjax("err", "设置权限失败啦~~~" + ex.Message, null, null);
            }
            return OperateContext.Current.RedirectAjax("ok", "设置权限成功啦~~~", null, null);
        }
    }
}
