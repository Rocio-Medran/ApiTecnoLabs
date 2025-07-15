using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModels.DTOs
{
    public class CreateCarritoDTO
    {
        public int UsuarioId { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
    }
}
