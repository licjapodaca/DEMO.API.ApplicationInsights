using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using DEMO.API.ApplicationInsights.ErrorHandler;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace DEMO.API.ApplicationInsights
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

			#region Activando CORS de manera Global (Para todos los Controllers Web API)

			var cors = new EnableCorsAttribute(origins: "*", headers: "*", methods: "*");
			config.EnableCors(cors);

			#endregion

			// Web API routes
			config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

			config.Services.Add(typeof(IExceptionLogger), new AiExceptionLogger());
		}
    }
}
