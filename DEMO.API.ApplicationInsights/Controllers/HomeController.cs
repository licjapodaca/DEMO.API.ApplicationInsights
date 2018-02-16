using DEMO.API.ApplicationInsights.ErrorHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DEMO.API.ApplicationInsights.Controllers
{
	[AiHandleError]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Title = "Home Page";

			return View();
		}
	}
}
