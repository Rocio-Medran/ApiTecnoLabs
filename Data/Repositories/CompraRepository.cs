using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppModels.Entities;
using Data.Interfaces;
using Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class CompraRepository: ICompraRepository
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Compra> _repository;

        public CompraRepository(AppDbContext context, IRepository<Compra> repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<IEnumerable<Compra>> GetComprasAsync()
        {
            return await _context.Compras
                .AsNoTracking()
                .Include(c => c.Carrito)
                    .ThenInclude(cp => cp.CarritoProductos)
                        .ThenInclude(p => p.Producto)
                .ToListAsync();
        }

        public async Task<Compra?> GetCompraByIdAsync(int id)
        {
            return await _context.Compras
                .AsNoTracking()
                .Include(c => c.Carrito)
                    .ThenInclude(cp => cp.CarritoProductos)
                        .ThenInclude(p => p.Producto)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Compra> AddCompraAsync(Compra compra)
        {
            await _repository.AddAsync(compra);
            await _context.SaveChangesAsync();
            return compra;
        }

        public async Task<bool> UpdateCompraAsync(int id, Compra compra)
        {
            _context.Update(compra);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SaveCompraAsync()
        {
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
