using AppModels.DTOs;
using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;
using Servicios.Servicios;

namespace PruebaTecnicaApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriasController : ControllerBase
	{
		private readonly ICategoriaService _service;

		public CategoriasController(ICategoriaService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetCategorias()
		{
			try
			{
				var categorias = await _service.GetCategoriasAsync();
				return Ok(categorias);
			}
			catch (Exception ex)
			{

				return BadRequest(new { mensaje = ex.Message });
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetCategoria(int id)
		{
			try
			{
				var categoria = await _service.GetCategoriaByIdAsync(id);
				if (categoria == null) return NotFound();

				return Ok(categoria);
			}
			catch (Exception ex)
			{

				return BadRequest(new { mensaje = ex.Message });
			}
		}

		[HttpPost]
		public async Task<IActionResult> AddCategoria([FromBody] CreateCategoriaDTO categoriaDTO)
		{
			try
			{
				if (!ModelState.IsValid) return BadRequest(ModelState);

				var categoria = await _service.AddCategoriaAsync(categoriaDTO);
				return CreatedAtAction(nameof(GetCategoria), new { id = categoria.Id }, categoria);
			}
			catch (Exception ex)
			{

				return BadRequest(new { mensaje = ex.Message });
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCategoria([FromBody] CreateCategoriaDTO categoriaDTO, int id)
		{
			try
			{
				if (!ModelState.IsValid) return BadRequest(ModelState);

				var categoria = await _service.UpdateCategoriaAsync(categoriaDTO, id);
				if (!categoria) return NotFound();
				return NoContent();
			}
			catch (Exception ex)
			{

				return BadRequest(new { mensaje = ex.Message });
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategoria(int id)
		{
			try
			{
				var categoria = await _service.DeleteCategoriaAsync(id);
				if (!categoria) return NotFound();
				return NoContent();
			}
			catch (Exception ex)
			{

				return BadRequest(new { mensaje = ex.Message });
			}
		}
	}
}
