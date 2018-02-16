using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DEMO.API.ApplicationInsights.Entities
{
	public class VehiculoSubmarca
	{
		public int Id { get; set; }
		[Required]
		[ForeignKey("VehiculoMarca")]
		public int VehiculoMarcaId { get; set; }
		[Index(IsUnique = true)]
		[Required]
		[MaxLength(100)]
		public string NombreSubmarca { get; set; }

		// Navigation
		public virtual VehiculoMarca VehiculoMarca { get; set; }
	}
}