using AppModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModels.DTOs
{
    public class CompraDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int CarritoId { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaCompra { get; set; } = DateTime.Now;
        public string Estado { get; set; } = "Pendiente";
        public List<CarritoProductoDTO> Productos { get; set; }
    }
}
