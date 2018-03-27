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
    [Route("api/TiposMovimiento")]
    public class TiposMovimientoController : Controller
    {
        private readonly ModeloContext _context;

        public TiposMovimientoController(ModeloContext context)
        {
            _context = context;
        }

        // GET: api/TiposMovimiento
        [HttpGet]
        public IEnumerable<TipoMovimiento> GetTiposMovimiento()
        {
            return _context.TiposMovimiento;
        }

        // GET: api/TiposMovimiento/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTipoMovimiento([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tipoMovimiento = await _context.TiposMovimiento.SingleOrDefaultAsync(m => m.Id == id);

            if (tipoMovimiento == null)
            {
                return NotFound();
            }

            return Ok(tipoMovimiento);
        }

        // PUT: api/TiposMovimiento/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoMovimiento([FromRoute] int id, [FromBody] TipoMovimiento tipoMovimiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoMovimiento.Id)
            {
                return BadRequest();
            }

            _context.Entry(tipoMovimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoMovimientoExists(id))
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

        // POST: api/TiposMovimiento
        [HttpPost]
        public async Task<IActionResult> PostTipoMovimiento([FromBody] TipoMovimiento tipoMovimiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TiposMovimiento.Add(tipoMovimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoMovimiento", new { id = tipoMovimiento.Id }, tipoMovimiento);
        }

        // DELETE: api/TiposMovimiento/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoMovimiento([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tipoMovimiento = await _context.TiposMovimiento.SingleOrDefaultAsync(m => m.Id == id);
            if (tipoMovimiento == null)
            {
                return NotFound();
            }

            _context.TiposMovimiento.Remove(tipoMovimiento);
            await _context.SaveChangesAsync();

            return Ok(tipoMovimiento);
        }

        [HttpGet("GetMaxId", Name = "GetMaxId")]
        public async Task<IActionResult> getMaxId()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tipoMovimiento = await _context.TiposMovimiento.OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            if (tipoMovimiento == null)
            {
                return NotFound();
            }

            return Ok(tipoMovimiento);
        }

        private bool TipoMovimientoExists(int id)
        {
            return _context.TiposMovimiento.Any(e => e.Id == id);
        }
    }
}