using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModels.Entities
{
	public class Producto
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[MaxLength(100)]
		public string Nombre { get; set; }
		[Required]
		[Precision(18, 2)]
		public decimal Precio { get; set; }
		[Required]
		public int Stock { get; set; }
		[ForeignKey(nameof(Categoria.Id))]
		public int IdCategoria { get; set; }
		public Categoria Categoria { get; set; }
	}
}
