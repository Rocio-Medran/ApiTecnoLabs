using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios.Interfaces;
using AppModels.DTOs;
using Data.Interfaces;
using AppModels.Entities;
using AutoMapper;
using Data.Data;

namespace Servicios.Servicios
{
    public class CarritoProductoService: ICarritoProductoService
    {
        private readonly IRepository<CarritoProducto> _repository;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CarritoProductoService(IRepository<CarritoProducto> repository, AppDbContext context, IMapper mapper)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CarritoProductoDTO>> GetCarritoProductosAsync()
        {
            var carritoProductos = await _repository.GetAllAsync(cp => cp.Producto);
            return _mapper.Map<IEnumerable<CarritoProductoDTO>>(carritoProductos);
        }

        public async Task<CarritoProductoDTO> AddCarritoProductoAsync(CreateCarritoProductoDTO carritoProductoDTO)
        {
            var carritoProducto = _mapper.Map<CarritoProducto>(carritoProductoDTO);
            var producto = await _context.Productos.FindAsync(carritoProductoDTO.ProductoId);
            if (producto == null)
            {
                throw new ArgumentException("Producto no encontrado");
            }
            carritoProducto.PrecioUnitario = producto.Precio;
            

            await _repository.AddAsync(carritoProducto);
            await _context.SaveChangesAsync();
            return _mapper.Map<CarritoProductoDTO>(carritoProducto);
        }

        public async Task<bool> UpdateCarritoProductoAsync(int id, UpCarritoProductoDTO carritoProductoDTO)
        {
            var carritoProducto = await _repository.GetByIdAsync(id);
            if (carritoProducto == null) return false;

            _mapper.Map(carritoProductoDTO, carritoProducto);
            _repository.Update(carritoProducto);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCarritoProductoAsync(int id)
        {
            var carritoProducto = await _repository.GetByIdAsync(id);
            if (carritoProducto == null) return false;
            _repository.Delete(carritoProducto);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
