using AppModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
	public interface IProductoService
	{
		Task<IEnumerable<ProductoDTO>> GetProductosAsync();
		Task<ProductoDTO?> GetProductoByIdAsync(int id);
		Task<ProductoDTO> AddProductoAsync(CreateProductoDTO productoDTO);
		Task<bool> UpdateProductoAsync(UpProductoDTO productoDTO, int id);
		Task<bool> DeleteProductoAsync(int id);
	}
}
