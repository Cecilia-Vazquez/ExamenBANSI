using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExamenWebAPI.Models;

namespace ExamenWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TbIexamenController : ControllerBase
    {
        private readonly BdiExamenContext _context;

        public TbIexamenController(BdiExamenContext context)
        {
            _context = context;
        }

        // GET: api/TbIexamen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TbIexaman>>> ConsultarExamen()
        {
          if (_context.TbIexamen == null)
          {
              return NotFound();
          }
            return await _context.TbIexamen.ToListAsync();
        }
        // GET: api/TbIexamen1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TbIexaman>> GetTbIexaman(int id)
        {
            if (_context.TbIexamen == null)
            {
                return NotFound();
            }
            var tbIexaman = await _context.TbIexamen.FindAsync(id);

            if (tbIexaman == null)
            {
                return NotFound();
            }

            return tbIexaman;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AgregarExamen(TbIexaman tbIexaman)
        {
            try
            {
                if (_context.TbIexamen == null)
                {
                    return Problem("Entity set 'BdiExamenContext.TbIexamen' is null.", statusCode: StatusCodes.Status500InternalServerError);
                }

                _context.TbIexamen.Add(tbIexaman);
                await _context.SaveChangesAsync();

                return true; // Indicador de éxito
            }
            catch (Exception ex)
            {
               
                return Problem($"Descripcion: {ex.Message}", statusCode: StatusCodes.Status500InternalServerError);              

            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> ActualizarExamen(int id, TbIexaman tbIexaman)
        {
            if (id != tbIexaman.IdExamen)
            {
                return BadRequest(false);
            }

            _context.Entry(tbIexaman).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true; // Operación exitosa
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TbIexamanExists(id))
                {
                    return NotFound(false);
                }
                else
                {
                    return false; // Falló debido a concurrencia
                }
            }
            catch (Exception ex)
            {
                // Otra excepción no manejada
                return Problem($"Error: {ex.Message}", statusCode: StatusCodes.Status500InternalServerError);
            }
        }     
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> EliminarExamen(int id)
        {
            try
            {
                if (_context.TbIexamen == null)
                {
                    return NotFound(false);
                }
                var tbIexaman = await _context.TbIexamen.FindAsync(id);
                if (tbIexaman == null)
                {
                    return NotFound(false);
                }

                _context.TbIexamen.Remove(tbIexaman);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                return Problem($"Descripcion: {ex.Message}", statusCode: StatusCodes.Status500InternalServerError);

            }
        }
        private bool TbIexamanExists(int id)
        {
            return (_context.TbIexamen?.Any(e => e.IdExamen == id)).GetValueOrDefault();
        }
    }
}
