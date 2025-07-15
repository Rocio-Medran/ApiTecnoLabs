using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppModels.Entities;
using Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class CarritoRepository: ICarritoRepository
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Carrito> _repository;

        public CarritoRepository(AppDbContext context, IRepository<Carrito> repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<IEnumerable<Carrito>> GetCarritosConProductosAsync()
        {
            return await _context.Carritos
                .AsNoTracking()
                .Include(c => c.CarritoProductos)
                    .ThenInclude(cp => cp.Producto)
                .ToListAsync();
        }

        public async Task<Carrito?> GetCarritoConProductosByIdAsync(int id)
        {
            return await _context.Carritos
                .AsNoTracking()
                .Include(c => c.CarritoProductos)
                    .ThenInclude(cp => cp.Producto)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
