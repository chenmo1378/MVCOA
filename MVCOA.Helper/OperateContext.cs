using Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVCOA.Helper
{
    /// <summary>
    /// 操作上下文
    /// </summary>
    public class OperateContext
    {
        const string Admin_CookiePath = "/admin/";
        const string Admin_InfoKey = "ainfo";
        const string Admin_PermissionKey = "apermission";
        const string Admin_TreeString = "aTreeString";
        const string Admin_LogicSessionKey = "BLLSession";

        #region 0.1 Http上下文 及 相关属性
        /// <summary>
        /// Http上下文
        /// </summary>
        HttpContext ContextHttp
        {
            get
            {
                return HttpContext.Current;
            }
        }

        HttpResponse Response
        {
            get
            {
                return ContextHttp.Response;
            }
        }

        HttpRequest Request
        {
            get
            {
                return ContextHttp.Request;
            }
        }

        System.Web.SessionState.HttpSessionState Session
        {
            get
            {
                return ContextHttp.Session;
            }
        }
        #endregion

        #region 0.2实例构造函数 初始化 业务仓储
        public OperateContext()
        {
            BLLSession = DI.SpringHelper.GetObject<IBLL.IBLLSession>("BLLSession");
        }
        #endregion

        #region 0.3 业务仓储 +IBLL.IBLLSession BLLSession
        /// <summary>
        /// 业务仓储
        /// </summary>
        public IBLL.IBLLSession BLLSession; 
        #endregion

        #region 0.4 获取当前 操作上下文 + OperateContext Current
        /// <summary>
        /// 获取当前 操作上下文 (为每个处理浏览器请求的服务器线程 单独创建 操作上下文)
        /// </summary>
        public static OperateContext Current
        {
            get
            {
                OperateContext oContext = CallContext.GetData(typeof(OperateContext).Name) as OperateContext;
                if (oContext == null)
                {
                    oContext = new OperateContext();
                    CallContext.SetData(typeof(OperateContext).Name, oContext);
                }
                return oContext;
            }
        } 
        #endregion

        //---------------------------------------------2.0 登陆权限 等系统操作--------------------

        #region 2.1 当前用户对象 +MODEL.Ou_UserInfo Usr
        // <summary>
        /// 当前用户对象
        /// </summary>
        public MODEL.Ou_UserInfo Usr
        {
            get
            {
                return Session[Admin_InfoKey] as MODEL.Ou_UserInfo;
            }
            set
            {
                Session[Admin_InfoKey] = value;
            }
        }
        #endregion

        #region 0.3 用户权限 +List<MODEL.Ou_Permission> UsrPermission
        // <summary>
        /// 用户权限
        /// </summary>
        public List<MODEL.Ou_Permission> UsrPermission
        {
            get
            {
                return Session[Admin_PermissionKey] as List<MODEL.Ou_Permission>;
            }
            set
            {
                Session[Admin_PermissionKey] = value;
            }
        }
        #endregion

        #region 2.0 根据用户id查询用户权限 +List<MODEL.Ou_Permission> GetUserPermission(int usrId)
        /// <summary>
        /// 根据用户id查询用户权限
        /// </summary>
        /// <param name="usrId"></param>
        public List<MODEL.Ou_Permission> GetUserPermission(int usrId)
        {
            //-------A.根据用户角色查询
            //1.0 根据用户 id 查到 该用户的 角色id
            List<int> listRoleId = Current.BLLSession.IOu_UserRoleBLL.GetListBy(ur => ur.urUId == usrId).Select(ur => ur.urRId).ToList();
            //2.0 根据角色 id 查询角色权限 id
            List<int> listPerIds = Current.BLLSession.IOu_RolePermissionBLL.GetListBy(rp => listRoleId.Contains(rp.rpRId)).Select(rp => rp.rpPId).ToList();
            //3.0 查询所有角色数据
            List<MODEL.Ou_Permission> listPermission = Current.BLLSession.IOu_PermissionBLL.GetListBy(p => listPerIds.Contains(p.pid)).Select(p => p.ToPOCO()).ToList();
            //-------B.根据用户特权查询
            //b.1 查询 用户特权id
            List<int> vipPerIds = Current.BLLSession.IOu_UserVipPermissionBLL.GetListBy(vip => vip.vipUserId == usrId).Select(vip => vip.vipPermission).ToList();
            //b.2 查询 特权数据
            List<MODEL.Ou_Permission> listPermission2 = Current.BLLSession.IOu_PermissionBLL.GetListBy(p => vipPerIds.Contains(p.pid)).Select(p => p.ToPOCO()).ToList();
            //-------C.将两个权限集合 合并（将集合2的权限数据 添加到 集合1中）
            listPermission2.ForEach(p =>
            {
                listPermission.Add(p);
            });
            return listPermission.OrderBy(u => u.pOrder).ToList();
        }
        #endregion

        #region 2.1 管理员登录方法 + bool LoginAdmin(MODEL.ViewModel.LoginUser usrPara)
        /// <summary>
        /// 管理员登录方法
        /// </summary>
        /// <param name="usrPara"></param>
        public bool LoginAdmin(MODEL.ViewModel.LoginUser usrPara)
        {
            //到业务成查询
            MODEL.Ou_UserInfo usr = BLLSession.IOu_UserInfoBLL.Login(usrPara.LoginName, usrPara.Pwd);
            if (usr != null)
            {
                //2.1 保存 用户数据(Session or Cookie)
                Usr = usr;
                //如果选择了复选框，则要使用cookie保存数据
                if (usrPara.IsAlways)
                {
                    //2.1.2将用户id加密成字符串
                    string strCookieValue = Common.SecurityHelper.EncryptUserInfo(usr.uId.ToString());
                    //2.1.3创建cookie
                    HttpCookie cookie = new HttpCookie(Admin_InfoKey, strCookieValue);
                    cookie.Expires = DateTime.Now.AddDays(1);
                    cookie.Path = Admin_CookiePath;
                    Response.Cookies.Add(cookie);
                }
                //2.2 查询当前用户的 权限，并将权限 存入 Session 中
                UsrPermission = GetUserPermission(usr.uId);
                return true;
            }
            return false;
        } 
        #endregion

        #region 2.2 判断当前用户是否登陆 +bool IsLogin()
        /// <summary>
        /// 判断当前用户是否登陆 而且
        /// </summary>
        /// <returns></returns>
        public bool IsLogin()
        {
            //1.验证用户是否登陆(Session && Cookie)
            if (Session[Admin_InfoKey] == null)
            {
                if (Request.Cookies[Admin_InfoKey] == null)
                {
                    return false;
                }
                else//如果有cookie则从cookie中获取用户id并查询相关数据存入 Session
                {
                    string strUserInfo = Request.Cookies[Admin_InfoKey].Value;
                    strUserInfo = Common.SecurityHelper.DecryptUserInfo(strUserInfo);
                    int userId = int.Parse(strUserInfo);
                    MODEL.Ou_UserInfo usr = BLLSession.IOu_UserInfoBLL.GetListBy(u => u.uId == userId).First();
                    Usr = usr;
                    UsrPermission = OperateContext.Current.GetUserPermission(usr.uId);
                }
            }
            return true;
        } 
        #endregion

        #region  2.3 判断当前用户 是否有 访问当前页面的权限 +bool HasPemission
        /// <summary>
        /// 2.3 判断当前用户 是否有 访问当前页面的权限
        /// </summary> 
        /// <param name="areaName"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        public bool HasPemission(string areaName, string controllerName, string actionName, string httpMethod)
        {
            var listP = from per in UsrPermission
                        where
                            string.Equals(per.pAreaName, areaName, StringComparison.CurrentCultureIgnoreCase) &&
                            string.Equals(per.pControllerName, controllerName, StringComparison.CurrentCultureIgnoreCase) &&
                            string.Equals(per.pActionName, actionName, StringComparison.CurrentCultureIgnoreCase) &&(
                                per.pFormMethod==3 ||//如果数据库保存的权限 请求方式 =3 代表允许 get/post请求
                                per.pFormMethod == (httpMethod.ToLower() == "get" ? 1 : 2)
                            )
                        select per;
            return listP.Count() > 0;
        } 
        #endregion

        #region 2.4 获取当前登陆用户的权限树Json字符串 +string UsrTreeJsonStr
        /// <summary>
        /// 获取当前登陆用户的权限树Json字符串
        /// </summary>
        public string UsrTreeJsonStr
        {
            get
            {
                if (Session[Admin_TreeString] == null)
                {
                    //将 登陆用户的 权限集合 转成 树节点 集合（其中 IsShow = false的不要生成到树节点集合中）
                    List<MODEL.EasyUIModel.TreeNode> listTree = MODEL.Ou_Permission.ToTreeNodes(UsrPermission.Where(p => p.pIsShow == true).OrderBy(p=>p.pOrder).ToList());
                    Session[Admin_TreeString] = Common.DataHelper.Obj2Json(listTree);
                }
                return Session[Admin_TreeString].ToString();
            }
        } 
        #endregion

        //---------------------------------------------3.0 公用操作方法--------------------

        #region 3.1 生成 Json 格式的返回值 +ActionResult RedirectAjax(string statu, string msg, object data, string backurl)
        /// <summary>
        /// 生成 Json 格式的返回值
        /// </summary>
        /// <param name="statu"></param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <param name="backurl"></param>
        /// <returns></returns>
        public ActionResult RedirectAjax(string statu, string msg, object data, string backurl)
        {
            MODEL.FormatModel.AjaxMsgModel ajax = new MODEL.FormatModel.AjaxMsgModel()
            {
                Statu = statu,
                Msg = msg,
                Data = data,
                BackUrl = backurl
            };
            JsonResult res = new JsonResult();
            res.Data = ajax;
            res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return res;
        } 
        #endregion

        #region 3.2 重定向方法 根据Action方法特性  +ActionResult Redirect(string url, ActionDescriptor action)
        /// <summary>
        /// 重定向方法 有两种情况：如果是Ajax请求，则返回 Json字符串；如果是普通请求，则 返回重定向命令
        /// </summary>
        /// <returns></returns>
        public ActionResult Redirect(string url, ActionDescriptor action)
        {
            //如果Ajax请求没有权限，就返回 Json消息
            if (action.IsDefined(typeof(AjaxRequestAttribute), false)
            || action.ControllerDescriptor.IsDefined(typeof(AjaxRequestAttribute), false))
            {
                return RedirectAjax("nologin", "您没有登陆或没有权限访问此页面~~", null, url);
            }
            else//如果 超链接或表单 没有权限访问，则返回 302重定向命令
            {
                return new RedirectResult(url);
            }
        } 
        #endregion

    }
}
