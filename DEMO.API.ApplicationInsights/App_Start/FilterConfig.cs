using System.Web;
using System.Web.Mvc;

namespace DEMO.API.ApplicationInsights
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new ErrorHandler.AiHandleErrorAttribute());
		}
	}
}
