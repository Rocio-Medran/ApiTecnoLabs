using AppModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
	public interface ICategoriaService
	{
		Task<IEnumerable<CategoriaDTO>> GetCategoriasAsync();
		Task<CategoriaDTO?> GetCategoriaByIdAsync(int id);
		Task<CategoriaDTO> AddCategoriaAsync(CreateCategoriaDTO categoriaDTO);
		Task<bool> UpdateCategoriaAsync(CreateCategoriaDTO categoriaDTO, int id);
		Task<bool> DeleteCategoriaAsync(int id);
	}
}
