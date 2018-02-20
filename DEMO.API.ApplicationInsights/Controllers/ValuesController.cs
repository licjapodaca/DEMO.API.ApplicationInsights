using DEMO.API.ApplicationInsights.Entities;
using DEMO.API.ApplicationInsights.ErrorHandler;
using DEMO.API.ApplicationInsights.Models;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace DEMO.API.ApplicationInsights.Controllers
{
	[Authorize]
    public class ValuesController : ApiController
    {
		private ApplicationDbContext _context;
		private readonly TelemetryClient _telemetry;

		public ValuesController()
		{
			_context = new ApplicationDbContext();
			_telemetry = new TelemetryClient();

			_telemetry.Context.Properties.Add("TENANT", "CFE");
		}

        // GET api/values
        public async Task<IHttpActionResult> Get()
        {
			return await Task.Run<IHttpActionResult>(() =>
			{
				try
				{
					var usuarios = _context.Users.Select(p => new
					{
						Id = p.Id,
						Username = p.UserName,
						Email = p.Email,
						AccessFailedCount = p.AccessFailedCount
					});

					return Ok(usuarios);
				}
				catch (Exception)
				{
					throw;
				}
			});
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values/marcas
		[Route("api/values/marcas")]
		[HttpPost]
        public async Task<IHttpActionResult> AgregaMarca([FromBody]VehiculoMarca datos)
        {
			return await Task.Run<IHttpActionResult>(() =>
			{
				try
				{
					if(!ModelState.IsValid)
						return BadRequest(ModelState);

					var marca = _context.VehiculoMarcas.Add(datos);

					_context.SaveChanges();

					_telemetry.TrackEvent("MarcaAgregada");

					return Ok(new { success = true, marcaAgregada = marca });
				}
				catch (Exception)
				{
					throw;
				}
			});
        }

		// POST api/values/marcas
		[Route("api/values/submarcas")]
		[HttpPost]
		public async Task<IHttpActionResult> AgregaSubMarca([FromBody]VehiculoSubmarca datos)
		{
			return await Task.Run<IHttpActionResult>(() =>
			{
				try
				{
					if (!ModelState.IsValid)
						return BadRequest(ModelState);

					var submarca = _context.VehiculoSubmarcas.Add(datos);

					_context.SaveChanges();

					_telemetry.TrackEvent("SubmarcaAgregada");

					return Ok(new { success = true, SubmarcaAgregada = submarca });
				}
				catch (Exception)
				{
					throw;
				}
			});
		}

		// GET api/values/marcas
		[Route("api/values/marcas")]
		[HttpGet]
		public async Task<IHttpActionResult> ObtenerMarcas([FromUri] int seconds = 0)
		{
			return await Task.Run<IHttpActionResult>(() =>
			{
				try
				{
					var inicio = DateTime.Now;
					Trace.TraceInformation($"Iniciando metodo ObtenerMarcas() - Inicio [{inicio}]");

					if (seconds > 0)
						Thread.Sleep(seconds * 1000);

					var marcas = _context.VehiculoMarcas.Select(p => new
					{
						Id = p.Id,
						Marca = p.NombreMarca
					}).ToList();

					var final = DateTime.Now;

					Trace.TraceInformation($"Termino de obtener [{marcas.Count}] marcas en un tiempo de {(final - inicio).TotalSeconds} sec / {(final - inicio).TotalMilliseconds} milliseconds | Fin [{final}]");

					_telemetry.TrackEvent("MarcasConsultadas");

					return Ok(marcas);
				}
				catch (Exception)
				{
					throw;
				}
			});
		}

		// GET api/values/submarcas
		[Route("api/values/submarcas")]
		[HttpGet]
		public async Task<IHttpActionResult> ObtenerSubmarcas([FromUri] int seconds = 0)
		{
			return await Task.Run<IHttpActionResult>(() =>
			{
				try
				{
					//var inicio = DateTime.Now;
					Trace.TraceInformation($"Iniciando metodo ObtenerSubmarcas() e inicializando stopwatch...");

					var stopwatch = System.Diagnostics.Stopwatch.StartNew();

					Trace.TraceInformation($"Iniciando el calculo de retraso con Thread.Sleep ...");

					if (seconds > 0)
						Thread.Sleep(seconds * 1000);

					Trace.TraceInformation($"Ejecutando LINQ para obtener submarcas...");

					var submarcas = _context.VehiculoSubmarcas.Select(p => new
					{
						IdMarca = p.VehiculoMarca.Id,
						Marca = p.VehiculoMarca.NombreMarca,
						IdSubmarca = p.Id,
						Submarca = p.NombreSubmarca
					}).ToList();

					Trace.TraceInformation($"Termino de ejecutar el LINQ a submarcas...");

					//var final = DateTime.Now;

					//Trace.TraceInformation($"Termino de obtener [{submarcas.Count}] submarcas en un tiempo de {(final - inicio).TotalSeconds} sec / {(final - inicio).TotalMilliseconds} milliseconds | Fin [{final}]");

					Trace.TraceInformation($"Terminando el stopwatch ...");

					stopwatch.Stop();

					Trace.TraceInformation($"Enviando evento Submarcas de Vehiculo con propiedades y metricas...");

					var metrics = new Dictionary<string, double>
					{
						{ "TiempoProcesamientoMilisegundos", stopwatch.Elapsed.TotalMilliseconds },
						{ "TiempoProcesamientoSegundos", stopwatch.Elapsed.TotalSeconds }
					};

					// Set up some properties:
					var properties = new Dictionary<string, string> { { "TotalSubmarcas", submarcas.Count.ToString() } };

					// Send the event:
					_telemetry.TrackEvent("Submarcas de Vehiculos", properties, metrics);

					_telemetry.TrackEvent("SubmarcasConsultadas");

					return Ok(submarcas);
				}
				catch (Exception)
				{
					throw;
				}
			});
		}

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
