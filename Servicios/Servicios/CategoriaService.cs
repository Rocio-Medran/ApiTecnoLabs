using AutoMapper;
using Servicios.Interfaces;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using AppModels.DTOs;
using AppModels.Entities;
using Data.Data;

namespace Servicios.Servicios
{
	public class CategoriaService : ICategoriaService
	{
		private readonly IMapper _mapper;
		private readonly IRepository<Categoria> _repository;
		private readonly AppDbContext _context;

		public CategoriaService(IMapper mapper, IRepository<Categoria> repository, AppDbContext context)
		{
			_mapper = mapper;
			_repository = repository;
			_context = context;
		}

		public async Task<IEnumerable<CategoriaDTO>> GetCategoriasAsync()
		{
			var categorias = await _repository.GetAllAsync();
			return _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);
		}

		public async Task<CategoriaDTO?> GetCategoriaByIdAsync(int id)
		{
			var categoria = await _repository.GetByIdAsync(id);
			return categoria == null ? null : _mapper.Map<CategoriaDTO>(categoria);
		}

		public async Task<CategoriaDTO> AddCategoriaAsync(CreateCategoriaDTO categoriaDTO)
		{
			var categoria = _mapper.Map<Categoria>(categoriaDTO);
			await _repository.AddAsync(categoria);
			await _context.SaveChangesAsync();
			return _mapper.Map<CategoriaDTO>(categoria);
		}

		public async Task<bool> UpdateCategoriaAsync(CreateCategoriaDTO categoriaDTO, int id)
		{
			var categoria = await _repository.GetByIdAsync(id);
			if (categoria == null) return false;

			_mapper.Map(categoriaDTO, categoria);
			_repository.Update(categoria);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteCategoriaAsync(int id)
		{
			var categoria = await _repository.FirstOrDefaultAsync(c => c.Id == id,
				c => c.Productos);
			if (categoria == null) return false;

			if (categoria.Productos.Any())
			{
				throw new Exception("No se puede eliminar esta categoria porque tiene productos asociados");
			}

			_repository.Delete(categoria);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
