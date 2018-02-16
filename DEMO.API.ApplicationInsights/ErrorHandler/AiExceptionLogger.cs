using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace DEMO.API.ApplicationInsights.ErrorHandler
{
	public class AiExceptionLogger : ExceptionLogger
	{
		public override void Log(ExceptionLoggerContext context)
		{
			if (context != null && context.Exception != null)
			{//or reuse instance (recommended!). see note above 
				var ai = new TelemetryClient();
				ai.TrackException(context.Exception);
			}
			base.Log(context);
		}
	}
}