using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModels.Entities
{
    public class Carrito
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string MetodoPago { get; set; } = string.Empty;
        public bool Finalizado { get; set; } = false;
        public ICollection<CarritoProducto> CarritoProductos { get; set; }
    }
}
