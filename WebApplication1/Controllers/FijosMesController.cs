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
    [Route("api/FijosMes")]
    public class FijosMesController : Controller
    {
        private readonly ModeloContext _context;

        public FijosMesController(ModeloContext context)
        {
            _context = context;
        }

        // GET: api/FijosMes
        [HttpGet]
        public IEnumerable<FijosMes> GetFijosMes()
        {
            return _context.FijosMes;
        }

        // GET: api/FijosMes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFijosMes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<FijosMes> fijosMes = await _context.FijosMes
                                            .Include(x => x.Entidad)
                                            .Where(m => m.Fecha.Month == id && m.Fecha.Year == DateTime.Now.Year)
                                            .ToListAsync();

            if (fijosMes == null)
            {
                return NotFound();
            }

            return Ok(fijosMes);
        }

        // PUT: api/FijosMes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFijosMes([FromRoute] int id, [FromBody] FijosMes fijosMes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fijosMes.Id)
            {
                return BadRequest();
            }

            _context.Entry(fijosMes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FijosMesExists(id))
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

        // POST: api/FijosMes
        [HttpPost]
        public async Task<IActionResult> PostFijosMes([FromBody] FijosMes fijosMes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.FijosMes.Add(fijosMes);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetFijosMes", new { id = fijosMes.Id }, fijosMes);
            }
            catch(Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                return NotFound(e.InnerException.Message);
            }
        }

        // DELETE: api/FijosMes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFijosMes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fijosMes = await _context.FijosMes.SingleOrDefaultAsync(m => m.Id == id);
            if (fijosMes == null)
            {
                return NotFound();
            }

            _context.FijosMes.Remove(fijosMes);
            await _context.SaveChangesAsync();

            return Ok(fijosMes);
        }

        private bool FijosMesExists(int id)
        {
            return _context.FijosMes.Any(e => e.Id == id);
        }
    }
}