using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RSA01.Clases;
using RSA01.Models.Recibo;

namespace RSA01.Api
{
    public class ReciboController : ApiController
    {
        [HttpPost]
        public Response<List<evento>> listarEventos()
        {
            Response<List<evento>> obj = new Response<List<evento>>();
            GestionRecibo list = new GestionRecibo();
            return list.obtenerEventosActivos();
        
        }

        [HttpPost]
        public Response<List<reciboconsulta>> listarRecibos([FromBody]decimal eve)
        {
            Response<List<reciboconsulta>> obj = new Response<List<reciboconsulta>>();
            GestionRecibo list = new GestionRecibo();
            return obj = list.obtenerRecibos(eve);
        }

        [HttpPost]
        public Response<List<eventoopcion>> listarOpcionesEvento([FromBody]decimal arg)
        {
            Response<List<eventoopcion>> obj = new Response<List<eventoopcion>>();
            GestionRecibo list = new GestionRecibo();
            return obj = list.obtenerOpcionesEvento(arg);
        }

        [HttpPost]
        public Response<List<pais>> listarPaises()
        {
            Response<List<pais>> obj = new Response<List<pais>>();
            GestionRecibo list = new GestionRecibo();
            return list.obtenerListaPaises();
        }

        [HttpPost]
        public Response<recibo> obtenerRecibo([FromBody]decimal num)
        {
            Response<recibo> obj = new Response<recibo>();
            GestionRecibo list = new GestionRecibo();
            return list.obtenerRecibo(num);
        }

        [HttpPost]
        public Response<recibo> GuardarRecibo([FromBody] recibo arg)
        {
            Response<recibo> obj = new Response<recibo>();
            GestionRecibo trans = new GestionRecibo();

            if (arg.idrecibo == null || arg.idrecibo.Equals(0))
            {
                return obj = trans.nuevoRecibo(arg);
            }
            else
            {
                return obj = trans.actualizarRecibo(arg);
            }

        }

        [HttpPost]
        public Response<recibo> anularRecibo([FromBody] decimal arg)
        {
            Response<recibo> obj = new Response<recibo>();
            GestionRecibo anul = new GestionRecibo();
            return obj = anul.anularRecibo(arg);
        }

    }
}
