using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Modelo
{
    interface IMovimiento
    {
        int Id { get; set; }
        double Importe { get; set; }
        DateTime Fecha { get; set; }
        TipoMovimiento Tipo { get; set; }
        Entidad Entidad { get; set; }

    }
}
