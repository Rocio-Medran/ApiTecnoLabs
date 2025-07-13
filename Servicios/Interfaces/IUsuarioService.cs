using AppModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
	public interface IUsuarioService
	{
		Task<bool> RegistrarAsync(UsuarioRegistroDTO dto);
		Task<string?> LoginAsync(UsuarioLoginDTO dto);
		Task<UsuarioDTO?> GetByIdAsync(int id);
	}
}
