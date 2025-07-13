using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModels.Entities
{
	public class Usuario
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[MaxLength(150)]
		public string Nombre { get; set; }
		[Required]
		[EmailAddress]
		[MaxLength(150)]
		public string Email { get; set; }
		[Required]
		public string PasswordHash { get; set; } = string.Empty;
		[Required]
		[MaxLength(30)]
		public string Rol { get; set; } = "Cliente";
		public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
	}
}
