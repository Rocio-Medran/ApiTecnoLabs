using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModels.DTOs
{
	public class CreateCategoriaDTO
	{
		[StringLength(100)]
		public string Nombre { get; set; }
	}
}
