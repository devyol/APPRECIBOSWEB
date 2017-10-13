using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RSA01.Clases;
using RSA01.Models.Estadistica;


namespace RSA01.Api
{
    public class ReciboEstadisticaController : ApiController
    {
        [HttpPost]
        public Response<List<estadisticarecibo>> estadisticasPais([FromBody]decimal arg)
        {            
            Response<List<estadisticarecibo>> obj = new Response<List<estadisticarecibo>>();
            GeneracionEstadisticas list = new GeneracionEstadisticas();
            return obj = list.estadisticaPais(arg);
        }

        [HttpPost]
        public Response<List<estadisticarecibo>> estadisticasConcepto([FromBody]decimal arg)
        {
            Response<List<estadisticarecibo>> obj = new Response<List<estadisticarecibo>>();
            GeneracionEstadisticas list = new GeneracionEstadisticas();
            return obj = list.estadisticaConcepto(arg);
        }

        [HttpPost]
        public Response<List<estadisticarecibo>> estadisticasTotales([FromBody]decimal arg)
        {
            Response<List<estadisticarecibo>> obj = new Response<List<estadisticarecibo>>();
            GeneracionEstadisticas list = new GeneracionEstadisticas();
            return obj = list.estadisticaTotales(arg);
        }
    }
}
