using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModels.Entities
{
    public class CarritoProducto
    {
        [Key]
        public int Id { get; set; }

        public int CarritoId { get; set; }

        public Carrito Carrito { get; set; }

        [ForeignKey(nameof(Producto.Id))]
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
