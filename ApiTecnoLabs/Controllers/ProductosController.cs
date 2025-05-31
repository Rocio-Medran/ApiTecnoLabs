using AppModels.DTOs;
using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;

namespace PruebaTecnicaApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductosController : ControllerBase
	{
		private readonly IProductoService _service;

		public ProductosController(IProductoService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetProductos()
		{
			try
			{
				var productos = await _service.GetProductosAsync();
				return Ok(productos);
			}
			catch (Exception ex)
			{

				return BadRequest(new { mensaje = ex.Message });
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetProducto(int id)
		{
			try
			{
				var producto = await _service.GetProductoByIdAsync(id);
				if (producto == null) return NotFound();

				return Ok(producto);
			}
			catch (Exception ex)
			{

				return BadRequest(new { mensaje = ex.Message });
			}
		}

		[HttpPost]
		public async Task<IActionResult> AddProducto([FromBody] CreateProductoDTO productoDTO)
		{
			try
			{
				if (!ModelState.IsValid) return BadRequest(ModelState);

				var producto = await _service.AddProductoAsync(productoDTO);
				return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
			}
			catch (Exception ex)
			{

				return BadRequest(new { mensaje = ex.Message });
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateProducto([FromBody] UpProductoDTO productoDTO, int id)
		{
			try
			{
				if (!ModelState.IsValid) return BadRequest(ModelState);

				var producto = await _service.UpdateProductoAsync(productoDTO, id);
				if (!producto) return NotFound();
				return NoContent();
			}
			catch (Exception ex)
			{

				return BadRequest(new { mensaje = ex.Message });
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProducto(int id)
		{
			try
			{
				var producto = await _service.DeleteProductoAsync(id);
				if (!producto) return NotFound();
				return NoContent();
			}
			catch (Exception ex)
			{

				return BadRequest(new { mensaje = ex.Message });
			}
		}
	}
}
