using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModels.DTOs
{
	public class ProductoDTO
	{
		public int Id { get; set; }
		public string Nombre {  get; set; }
		public decimal Precio { get; set; }
		public int Stock { get; set; }
		public int IdCategoria { get; set; }
		public string NombreCategoria { get; set; }
	}
}
