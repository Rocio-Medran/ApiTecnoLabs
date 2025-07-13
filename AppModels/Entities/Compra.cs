using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModels.Entities
{
    public class Compra
    {
        [Key]
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int CarritoId { get; set; }
        public Carrito Carrito { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaCompra { get; set; } = DateTime.Now;
        public string Estado { get; set; } = "Pendiente";
    }
}
