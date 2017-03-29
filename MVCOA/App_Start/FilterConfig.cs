using System.Web;
using System.Web.Mvc;

namespace MVCOA
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Login.Admin.Filters.LoginValidateAttribute());
        }
    }
}