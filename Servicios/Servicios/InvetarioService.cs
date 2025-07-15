using Data.Data;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppModels.Entities;

namespace Servicios.Servicios
{
    public class InvetarioService: IInventarioService
    {
        private readonly AppDbContext _context;

        public InvetarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task DescontarStock(IEnumerable<CarritoProducto> carritoProductos)
        {
            foreach (var item in carritoProductos)
            {
                if (item.Producto.Stock < item.Cantidad)
                {
                    throw new InvalidOperationException($"No hay suficiente stock para el producto {item.Producto.Nombre}");
                }

                item.Producto.Stock -= item.Cantidad;
                _context.Productos.Update(item.Producto);
            }

            await _context.SaveChangesAsync();
        }
    }
}
