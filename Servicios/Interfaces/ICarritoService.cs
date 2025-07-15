using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppModels.DTOs;

namespace Servicios.Interfaces
{
    public interface ICarritoService
    {
        Task <IEnumerable<CarritoDTO>> GetCarritosAsync();
        Task<CarritoDTO?> GetCarritoByIdAsync(int id);
        Task<CarritoDTO> AddCarritoAsync(CreateCarritoDTO carritoDTO);
        Task<bool> UpdateCarritoAsync(int id, UpCarritoDTO carritoDTO);
        Task<bool> DeleteCarritoAsync(int id);
    }
}
