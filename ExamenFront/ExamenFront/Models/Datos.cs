using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenFront.Models
{
    public class Datos
    {
        public List<Examen> Examenes { get; set; }

        public Datos(List<Examen> examenes)
        {
            Examenes = examenes;
        }
    }
}