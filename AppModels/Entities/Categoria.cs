using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModels.Entities
{
	public class Categoria
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[MaxLength(100)]
		public string Nombre { get; set; }
		public ICollection<Producto> Productos { get; set; }
	}
}
