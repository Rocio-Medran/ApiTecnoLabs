using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppModels.DTOs;

namespace Servicios.Interfaces
{
    public interface ICarritoProductoService
    {
        Task<IEnumerable<CarritoProductoDTO>> GetCarritoProductosAsync();
        Task<CarritoProductoDTO> AddCarritoProductoAsync(CreateCarritoProductoDTO carritoProductoDTO);
        Task<bool> UpdateCarritoProductoAsync(int id, UpCarritoProductoDTO carritoProductoDTO);
        Task<bool> DeleteCarritoProductoAsync(int id);
    }
}
