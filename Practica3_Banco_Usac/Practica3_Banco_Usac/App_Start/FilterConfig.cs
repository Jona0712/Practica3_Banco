using System.Web;
using System.Web.Mvc;

namespace Practica3_Banco_Usac
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
