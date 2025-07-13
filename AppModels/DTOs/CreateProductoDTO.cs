using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModels.DTOs
{
	public class CreateProductoDTO
	{
		[StringLength(100)]
		public string Nombre { get; set; }

		[Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a cero")]
		public decimal Precio { get; set; }

		[Range(0, 1000000, ErrorMessage = "El stock no puede ser negativo")]
		public int Stock { get; set; }

		[Range(1, int.MaxValue, ErrorMessage = "El IdCategoria no puede ser negativo")]
		public int IdCategoria {  get; set; }
	}
}
