using AppModels.DTOs;
using AppModels.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;

namespace ApiTecnoLabs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsuariosController : Controller
	{
		private readonly IUsuarioService _usuarioService;

		public UsuariosController(IUsuarioService usuarioService)
		{
			_usuarioService = usuarioService;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetUsuario(int id)
		{
			try
			{
				var usuario = await _usuarioService.GetByIdAsync(id);
				if (usuario == null) return NotFound();

				return Ok(usuario);
			}
			catch (Exception ex)
			{

				return BadRequest(new { mensaje = ex.Message });
			}
		}

		[HttpPost("registrar")]
		public async Task<IActionResult> Registrar([FromBody] UsuarioRegistroDTO usuarioDto)
		{
			try
			{
				if (!ModelState.IsValid) return BadRequest(ModelState);

				var usuarioCreado = await _usuarioService.RegistrarAsync(usuarioDto);
				return Ok(usuarioCreado);
			}
			catch (Exception ex)
			{
				return BadRequest(new { mensaje = ex.Message });
			}
			
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO usuarioDto)
		{
			var token = await _usuarioService.LoginAsync(usuarioDto);
			if (token == null)
				return Unauthorized("Credenciales inválidas.");

			return Ok(new { token });
		}
	}
}
