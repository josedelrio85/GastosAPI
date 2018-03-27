using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Modelo;

namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    [Route("api/Entidades")]
    public class EntidadesController : Controller
    {
        private readonly ModeloContext _context;

        public EntidadesController(ModeloContext context)
        {
            _context = context;
        }

        // GET: api/Entidades
        [HttpGet]
        public IEnumerable<Entidad> GetEntidades()
        {
            return _context.Entidades;
        }

        // GET: api/Entidades/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEntidad([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entidad = await _context.Entidades.SingleOrDefaultAsync(m => m.Id == id);

            if (entidad == null)
            {
                return NotFound();
            }

            return Ok(entidad);
        }

        // PUT: api/Entidades/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntidad([FromRoute] int id, [FromBody] Entidad entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != entidad.Id)
            {
                return BadRequest();
            }

            _context.Entry(entidad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntidadExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Entidades
        [HttpPost]
        public async Task<IActionResult> PostEntidad([FromBody] Entidad entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entidades.Add(entidad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntidad", new { id = entidad.Id }, entidad);
        }

        // DELETE: api/Entidades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntidad([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entidad = await _context.Entidades.SingleOrDefaultAsync(m => m.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }

            _context.Entidades.Remove(entidad);
            await _context.SaveChangesAsync();

            return Ok(entidad);
        }

        [HttpGet("{getMaxId}")]
        public async Task<IActionResult> getMaxId()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entidad = await _context.Entidades.OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            if (entidad == null)
            {
                return NotFound();
            }

            return Ok(entidad);
        }

        private bool EntidadExists(int id)
        {
            return _context.Entidades.Any(e => e.Id == id);
        }
    }
}