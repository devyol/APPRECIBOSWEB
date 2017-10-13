using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RSA01.Data;

namespace RSA01.Clases
{
    public class recibo
    {
        public decimal? idrecibo { get; set; }
        public string nombre { get; set; }
        public decimal cantidad { get; set; }
        public decimal valor { get; set; }
        public decimal total { get; set; }
        public string detalle { get; set; }
        public decimal evento { get; set; }
        public decimal? opcion { get; set; }
        public string pais { get; set; }
        public string estado_registro { get; set; }
        public string usuario_creacion { get; set; }
        public string fecha_creacion { get; set; }
        public string usuario_modificacion { get; set; }
        public string fecha_modificacion { get; set; }

        public pais pais_ { get; set; }
        public eventoopcion opcion_ { get; set; }
    }
}