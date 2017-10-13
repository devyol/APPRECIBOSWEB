using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSA01.Clases
{
    public class reciboconsulta
    {
        public decimal idevento { get; set; }
        public decimal idrecibo { get; set; }
        public string nombre { get; set; }
        public decimal total { get; set; }
        public string estado { get; set; }
        public string evento_nombre { get; set; }
        public string pais { get; set; }
        public string opcion { get; set; }
        public string usuario { get; set; }
        public string detalle { get; set; }
        public string us_name { get; set; }
        public string fecha_creacion { get; set; }
        public string serie { get; set; }
        public string concepto { get; set; }
        public decimal? precio_concepto { get; set; }
        public decimal? cantidad { get; set; }
    }
}