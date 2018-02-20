using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DEMO.API.ApplicationInsights
{
	public class TelemetryInitializer : ITelemetryInitializer
	{
		public void Initialize(ITelemetry telemetry)
		{
			var requestTelemetry = telemetry as RequestTelemetry;
			if (requestTelemetry?.Context?.Cloud == null) return;
			requestTelemetry.Context.Cloud.RoleName = WebConfigurationManager.AppSettings.Get("AppInsights-CloudRoleName");

			telemetry.Context.User.Id = HttpContext.Current.User.Identity.GetUserName(); //HttpContext.Current.User.Identity.GetUserId();
			telemetry.Context.User.AuthenticatedUserId = HttpContext.Current.User.Identity.GetUserName();
			telemetry.Context.Session.Id = HttpContext.Current.User.Identity.GetUserName(); //HttpContext.Current.User.Identity.GetUserId();

		}
	}

	public class WebApiApplication : System.Web.HttpApplication
	{

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			ApplicationInsights_Initialize();
		}

		public static void ApplicationInsights_Initialize()
		{
			if (WebConfigurationManager.AppSettings.Get("AppInsights-DeshabilitaTelemetria").ToLower().Trim() == "true")
			{
				// Deshabilitando envio de teletria a la NUBE cuando la app este localmente
				TelemetryConfiguration.Active.DisableTelemetry = true;
				TelemetryConfiguration.Active.InstrumentationKey = "";
			}
			else
			{
				// Asignando configuracion inicial de la telemetria para Application Insights
				TelemetryConfiguration.Active.TelemetryInitializers.Add(new TelemetryInitializer());

				// Deshabilitando envio de teletria a la NUBE cuando la app este localmente
				TelemetryConfiguration.Active.DisableTelemetry = false;
				TelemetryConfiguration.Active.InstrumentationKey = WebConfigurationManager.AppSettings.Get("AppInsights-InstrumentationKey");
			}
		}
	}
}
