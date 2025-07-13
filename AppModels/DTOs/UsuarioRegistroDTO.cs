using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModels.DTOs
{
	public class UsuarioRegistroDTO
	{
		[Required]
		[MaxLength(150)]
		public string Nombre { get; set; }
		[Required(ErrorMessage = "El email es obligatorio.")]
		[EmailAddress(ErrorMessage = "El formato del email no es válido.")]
		[MaxLength(150)]
		public string Email { get; set; }
		[Required(ErrorMessage = "La contraseña es obligatoria.")]
		[MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
		public string Password { get; set; }
	}
}
