using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Modelo;
using Microsoft.AspNetCore.Routing;

namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    [Route("api/Gastos")]
    public class GastosController : Controller
    {
        private readonly ModeloContext _context;

        public GastosController(ModeloContext context)
        {
            _context = context;
        }

        // GET: api/Gastos
        [HttpGet]
        public IEnumerable<Gasto> GetGastos()
        {
            return _context.Gastos.Include(x => x.Entidad).Include(x => x.Tipo).OrderByDescending(x => x.Fecha);
        }

        // GET: api/Gastos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGasto([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
      
            var gasto = await _context.Gastos
                        .Include(x => x.Entidad)
                        .Include(x => x.Tipo)
                        .SingleOrDefaultAsync(m => m.Id == id);

            if (gasto == null)
            {
                return NotFound();
            }

            return Ok(gasto);
        }

        // PUT: api/Gastos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGasto([FromRoute] int id, [FromBody] Gasto gasto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gasto.Id)
            {
                return BadRequest();
            }

            _context.Entry(gasto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GastoExists(id))
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

        // POST: api/Gastos
        [HttpPost]
        public async Task<IActionResult> PostGasto([FromBody] Gasto gasto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Gastos.Add(gasto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGasto", new { id = gasto.Id }, gasto);
        }

        // DELETE: api/Gastos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGasto([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gasto = await _context.Gastos.SingleOrDefaultAsync(m => m.Id == id);
            if (gasto == null)
            {
                return NotFound();
            }

            _context.Gastos.Remove(gasto);
            await _context.SaveChangesAsync();

            return Ok(gasto);
        }

        private bool GastoExists(int id)
        {
            return _context.Gastos.Any(e => e.Id == id);
        }

        //GET: api/GastosMes/5
        [HttpGet("GastosMes/{param}", Name = "GastosMes")]
        public async Task<IActionResult> GetGastosMes([FromRoute] string param)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fec;
            var isFecha = DateTime.TryParseExact(param, "yyyy-M", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out fec);

            if (isFecha)
            {
                IEnumerable<Gasto> lista = await _context.Gastos
                                                    .Include(x => x.Entidad)
                                                    .Include(x => x.Tipo)
                                                    .Where(x => x.Fecha.Month == fec.Month)
                                                    .Where(x => x.Fecha.Year == fec.Year)
                                                    .Where(x => x.Activo == 1)
                                                    .Where(x => x.idTipoMovimiento == 1)
                                                    .ToListAsync();
                return Ok(lista);
            }

            return BadRequest();
        }

        [HttpGet("MesSiguiente", Name = "MesSiguiente")]
        public async Task<IActionResult> getMesSiguiente()
        {
            var mesActual = DateTime.Now.Month+1;
            var anhoActual = DateTime.Now.Year;


            IEnumerable<Gasto> lista = await _context.Gastos
                                    .Include(x => x.Entidad)
                                    .Include(x => x.Tipo)
                                    .Where(x => x.Fecha.Month == mesActual)
                                    .Where(x => x.Fecha.Year == anhoActual)
                                    .Where(x => x.idTipoMovimiento == 1)
                                    .ToListAsync();
            return Ok(lista);
        }

        ///<summary>
        /// Esta funcion devuelve un listado de los gastos totales por entidad agrupados por meses
        /// Formato: array 2 dimensiones. Ejemplo (meses gastos agrupados por entidad para meses de febrero y marzo)
        /// que son los meses que tengo presentes en BD
        /// [
        ///     {
        ///         "mes": 2,
        ///         "elementos": [
        ///             {
        ///                 "total": 27.14,
        ///                 "entidad": "Internet",
        ///                 "idEntidad": 1
        ///             },
        ///             {
        ///                "total": 390,
        ///                "entidad": "Metálico",
        ///                "idEntidad": 3
        ///             },
        ///             .
        ///             .
        ///             .
        ///         ]
        ///     },
        ///     {
        ///         "mes": 3,
        ///         "elementos": [
        ///             {
        ///                "total": 999,
        ///                "entidad": "Internet",
        ///                "idEntidad": 1
        ///             }
        ///        ]
        ///    }       
        ///</summary>
        //GET: api/ResumenMes
        [HttpGet("ResumenMes/{param:int?}", Name = "ResumenMes")]
        public async Task<IActionResult> GetResumenMes([FromRoute]int? param = null)
        {
            try
            {
                if (param == null)
                    param = DateTime.Now.Year;

                var b = await _context.Gastos
                    // .Where(x => x.idTipoMovimiento == 1 && x.Fecha.Year == param)
                    .Where(x => x.Fecha.Year == param && x.idEntidad != 12)
                    .GroupBy(x => x.Fecha.Month,
                        (mes, gastos) =>
                        new
                        {
                            Mes = mes,
                            Elementos = gastos.GroupBy(x => x.idEntidad)
                                              .Select(g => new
                                              {
                                                  Total = g.Sum(x => x.Importe),                
                                                  Entidad = g.Select(z => z.Entidad.nombreEntidad).Distinct().SingleOrDefault(),
                                                  idEntidad = g.Select(z => z.idEntidad).Distinct().SingleOrDefault()
                                              })
                                              .OrderBy(x => x.idEntidad),
                            Total = gastos.Sum(x => x.Importe)
                        }
                    )
                    .OrderByDescending(x => x.Mes)
                    .ToListAsync();

                return Ok(b);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //GET: api/Gastos/fijosMes/5
        [HttpGet("FijosMes/{param}", Name = "FijosMes")]
        public async Task<IActionResult> GetGastosFijosMes([FromRoute] string param)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime fec;
            var isFecha = DateTime.TryParseExact(param, "yy-MM-dd", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out fec);

            if (isFecha)
            {
                IEnumerable<Gasto> lista = await _context.Gastos
                                                    .Include(x => x.Entidad)
                                                    .Include(x => x.Tipo)
                                                    .Where(x => x.Fecha.Month == fec.Month)
                                                    .Where(x => x.Fecha.Year == fec.Year)
                                                    .Where(x => x.Fijo == 1)
                                                    .ToListAsync();
                return Ok(lista);
            }

            return BadRequest();
        }

        [HttpGet("IngresosMes/{param:int?}", Name = "IngresosMes")]
        public async Task<IActionResult> GetIngresosMes([FromRoute] int? param = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                if(param == null)
                {
                    param = DateTime.Now.Month;
                }

                var lista = await _context.Gastos
                    .Where(x => x.idTipoMovimiento == 2)
                    .Where(x => x.Fecha.Month == param)
                    //.Where(x => x.Fecha.Year == anho)
                    .ToListAsync();

                return Ok(lista);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //GET: api/ResumenMes
        [HttpGet("ResultanteMes/{param:int?}", Name = "ResultanteMes")]
        public async Task<IActionResult> GetResultanteMes([FromRoute]int? param = null)
        {
            try
            {
                if (param == null)
                    param = DateTime.Now.Year;

                var b = await _context.Gastos
                    .Where(x => x.Fecha.Year == param)
                    .GroupBy(x => x.Fecha.Month,
                        (mes, movimientos) =>
                        new
                        {
                            Mes = mes,
                            Elementos = movimientos
                                      .GroupBy(x => x.idTipoMovimiento)
                                      .Select(g => new
                                      {
                                        Tipo = g.Select(x => x.Tipo.tipoMovimiento).Distinct().SingleOrDefault(),
                                        Valor =  Math.Abs(g.Sum(x => x.Importe))
                                      })
                                      .OrderByDescending(x => x.Tipo),
                            Resultante = (movimientos.Where(x => x.idTipoMovimiento == 2).Sum(x => x.Importe) * -1) - movimientos.Where(x => x.idTipoMovimiento == 1).Sum(x => x.Importe)
                        }
                    )
                    .OrderByDescending(x => x.Mes)
                    .ToListAsync();

                return Ok(b);

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        [HttpGet("ResultanteMesGrafica/{param:int?}", Name = "ResultanteMesGrafica")]
        public async Task<IActionResult> getResultanteMesGrafica([FromRoute]int? param = null)
        {
            try
            {
                if (param == null)
                    param = DateTime.Now.Year;

                var b = await _context.Gastos
                    .Where(x => x.Fecha.Year == param)
                    .GroupBy(x => x.Tipo.tipoMovimiento,
                        (tipoMovimiento, movimientos) =>
                        new
                        {
                            name = tipoMovimiento,
                            series = movimientos
                                      .GroupBy(x => x.Fecha.Month)
                                      .Select(g => new
                                      {
                                          name = numMesToString(g.Select(x => x.Fecha.Month).Distinct().SingleOrDefault()),
                                          value = Math.Abs(g.Sum(x => x.Importe)),
                                          zz = g.Select(x => x.Fecha.Month).Distinct().SingleOrDefault()
                                      })
                                      .OrderBy(x => x.zz)
                        }
                    )
                    .ToListAsync();

                return Ok(b);

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        [HttpGet("{getMaxId}")]
        public async Task<IActionResult> getMaxId()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gastos = await _context.Gastos
                                       .Include(x => x.Entidad)
                                       .Include(x => x.Tipo)
                                       .OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            if (gastos == null)
            {
                return NotFound();
            }

            return Ok(gastos);
        }


        public string numMesToString(int param)
        {
            switch (param)
            {
                case 1:
                    return "Xaneiro";
                case 2:
                    return "Febreiro";
                case 3:
                    return "Marzo";
                case 4:
                    return "Abril";
                case 5:
                    return "Maio";
                case 6:
                    return "Xuño";
                case 7:
                    return "Xullo";
                case 8:
                    return "Agosto";
                case 9:
                    return "Setembro";
                case 10:
                    return "Outubro";
                case 11:
                    return "Novembro";
                case 12:
                    return "Decembro";
                default:
                    return "";
            }
        }

    }
}