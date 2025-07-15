using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;

namespace ApiTecnoLabs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : Controller
    {
        private readonly ICompraService _compraService;

        public ComprasController(ICompraService compraService)
        {
            _compraService = compraService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompras()
        {
            var compras = await _compraService.GetComprasAsync();
            return Ok(compras);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCompraById(int id)
        {
            var compra = await _compraService.GetCompraByIdAsync(id);
            if (compra == null)
            {
                return NotFound();
            }
            return Ok(compra);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmarCompra(int carritoId)
        {
            try
            {
                var compra = await _compraService.ConfirmarCompraAsync(carritoId);
                return CreatedAtAction(nameof(GetCompraById), new { id = compra.Id }, compra);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> ModificarEstadoCompra(int id, [FromBody] string nuevoEstado)
        {
            if (string.IsNullOrEmpty(nuevoEstado))
            {
                return BadRequest("El nuevo estado no puede estar vacío.");
            }
            var resultado = await _compraService.ModificarEstadoCompraAsync(id, nuevoEstado);
            if (!resultado)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
