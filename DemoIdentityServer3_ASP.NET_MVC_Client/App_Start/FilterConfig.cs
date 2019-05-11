using System.Web;
using System.Web.Mvc;

namespace DemoIdentityServer3_ASP.NET_MVC_Client
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
