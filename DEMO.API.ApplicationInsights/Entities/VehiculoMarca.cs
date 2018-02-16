using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DEMO.API.ApplicationInsights.Entities
{
	public class VehiculoMarca
	{
		public int Id { get; set; }
		[Index(IsUnique = true)]
		[Required]
		[MaxLength(100)]
		public string NombreMarca { get; set; }

		public virtual ICollection<VehiculoSubmarca> VehiculoSubmarcas { get; set; }
	}
}