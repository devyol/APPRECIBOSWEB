using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSA01.Clases
{
    public class eventoopcion
    {

        public decimal opcion { get; set; }
        public decimal evento { get; set; }
        public string descripcion { get; set; }
        public decimal? precio { get; set; }
        public string estado_registro { get; set; }
        public string usuario_creacion { get; set; }
        public string fecha_creacion { get; set; }
        public string usuario_modificacion { get; set; }
        public string fecha_modificacion { get; set; }
    }
}