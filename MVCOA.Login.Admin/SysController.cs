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
    /// 系统管理
    /// </summary>
    public class SysController : Controller
    {
        #region 1.0 权限列表 视图 +Permission()
        [HttpGet]
        /// <summary>
        /// 权限列表 视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Permission()
        {
            return View();
        } 
        #endregion

        #region 1.1 权限列表 数据 +GetPermData()
        [HttpPost]
        /// <summary>
        /// 权限列表 视图
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPermData()
        {
            //获取页容量
            int pageSize = int.Parse(Request.Form["rows"]);
            //获取请求的页码
            int pageIndex = int.Parse(Request.Form["page"]);

            int rowCount = 0;
            // 查询分页数据
            var list = OperateContext.Current.BLLSession.IOu_PermissionBLL.GetPagedList(pageIndex, pageSize, ref rowCount, p => p.pParent == 1 && p.pIsDel == false, p => p.pOrder).Select(p => p.ToPOCO());
            //2 生成规定格式的 json字符串发回 给 异步对象
            MODEL.EasyUIModel.DataGridModel dgModel = new MODEL.EasyUIModel.DataGridModel()
            {
                total = rowCount,
                rows = list,
                footer = null
            };
            return Json(dgModel);
        }
        #endregion

        #region 1.2 子权限列表 视图 + PermissionSon() -get
        [HttpGet]
        /// <summary>
        /// 子权限列表 视图
        /// </summary>
        /// <returns></returns>
        public ActionResult PermissionSon()
        {
            //获取父权限id
            int parentMenuId = int.Parse(Request.QueryString["pid"]);
            return View();
        } 
        #endregion

        #region 1.2. 查询子权限列表数据 + PermissionSon -post
        [HttpPost]
        /// <summary>
        /// 查询子权限列表数据
        /// </summary>
        /// <returns></returns>
        public ActionResult PermissionSon(FormCollection form)
        {
            //获取页容量
            int pageSize = int.Parse(Request.Form["rows"]);
            //获取请求的页码
            int pageIndex = int.Parse(Request.Form["page"]);
            //获取父权限id
            int parentMenuId = int.Parse(Request.QueryString["pid"]);
            int rowCount = 0;
            // 查询分页数据
            var list = OperateContext.Current.BLLSession.IOu_PermissionBLL.GetPagedList(pageIndex, pageSize, ref rowCount, p => p.pParent == parentMenuId && p.pIsDel == false, p => p.pOrder).Select(p => p.ToPOCO());
            //2 生成规定格式的 json字符串发回 给 异步对象
            MODEL.EasyUIModel.DataGridModel dgModel = new MODEL.EasyUIModel.DataGridModel()
            {
                total = rowCount,
                rows = list,
                footer = null
            };
            return Json(dgModel);
        } 
        #endregion

        //------------------------------------------
        /// <summary>
        /// 设置新增和修改通用的下拉框数据
        /// </summary>
        void SetDropDonwList()
        {
            //准备请求方式下拉框数据
            ViewBag.httpMethodList = new List<SelectListItem>() { 
             new SelectListItem(){ Text="Get",Value="1"},
             new SelectListItem(){ Text="Post",Value="2"},
             new SelectListItem(){ Text="Both",Value="3"}
            };

            /*
             0-无操作 1-easyui连接 2-打开新窗体
             */
            ViewBag.operationList = new List<SelectListItem>() { 
             new SelectListItem(){ Text="无操作",Value="0"},
             new SelectListItem(){ Text="easyui连接",Value="1"},
             new SelectListItem(){ Text="打开新窗体",Value="2"}
            };
        }

        /// <summary>
        /// 生成 父菜单 下拉框 html字符串
        /// </summary>
        /// <returns></returns>
        string GetParentMenuDrowDownListHtml()
        {
            //1.查询所有的父菜单
            var list = OperateContext.Current.BLLSession.IOu_PermissionBLL.GetListBy(p => p.pParent == 1).Select(p => p.ToViewModel()).ToList();
            
            System.Text.StringBuilder sb = new StringBuilder("<select name='pParent'><option value=1>父菜单</option>",1000);
            list.ForEach(p => sb.AppendLine("<option value='" + p.pid + "'>" + p.pName + "</option>"));
            sb.AppendLine("</select>");

            return sb.ToString();
        }
        //------------------------------------------

        #region 1.2 加载 权限修改 窗体html +EditPermission()
        [HttpGet]
        [AjaxRequest]
        /// <summary>
        /// 1.2 加载 权限修改 窗体html
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPermission(int id)
        {
            //根据id查询要修改的权限
            var model = OperateContext.Current.BLLSession.IOu_PermissionBLL.GetListBy(p => p.pid == id).FirstOrDefault().ToViewModel();

            SetDropDonwList();

            //将 权限对象 传给 视图 的 Model属性
            return PartialView(model);
        } 
        #endregion

        #region 1.2 权限修改 +EditPermission(MODEL.ViewModel.Permission model)
        [HttpPost]
        [AjaxRequest]
        /// <summary>
        /// 1.2 权限修改
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPermission(MODEL.Ou_Permission model)
        {
            int res = OperateContext.Current.BLLSession.IOu_PermissionBLL.Modify(model, "pName", "pAreaName", "pControllerName", "pActionName", "pFormMethod", "pOperationType", "pOrder", "pIsShow", "pRemark");

            if (res > 0)
                return Redirect("/admin/sys/Permission?ok");
            else
                return Redirect("/admin/sys/Permission?err");
        }
        #endregion

        #region 1.3 显示 新增权限 表单代码 +AddPermission()
        [HttpGet]
        [AjaxRequest]
        /// <summary>
        /// 显示 新增权限 表单代码
        /// </summary>
        /// <returns></returns>
        public ActionResult AddPermission()
        {
            SetDropDonwList();
            ViewBag.ParentMenuHtml = GetParentMenuDrowDownListHtml();
            return PartialView("EditPermission");
        } 
        #endregion

        #region 1.3 新增权限 +AddPermission()
        [HttpPost]
        [AjaxRequest]
        /// <summary>
        /// 新增权限
        /// </summary>
        /// <returns></returns>
        public ActionResult AddPermission(MODEL.Ou_Permission model)
        {
            model.pIsDel = false;
            model.pAddTime = DateTime.Now;

            int res = OperateContext.Current.BLLSession.IOu_PermissionBLL.Add(model);

            if (res > 0)
                return Redirect("/admin/sys/Permission?ok");
            else
                return Redirect("/admin/sys/Permission?err");
        }
        #endregion

        #region 1.4 删除权限 +DelPemission()
        /// <summary>
        /// 1.4 删除权限
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxRequest]
        public ActionResult DelPemission()
        {
            try
            {
                int id = int.Parse(Request.Form["id"]);
                OperateContext.Current.BLLSession.IOu_PermissionBLL.DelBy(p => p.pid == id);
                return OperateContext.Current.RedirectAjax("ok", "删除成功~~~", null, "");
            }
            catch (Exception ex)
            {
                return OperateContext.Current.RedirectAjax("err", "您现在删除的权限正在被角色使用，请先取消角色中的这个权限！", null, "");
            }

        } 
        #endregion
    }
}
