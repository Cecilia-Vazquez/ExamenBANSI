using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenFront.Models
{
    public class Examen
    {

        public int idexamen { set; get; }
        public string nombre { set; get; }
        public string descripcion { set; get; }
        public Examen(int id, string nom, string desc)
        {
            idexamen = id;
            nombre = nom;
            descripcion = desc;


        }
        public Examen()
        {

        }

        public Examen(Examen examen)
        {
            this.idexamen = examen.idexamen;
            this.descripcion = examen.descripcion;
            this.nombre = examen.nombre;
        }
    }
}