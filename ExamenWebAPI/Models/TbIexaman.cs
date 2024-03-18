using System;
using System.Collections.Generic;

namespace ExamenWebAPI.Models
{
    public partial class TbIexaman
    {
        public int IdExamen { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
}
