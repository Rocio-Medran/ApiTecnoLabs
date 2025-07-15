using AppModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface ICompraService
    {
        Task<IEnumerable<CompraDTO>> GetComprasAsync();
        Task<CompraDTO?> GetCompraByIdAsync(int id);
        Task<CompraDTO> ConfirmarCompraAsync(int carritoId);
        Task<bool> ModificarEstadoCompraAsync(int id, string nuevoEstado);
    }
}
