using System.Web.Mvc;

namespace MVCOA.Login.Admin
{
    /// <summary>
    /// 区域注册类
    /// </summary>
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName//加载 视图用的！
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            int a = 1;
            context.MapRoute(
                "Admin_default",// admin/admin/login
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[1] { "MVCOA.Login.Admin" }
            );
        }
    }
}
