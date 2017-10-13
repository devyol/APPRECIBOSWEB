using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSA01.Clases
{
    public class evento
    {
        public decimal idevento { get; set; }
        public string nombre { get; set; }
        public string estado_registro { get; set; }
        public string usuario_creacion { get; set; }
        public string fecha_creacion { get; set; }
        public string usuario_modificacion { get; set; }
        public string fecha_modificacion { get; set; }
    }
}