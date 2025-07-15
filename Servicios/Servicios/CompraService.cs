using AppModels.DTOs;
using AppModels.Entities;
using AutoMapper;
using Data.Data;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Servicios
{
    public class CompraService: ICompraService
    {
        private readonly IMapper _mapper;
        private readonly ICompraRepository _repository;
        private readonly ICarritoRepository _carritoRepository;
        private readonly IInventarioService _inventarioService;
        private readonly AppDbContext _context;

        public CompraService(IMapper mapper, ICompraRepository repository, ICarritoRepository carritoRepository, IInventarioService inventarioService, AppDbContext context)
        {
            _mapper = mapper;
            _repository = repository;
            _carritoRepository = carritoRepository;
            _inventarioService = inventarioService;
            _context = context;
        }

        public async Task<IEnumerable<CompraDTO>> GetComprasAsync()
        {
            var compras = await _repository.GetComprasAsync();
            return _mapper.Map<IEnumerable<CompraDTO>>(compras);
        }

        public async Task<CompraDTO?> GetCompraByIdAsync(int id)
        {
            var compra = await _repository.GetCompraByIdAsync(id);
            return compra == null ? null : _mapper.Map<CompraDTO>(compra);
        }

        public async Task<CompraDTO> ConfirmarCompraAsync(int carritoId)
        {
            var carrito = await _carritoRepository.GetCarritoConProductosByIdAsync(carritoId);
            if (carrito == null) throw new ArgumentException("Carrito no encontrado");

            carrito.Finalizado = true;
            _context.Entry(carrito).State = EntityState.Modified;

            decimal total = carrito.CarritoProductos.Sum(cp => cp.Producto.Precio * cp.Cantidad);

            var compra = new Compra
            {
                CarritoId = carritoId,
                Total = total,
                FechaCompra = DateTime.UtcNow,
                UsuarioId = carrito.UsuarioId,
                Estado = "Confirmada"
            };

            await _repository.AddCompraAsync(compra);

            await _inventarioService.DescontarStock(carrito.CarritoProductos);

            await _context.SaveChangesAsync();
            return _mapper.Map<CompraDTO>(compra);
        }

        public async Task<bool> ModificarEstadoCompraAsync(int id, string nuevoEstado)
        {
            var compra = await _repository.GetCompraByIdAsync(id);
            if (compra == null)
            {
                throw new ArgumentException("Compra no encontrada");
                return false;
            }

            compra.Estado = nuevoEstado;
            await _repository.UpdateCompraAsync(id, compra);
            await _repository.SaveCompraAsync();
            return true;
        }
    }
}
