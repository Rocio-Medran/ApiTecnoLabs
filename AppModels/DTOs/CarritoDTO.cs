using AppModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModels.DTOs
{
    public class CarritoDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string MetodoPago { get; set; } = string.Empty;
        public bool Finalizado { get; set; } = false;
        public List<CarritoProductoDTO>? Productos { get; set; }
    }
}
