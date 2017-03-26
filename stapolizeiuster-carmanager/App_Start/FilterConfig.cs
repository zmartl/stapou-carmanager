using System.Web;
using System.Web.Mvc;

namespace stapolizeiuster_carmanager
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
