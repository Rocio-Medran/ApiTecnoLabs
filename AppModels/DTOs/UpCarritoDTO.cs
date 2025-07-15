using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModels.DTOs
{
    public class UpCarritoDTO
    {
        public string MetodoPago { get; set; } = string.Empty;
        public bool Finalizado { get; set; }
    }
}
