using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using RSA01.Data;
using RSA01.Clases;

namespace RSA01.Models.Estadistica
{
    public class GeneracionEstadisticas
    {
        public decimal idEvento { get; set; }

        #region Constantes

        private const string _sqlEstadisticaPais = @"select pa.descripcion pais, sum(re.cantidad) total, 
                                                        to_char(sysdate,'dd/mm/yyyy hh24:mi:ss') fecha_generacion
                                                        from sag01_recibo re, sag01_pais pa
                                                        where re.pais = pa.pais
                                                        and re.estado_registro = 'A'
                                                        and re.evento = :evento
                                                        group by pa.descripcion, pa.pais
                                                        order by pa.pais";

        private const string _sqlEstadisticaConcepto = @"select op.descripcion concepto, sum(re.cantidad) total,sum(re.total) total_cobrado,
                                                        to_char(sysdate,'dd/mm/yyyy hh24:mi:ss') fecha_generacion
                                                        from sag01_recibo re, sag01_evento_opcion op
                                                        where re.evento = op.evento
                                                        and re.opcion = op.opcion
                                                        and re.estado_registro = 'A'
                                                        and re.evento = :evento
                                                        group by op.descripcion, op.opcion
                                                        order by op.opcion";

        private const string _sqlEstadisticaTotales = @"select fecha, estado, total_recibos, total_cobrado
                                                        from(
                                                        select to_char(re.fecha_creacion, 'dd/mm/yyyy') fecha,
                                                        es.descripcion estado, count(re.recibo) total_recibos, sum(total) total_cobrado
                                                        from sag01_recibo re, sag01_estado es
                                                        where re.estado_registro = es.estado
                                                        and re.evento = :evento
                                                        group by to_char(re.fecha_creacion, 'dd/mm/yyyy'),
                                                        es.descripcion)
                                                        order by to_date(fecha,'dd/mm/yyyy')";

        private const string _sqlObtenerInfoDetallada = @"select ev.nombre evento_nombre, to_char(re.fecha_creacion,'dd/mm/yyyy')fecha_creacion, uss.serie,
                                                        re.recibo idrecibo, re.nombre, pa.descripcion pais, op.descripcion concepto,
                                                        re.valor precio_concepto, re.cantidad, re.total, es.descripcion estado, usc.nombre usuario
                                                        from 
                                                        sag01_recibo re, 
                                                        sag01_evento ev, 
                                                        sag01_evento_opcion op,
                                                        sag01_usuario_serie uss,
                                                        sag01_pais pa, 
                                                        sag01_usuario usc, 
                                                        sag01_estado es
                                                        where re.evento = ev.evento
                                                        and re.evento = op.evento
                                                        and re.opcion = op.opcion
                                                        and re.pais = pa.pais
                                                        and re.usuario_creacion = usc.usuario
                                                        and re.estado_registro = es.estado
                                                        and re.usuario_creacion = uss.usuario
                                                        and ev.evento = :evento
                                                        order by re.recibo";
        #endregion

        #region Metodos


        public Response<List<estadisticarecibo>> estadisticaPais(decimal ev)
        {
            Response<List<estadisticarecibo>> result = new Response<List<estadisticarecibo>>();
            result.code = 1;
            result.message = "Ocurrio un error en base de datos al tratar de obtener el listado de estadistica por Pais";
            result.data = new List<estadisticarecibo>();

            try
            {
                using (var db = new EntitiesRecibo())
                {
                    StringBuilder sqlstr = new StringBuilder();
                    sqlstr.Append(_sqlEstadisticaPais);

                    var list = db.Database.SqlQuery<estadisticarecibo>(sqlstr.ToString(), new object[] { ev }).ToList<estadisticarecibo>();

                    foreach (var item in list)
                    {
                        result.data.Add(item);
                    }
                    result.totalRecords = list.Count();
                }
                result.code = 0;
                result.message = "Ok";
                return result;
            }
            catch (Exception ex)
            {
                result.code = -1;
                result.message = "Ocurrio una excepcion al tratar de obtener la estadisticas por Pais";
                result.messageError = ex.ToString();
                return result;
            }
        }

        public Response<List<estadisticarecibo>> estadisticaConcepto(decimal ev)
        {
            Response<List<estadisticarecibo>> result = new Response<List<estadisticarecibo>>();
            result.code = 1;
            result.message = "Ocurrio un error en base de datos al tratar de obtener estadisticas por concepto";
            result.data = new List<estadisticarecibo>();

            try
            {
                using (var db = new EntitiesRecibo())
                {
                    StringBuilder sqlstr = new StringBuilder();
                    sqlstr.Append(_sqlEstadisticaConcepto);

                    var list = db.Database.SqlQuery<estadisticarecibo>(sqlstr.ToString(), new object[] { ev }).ToList<estadisticarecibo>();

                    foreach (var item in list)
                    {
                        result.data.Add(item);
                    }
                    result.totalRecords = list.Count();
                }
                result.code = 0;
                result.message = "Ok";
                return result;
            }
            catch (Exception ex)
            {
                result.code = -1;
                result.message = "Ocurrio una excepcion al tratar de obtener la estadisticas por Concepto";
                result.messageError = ex.ToString();
                return result;
            }
        }

        public Response<List<estadisticarecibo>> estadisticaTotales(decimal ev)
        {
            Response<List<estadisticarecibo>> result = new Response<List<estadisticarecibo>>();
            result.code = 1;
            result.message = "Ocurrio un error en Base de Datos al tratar de obtener los datos totales";
            result.data = new List<estadisticarecibo>();

            try
            {
                using (var db = new EntitiesRecibo())
                {
                    StringBuilder sqlstr = new StringBuilder();
                    sqlstr.Append(_sqlEstadisticaTotales);

                    var list = db.Database.SqlQuery<estadisticarecibo>(sqlstr.ToString(), new object[] { ev }).ToList<estadisticarecibo>();

                    foreach (var item in list)
                    {
                        result.data.Add(item);
                    }
                    result.totalRecords = list.Count();
                }
                result.code = 0;
                result.message = "Ok";
                return result;
            }
            catch (Exception ex)
            {
                result.code = -1;
                result.message = "ocurrio una excepcion al tratar de obtener los datos totales";
                result.messageError = ex.ToString();
                return result;
            }
        }

        public List<reciboconsulta> obtenerInfoDetallada()
        {
            List<reciboconsulta> result = new List<reciboconsulta>();
            
            try
            {
                using (var db = new EntitiesRecibo())
                {
                    StringBuilder sqlstr = new StringBuilder();
                    sqlstr.Append(_sqlObtenerInfoDetallada);

                    var list = db.Database.SqlQuery<reciboconsulta>(sqlstr.ToString(), new object[] { this.idEvento }).ToList<reciboconsulta>();

                    if (list == null)
                    {
                        result = new List<reciboconsulta>();                        
                    }
                    result = list;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        #endregion
    }
}