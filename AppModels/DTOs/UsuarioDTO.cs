using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModels.DTOs
{
	public class UsuarioDTO
	{
		public string Nombre { get; set; }
		public string Email { get; set; }
		public string Rol { get; set; } = "Cliente";
	}
}
