using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APIExamen;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ExamenFront.Models;
using System.Data.SqlClient;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Linq.Expressions;

namespace ExamenFront.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var result = await ClsExamen.ConsultarExamen("API", "", "");
            Datos datos = new Datos(JsonConvert.DeserializeObject<List<Models.Examen>>(result));
            List<Models.Examen> listaExamen = new List<Models.Examen>();
            foreach (Models.Examen exa in datos.Examenes)
            {
                listaExamen.Add(exa);
            }

            return View(listaExamen.AsEnumerable());
            // List<Models.Examen> listaExamen = new List<Models.Examen>();
            //return View(listaExamen.AsEnumerable());
        }
        [HttpPost]
        public async Task<ActionResult> Index(string nombre, string descripcion, string metodo)
        {
            var result = await ClsExamen.ConsultarExamen(metodo, nombre, descripcion);
            Datos datos = new Datos(JsonConvert.DeserializeObject<List<Models.Examen>>(result));
            List<Models.Examen> listaExamen = new List<Models.Examen>();
            foreach (Models.Examen exa in datos.Examenes)
            {
                listaExamen.Add(exa);
            }
            return View(listaExamen.AsEnumerable());
        }

        public ActionResult AgregarExamen()
        {

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AgregarExamen(string nombre, string descripcion, string metodo)
        {
            int id =  ClsExamen.existeExamen( nombre, descripcion);

            var result = (id == 0) ? await ClsExamen.AgregarExamen(metodo, 0, nombre, descripcion) : ViewBag.id = id;
            
            //var result = await ClsExamen.AgregarExamen(metodo, 0, nombre, descripcion);
            return Redirect("~/Home");
        }
        public async Task<ActionResult> ActualizarExamen(int id)
        {
            var result = await ClsExamen.ConsultarExamen(id);
            Models.Examen examen = new Models.Examen(JsonConvert.DeserializeObject<Models.Examen>(result));
            return View(examen);
        }
        [HttpPost]
        public async Task<ActionResult> ActualizarExamen([Bind(Include = "idexamen, nombre, descripcion")] Models.Examen Examen, string metodo)
        {

            var result = await ClsExamen.ActualizarExamen(metodo, Examen.idexamen, Examen.nombre, Examen.descripcion);
            return Redirect("~/Home");
        }

        public async Task<ActionResult> EliminarExamen(int id)
        {
            var result = await ClsExamen.ConsultarExamen(id);
            Models.Examen examen = new Models.Examen(JsonConvert.DeserializeObject<Models.Examen>(result));
            return View(examen);
        }
        [HttpPost]
        public async Task<ActionResult> EliminarExamen(int id, string metodo)
        {

            var result = await ClsExamen.EliminarExamen( id, metodo);
            return Redirect("~/Home");
        }
    }
}