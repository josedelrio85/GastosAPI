using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Modelo
{
    public class FijosMes
    {
        
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public double Importe { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        public int idEntidad { get; set; }
        public virtual Entidad Entidad { get; set; }
    }
}
