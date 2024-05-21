using System.Web;
using System.Web.Mvc;
using PHONGKHAMTHUY.Filters;

namespace PHONGKHAMTHUY
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
