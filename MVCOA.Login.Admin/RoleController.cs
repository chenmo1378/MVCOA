using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MVCOA.Helper;
using Common.Attributes;

namespace MVCOA.Login.Admin
{
    public class RoleController:Controller
    {
        #region 1.0 显示 角色视图 +Index() - get
        /// <summary>
        /// 显示 角色视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        } 
        #endregion

        #region 2.0 加载 角色数据 +Index() - post
        /// <summary>
        /// 加载 角色数据
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
            var list = OperateContext.Current.BLLSession.IOu_RoleBLL.GetPagedList(pageIndex, pageSize, ref rowCount, r => r.rIsDel == false, r => r.rId).Select(r => r.ToPOCO()).ToList();
            //2.将list转成 json 字符串发回浏览器
            return Json(new MODEL.EasyUIModel.DataGridModel() { rows = list, total = rowCount });
        }
        #endregion

        #region 3.0 加载 所有的权限树（把角色对应的权限选中） - get
        /// <summary>
        /// 3.0 加载 所有的权限树（把角色对应的权限选中）
        /// </summary>
        /// <param name="id">角色id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetRoleTree(int id)
        {
            //1.获取角色 权限
            var listUserPer = OperateContext.Current.BLLSession.IOu_RolePermissionBLL.GetPermissionByRoleId(id);
            //2.获取所有 权限
            var listAllPer = OperateContext.Current.BLLSession.IOu_PermissionBLL.GetListBy(p => p.pIsDel == false).ToList();
            //3.获取父 权限集合
            var listParentPer = (from p in listAllPer where p.pParent == 1 select p).ToList();

            //准备一个 角色id，传给视图
            ViewBag.roleId = id;

            return PartialView(new MODEL.ViewModel.RolePemissionTree() { UserPer = listUserPer,
                AllPer = listAllPer, AllParentPer = listParentPer });
        } 
        #endregion

        #region 4.0 设置角色权限 + SetRolePer() -post
        /// <summary>
        /// 4.0 设置角色权限
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxRequest]
        public ActionResult SetRolePer()
        {
            try
            {
                //1.获取角色id
                int roleId = int.Parse(Request.Form["rid"]);
                //2.获取新的权限 id字符串
                string[] perIds = Request.Form["roleids"].Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries);

                //方式一 简单方式：先到角色权限表 里 把该角色 所有权限删除，然后再 把新的权限 加进去
                //方式二 麻烦方式：
                //3.1原来的所有权限id 3
                var listOldPer = OperateContext.Current.BLLSession.IOu_RolePermissionBLL.GetPermissionByRoleId(roleId).Select(r => r.pid).ToList();
                
                //3.2新权限id  3,5,6
                var listNewPer = perIds.ToList();
                //3.3将两个集合中 相同的元素 都删除，之后，新权限集合里的权限就是要新增到数据库的，旧权限集合里的权限 就是要从数据库删除的
                for (int i = listOldPer.Count-1; i >= 0; i--)
                {
                    int old = listOldPer[i];
                    if (listNewPer.Contains(old.ToString()))
                    {
                        listOldPer.Remove(old);
                        listNewPer.Remove(old.ToString());
                    }
                }
                //3.4新增新的权限
                listNewPer.ForEach(newP =>
                {
                    OperateContext.Current.BLLSession.IOu_RolePermissionBLL.Add(new MODEL.Ou_RolePermission() { rpRId = roleId, rpPId = int.Parse(newP), rpAddTime = DateTime.Now, rpIsDel = false });
                });
                //3.5移除旧的权限
                listOldPer.ForEach(oldP =>
                {
                    OperateContext.Current.BLLSession.IOu_RolePermissionBLL.DelBy(p => p.rpRId == roleId && p.rpPId == oldP);
                });
            }
            catch (Exception ex)
            {
                return OperateContext.Current.RedirectAjax("err", "异常：" + ex.Message, null,"");
            }
            return OperateContext.Current.RedirectAjax("ok", "修改权限成功~~~！", null, "");
        } 
        #endregion

        [HttpGet]
        [AjaxRequest]
        public ActionResult GetRoleByDepId(int id)
        {
            var list = OperateContext.Current.BLLSession.IOu_RoleBLL.GetListBy(r => r.rDepId == id).Select(r => r.ToPOCO());
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
