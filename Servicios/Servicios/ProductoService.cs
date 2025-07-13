using AppModels;
using AppModels.DTOs;
using AppModels.Entities;
using AutoMapper;
using Data.Data;
using Data.Interfaces;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Servicios
{
	public class ProductoService : IProductoService
	{
		private readonly IMapper _mapper;
		private readonly IRepository<Producto> _productoRepository;
		private readonly IRepository<Categoria> _categoriaRepository;
		private readonly AppDbContext _context;

		public ProductoService(IMapper mapper, IRepository<Producto> productoRepository, IRepository<Categoria> categoriaRepository, AppDbContext context)
		{
			_mapper = mapper;
			_productoRepository = productoRepository;
			_categoriaRepository = categoriaRepository;
			_context = context;
		}

		public async Task<IEnumerable<ProductoDTO>> GetProductosAsync()
		{
			var productos = await _productoRepository.GetAllAsync(p => p.Categoria);
			return _mapper.Map<IEnumerable<ProductoDTO>>(productos);
		}

		public async Task<ProductoDTO?> GetProductoByIdAsync(int id)
		{
			var producto = await _productoRepository.GetByIdWithIncludesAsync(id, p => p.Categoria);
			return producto == null ? null : _mapper.Map<ProductoDTO>(producto);
		}

		public async Task<ProductoDTO> AddProductoAsync(CreateProductoDTO productoDTO)
		{
			var categoria = await _categoriaRepository.GetByIdAsync(productoDTO.IdCategoria);
			if (categoria == null) throw new Exception("La categoría ingresada no existe"); 

			var producto = _mapper.Map<Producto>(productoDTO);
			await _productoRepository.AddAsync(producto);
			await _context.SaveChangesAsync();
			return _mapper.Map<ProductoDTO>(producto);
		}

		public async Task<bool> UpdateProductoAsync(UpProductoDTO upProducto, int id)
		{
			var producto = await _productoRepository.GetByIdAsync(id);
			if (producto == null) return false;

			if (upProducto.IdCategoria.HasValue)
			{
				var categoria = await _categoriaRepository.GetByIdAsync(upProducto.IdCategoria.Value);
				if (categoria == null)
				{
					throw new Exception("La categoria ingresada no existe");
				}
			}

			_mapper.Map(upProducto, producto); // Actualiza las propiedades del objeto existente

			_productoRepository.Update(producto);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteProductoAsync(int id)
		{
			var producto = await _productoRepository.GetByIdAsync(id);
			if (producto == null) return false;

			_productoRepository.Delete(producto);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
