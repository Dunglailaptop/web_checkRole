using System.Web;
using System.Web.Mvc;

namespace webapp_manageSupperBrain
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
