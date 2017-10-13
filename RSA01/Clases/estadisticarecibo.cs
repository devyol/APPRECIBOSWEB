using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSA01.Clases
{
    public class estadisticarecibo
    {
        public decimal? total { get; set; }
        public string pais { get; set; }
        public string concepto { get; set; }
        public string fecha_generacion { get; set; }
        public decimal idrecibo { get; set; }
        public string nombre { get; set; }
        public decimal? cantidad { get; set; }
        public decimal? precio { get; set; }
        public string estado { get; set; }
        public string usuario_creacion { get; set; }
        public string usuario_modificacion { get; set; }
        public string evento_nombre { get; set; }
        public string fecha_creacion { get; set; }
        public string fecha_modificacion { get; set; }
        public string fecha { get; set; }
        public decimal? total_recibos { get; set; }
        public decimal? total_cobrado { get; set; }
    }
}