using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;
using System;
using System.Threading.Tasks;
using AppModels.DTOs;

namespace ApiTecnoLabs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritosController : Controller
    {
        private readonly ICarritoService _carritoService;

        public CarritosController(ICarritoService carritoService)
        {
            _carritoService = carritoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarritos()
        {
            try
            {
                var carritos = await _carritoService.GetCarritosAsync();
                return Ok(carritos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarrito(int id)
        {
            try
            {
                var carrito = await _carritoService.GetCarritoByIdAsync(id);
                if (carrito == null) return NotFound();
                return Ok(carrito);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCarrito([FromBody] CreateCarritoDTO carritoDTO)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var carrito = await _carritoService.AddCarritoAsync(carritoDTO);
                return CreatedAtAction(nameof(GetCarrito), new { id = carrito.Id }, carrito);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarrito(int id, [FromBody] UpCarritoDTO carritoDTO)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var updated = await _carritoService.UpdateCarritoAsync(id, carritoDTO);
                if (!updated) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrito(int id)
        {
            try
            {
                var deleted = await _carritoService.DeleteCarritoAsync(id);
                if (!deleted) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
