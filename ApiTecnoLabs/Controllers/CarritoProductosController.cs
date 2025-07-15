using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;
using System;
using System.Threading.Tasks;
using AppModels.DTOs;

namespace ApiTecnoLabs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoProductosController : Controller
    {
        private readonly ICarritoProductoService _carritoProductoService;

        public CarritoProductosController(ICarritoProductoService carritoProductoService)
        {
            _carritoProductoService = carritoProductoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarritoProductos()
        {
            try
            {
                var carritoProductos = await _carritoProductoService.GetCarritoProductosAsync();
                return Ok(carritoProductos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCarritoProducto([FromBody] CreateCarritoProductoDTO carritoProductoDTO)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var carritoProducto = await _carritoProductoService.AddCarritoProductoAsync(carritoProductoDTO);
                return CreatedAtAction(nameof(GetCarritoProductos), new { id = carritoProducto.Id }, carritoProducto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarritoProducto(int id, [FromBody] UpCarritoProductoDTO carritoProductoDTO)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = await _carritoProductoService.UpdateCarritoProductoAsync(id, carritoProductoDTO);
                if (!result) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarritoProducto(int id)
        {
            try
            {
                var result = await _carritoProductoService.DeleteCarritoProductoAsync(id);
                if (!result) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
