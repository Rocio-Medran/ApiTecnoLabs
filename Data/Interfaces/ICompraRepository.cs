using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppModels.Entities;

namespace Data.Interfaces
{
    public interface ICompraRepository
    {
        Task<IEnumerable<Compra>> GetComprasAsync();
        Task<Compra?> GetCompraByIdAsync(int id);
        Task<Compra> AddCompraAsync(Compra compra);
        Task<bool> UpdateCompraAsync(int id, Compra compra);
        Task<bool> SaveCompraAsync();
    }
}
