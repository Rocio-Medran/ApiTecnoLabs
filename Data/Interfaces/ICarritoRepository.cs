using AppModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ICarritoRepository
    {
        Task<IEnumerable<Carrito>> GetCarritosConProductosAsync();
        Task<Carrito?> GetCarritoConProductosByIdAsync(int id);
    }
}
