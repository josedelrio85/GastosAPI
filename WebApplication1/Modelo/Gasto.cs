using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Modelo
{
    public class Gasto : IMovimiento
    {
        public int Id { get; set; }
        public double Importe { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        public int? Activo { get; set; }
        public bool IsActivo
        {
            get
            {
                return (Activo == 1) ? true : false;
            }
        }

        public int? Fijo { get; set; }
        public bool IsFijo
        {
            get
            {
                return (Fijo == 1) ? true : false;
            }
        }

        public int idTipoMovimiento { get; set; }
        public virtual TipoMovimiento Tipo { get; set; }

        public int idEntidad { get; set; }
        public virtual Entidad Entidad { get; set; }

        public string Descripcion { get; set; }

    }
}
