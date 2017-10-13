using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using RSA01.Data;
using RSA01.Clases;
using System.Text;


namespace RSA01.Models.Recibo
{
    public class GestionRecibo
    {
        string usuario = HttpContext.Current.User.Identity.Name.ToUpper().ToString();
        DateTime fecha = DateTime.Now;
        public decimal numerorecibo { get; set; }

        #region Constantes
        private const string _sqlObtenerRecibo = @"select recibo idrecibo, nombre, cantidad, 
                                                    valor, total, detalle,evento,
                                                    opcion, pais,estado_registro,usuario_creacion,
                                                    to_char(fecha_creacion,'dd/mm/yyyy hh24:mi:ss') fecha_creacion,usuario_modificacion,
                                                    to_char(fecha_modificacion,'dd/mm/yyyy hh24:mi:ss') fecha_modificacion
                                                    from sag01_recibo
                                                    where estado_registro = 'A'
                                                    and recibo = :recibo";

        private const string _sqlObtenerEvento = @"select evento idevento, nombre
                                                    from sag01_evento
                                                    where estado_registro = 'A'";

        private const string _sqlObtenerPaises = @"select pais idpais, descripcion
                                                    from sag01_pais
                                                    where estado_registro = 'A'
                                                    order by descripcion";

        private const string _sqlObtenerOpcionesEvento = @"select opcion, evento, descripcion, precio
                                                            from sag01_evento_opcion
                                                            where estado_registro = 'A'
                                                            and evento = :evento
                                                            order by opcion";

        private const string _sqlObtenerPais = @"select pais, descripcion
                                                    from sag01_pais
                                                    where upper(pais) = upper(:pais)
                                                    and estado_registro = 'A'";

        private const string _sqlObtenerOpcion = @"select opcion, evento, descripcion
                                                    from sag01_evento_opcion
                                                    where evento = :evento
                                                    and opcion = :opcion
                                                    and estado_registro = 'A'";

        private const string _sqlObtenerRecibos = @"select re.evento idevento, re.recibo idrecibo, re.nombre, 
                                                    re.total,pa.descripcion pais, es.descripcion estado, ev.nombre evento_nombre, 
                                                    op.descripcion opcion, us.nombre usuario
                                                    from sag01_recibo re, sag01_evento ev, 
                                                    sag01_pais pa, sag01_evento_opcion op, 
                                                    sag01_usuario us, sag01_estado es
                                                    where re.evento = ev.evento
                                                    and re.evento = op.evento
                                                    and re.opcion = op.opcion
                                                    and re.pais = pa.pais
                                                    and re.usuario_creacion = us.usuario
                                                    and re.estado_registro = es.estado
                                                    and re.evento = :idevento
                                                    order by re.recibo desc";

        private const string _sqlDatosRecibo = @"select re.recibo idrecibo, re.nombre, 
                                                    re.total, es.descripcion estado, ev.nombre evento_nombre, 
                                                    op.descripcion opcion, RE.DETALLE, us.nombre usuario, 
                                                    re.usuario_creacion us_name, pa.descripcion pais,
                                                    to_char(re.fecha_creacion,'dd/mm/yyyy hh24:mi:ss') fecha_creacion
                                                    from sag01_recibo re, sag01_evento ev, 
                                                    sag01_pais pa, sag01_evento_opcion op, 
                                                    sag01_usuario us, sag01_estado es
                                                    where re.evento = ev.evento
                                                    and re.evento = op.evento
                                                    and re.opcion = op.opcion
                                                    and re.pais = pa.pais
                                                    and re.usuario_creacion = us.usuario
                                                    and re.estado_registro = es.estado
                                                    and re.recibo = :idrecibo";



        #endregion

        #region Metodos

        public Response<List<evento>> obtenerEventosActivos()
        {
            Response<List<evento>> result = new Response<List<evento>>();
            result.code = 1;
            result.message = "Ocurrio un Error en Base de Datos al tratar de obtener el evento";
            result.data = new List<evento>();

            try
            {
                using (var db = new EntitiesRecibo())
                {
                    StringBuilder sqlstr = new StringBuilder();
                    sqlstr.Append(_sqlObtenerEvento);

                    var list = db.Database.SqlQuery<evento>(sqlstr.ToString()).ToList<evento>();

                    foreach (var item in list)
                    {
                        result.data.Add(item);
                    }
                    result.totalRecords = list.Count();
                }
                result.code = 0;
                result.message = "OK";
                return result;

            }
            catch (Exception ex)
            {
                result.code = -1;
                result.message = "Ocurrio una excepcion al obtener el listado de Eventos";
                result.messageError = ex.ToString();
                return result;
            }
        }

        public Response<recibo> obtenerRecibo(decimal rec)
        {
            Response<recibo> result = new Response<recibo>();
            result.code = 1;
            result.message = "Ocurrio un error en base de datos al tratar de obtener el recibo";
            result.data = new recibo();

            try
            {
                using (var db = new EntitiesRecibo())
                {
                    StringBuilder strsql = new StringBuilder();
                    strsql.Append(_sqlObtenerRecibo);
                    var list = db.Database.SqlQuery<recibo>(strsql.ToString(), new object[] { rec }).SingleOrDefault<recibo>();

                    if (list == null)
                    {
                        result.code = -1;
                        result.message = "No existe informacion del Recibo indicado";
                        result.data = new recibo();
                        result.totalRecords = 0;
                        return result;
                    }

                    if (list.pais != null)
                    {
                        StringBuilder sqlpais = new StringBuilder();
                        sqlpais.Append(_sqlObtenerPais);
                        var listpais = db.Database.SqlQuery<pais>(sqlpais.ToString(), new object[] { list.pais }).SingleOrDefault<pais>();

                        if (listpais == null)
                        {
                            result.code = -1;
                            result.message = "No existe informacion del Pais";
                            return result;                            
                        }
                        list.pais_ = new pais();
                        list.pais_.idpais = listpais.idpais;
                        list.pais_.descripcion = listpais.descripcion;                        
                    }

                    if (list.opcion != null)
                    {
                        StringBuilder sqlopcion = new StringBuilder();
                        sqlopcion.Append(_sqlObtenerOpcion);
                        var listopcion = db.Database.SqlQuery<eventoopcion>(sqlopcion.ToString(), new object[] { list.evento,list.opcion }).SingleOrDefault<eventoopcion>();

                        if (listopcion == null)
                        {
                            result.code = -1;
                            result.message = "No existe informacion de la opcion del Evento";
                            return result;                             
                        }
                        list.opcion_ = new eventoopcion();
                        list.opcion_.evento = listopcion.evento;
                        list.opcion_.opcion = listopcion.opcion;
                        list.opcion_.descripcion = listopcion.descripcion;                        
                    }
                    result.data = list;
                }
                result.code = 0;
                result.message = "OK";
                return result;
            }
            catch (Exception ex)
            {
                result.code = -1;
                result.message = "Ocurrio una excepcion al momento de obtener el recibo";
                result.message = ex.ToString();
                return result;
            }
        }

        public Response<List<reciboconsulta>> obtenerRecibos(decimal eve)
        {
            Response<List<reciboconsulta>> result = new Response<List<reciboconsulta>>();
            result.code = 1;
            result.message = "Ocurrio un error en Base de Datos al obtener el listado de Recibos";
            result.data = new List<reciboconsulta>();

            try
            {
                using (var db = new EntitiesRecibo())
                {
                    StringBuilder sqllist = new StringBuilder();
                    sqllist.Append(_sqlObtenerRecibos);

                    var list = db.Database.SqlQuery<reciboconsulta>(sqllist.ToString(), new object[] { eve }).ToList<reciboconsulta>();

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
                result.message = "Ocurrio una excepcion al obtener el listado de Recibos";
                result.messageError = ex.ToString();
                return result;
            }

        }

        public Response<List<eventoopcion>> obtenerOpcionesEvento(decimal ev)
        {
            Response<List<eventoopcion>> result = new Response<List<eventoopcion>>();
            result.code = 1;
            result.message = "Ocurrio un Error en base de datos al tratar de Obtener las Opciones del Evento";
            result.data = new List<eventoopcion>();

            try
            {
                using (var db = new EntitiesRecibo())
                {
                    StringBuilder sqlstr = new StringBuilder();
                    sqlstr.Append(_sqlObtenerOpcionesEvento);

                    var list = db.Database.SqlQuery<eventoopcion>(sqlstr.ToString(), new object[] { ev }).ToList<eventoopcion>();

                    foreach (var item in list)
                    {
                        result.data.Add(item);
                    }
                    result.totalRecords = list.Count();
                }
                result.code = 0;
                result.message = "OK";
                return result;
            }
            catch (Exception ex)
            {
                result.code = -1;
                result.message = "Ocurrio una excepcion al obtener el listado de Opciones del Evento";
                result.messageError = ex.ToString();
                return result;
            }
        }

        public Response<List<pais>> obtenerListaPaises()
        {
            Response<List<pais>> result = new Response<List<pais>>();
            result.code = 1;
            result.message = "Ocurrio un error en Base de Datos al tratar de obtener los paises";
            result.data = new List<pais>();

            try
            {
                using (var db = new EntitiesRecibo())
                {
                    StringBuilder sqlstr = new StringBuilder();
                    sqlstr.Append(_sqlObtenerPaises);

                    var list = db.Database.SqlQuery<pais>(sqlstr.ToString()).ToList<pais>();

                    foreach (var item in list)
                    {
                        result.data.Add(item);
                    }
                    result.totalRecords = list.Count();
                }
                result.code = 0;
                result.message = "OK";
                return result;

            }
            catch (Exception ex)
            {
                result.code = -1;
                result.message = "Ocurrio una excepcion al obtener el listado de Paises";
                result.messageError = ex.ToString();
                return result;
            }
        }

        public Response<recibo> nuevoRecibo(recibo obj)
        {
            Response<recibo> result = new Response<recibo>();
            result.code = 1;
            result.message = "Ocurrio un Error a nivel de Base de Datos al tratar de Guardar el Recibo";
            result.data = new recibo();

            if (obj.nombre == null || obj.nombre=="" ||
                obj.pais_.idpais ==null || obj.pais_.idpais =="" ||
                obj.opcion_.opcion.Equals(null)||
                obj.valor.Equals(null)||obj.cantidad.Equals(null) || obj.total.Equals(null))
            {
                result.code = -1;
                result.message = "Los datos: nombre, pais, opcion, precio, cantidad y total no pueden ser datos vacios, favor de verificar";
                result.data = new recibo();
                return result;
            }

            try
            {
                //Se inicia la transaccion
                using (var tra = new System.Transactions.TransactionScope())
                {
                    using (var db = new EntitiesRecibo())
                    {
                        //se obtiene el correlativo de la llave primaria SAG01_RECIBO de la tabla SAG01_CORRELATIVO
                        var correlativo = (from c in db.SAG01_CORRELATIVO where c.CORRELATIVO == 1 && c.ESTADO_REGISTRO == "A" select c).SingleOrDefault();
                        if (correlativo == null)
                        {
                            result.code = 1;
                            result.message = "No fue posible generar el Recibo";
                            return result;
                        }

                        //Se guarda el registro del Recibo en la tabla SAG01_RECIBO
                        SAG01_RECIBO re = new SAG01_RECIBO(){
                            RECIBO = (int) correlativo.CORRELATIVO_DISPONIBLE,
                            NOMBRE = obj.nombre,
                            CANTIDAD = obj.cantidad,
                            VALOR = obj.valor,
                            TOTAL = obj.total,
                            DETALLE = obj.detalle,
                            EVENTO = obj.opcion_.evento,
                            OPCION = obj.opcion_.opcion,
                            PAIS = obj.pais_.idpais,
                            ESTADO_REGISTRO = "A",
                            USUARIO_CREACION = usuario,
                            FECHA_CREACION = fecha                            
                        };
                        db.SAG01_RECIBO.Add(re);
                        correlativo.CORRELATIVO_DISPONIBLE++;
                        int res = db.SaveChanges();
                        if (res <= 0)
                        {
                            System.Transactions.Transaction.Current.Rollback();
                            result.code = 2;
                            result.message = "No fue posible generar el Recibo";
                            return result;
                        }
                        correlativo.CORRELATIVO_DISPONIBLE--;
                        result.data.idrecibo =(decimal)correlativo.CORRELATIVO_DISPONIBLE;
                    }
                    //Se hace commit de los cambios
                    tra.Complete();
                    result.code = 0;                    
                    result.message = "Se registro el Recibo de forma Exitosa";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.code = -1;
                result.message = "Ocurrio una excepcion al tratar de registrar el Recibo";
                result.messageError = ex.ToString();
                return result;
            }
        }

        public Response<recibo> actualizarRecibo(recibo obj)
        {
            Response<recibo> result = new Response<recibo>();
            result.code = 1;
            result.message = "Ocurrio un error en base datos al tratar de actualizar el recibo";
            result.data = new recibo();
            
            if (obj.nombre == null || obj.nombre == "" ||
                obj.pais == null || obj.pais == "" ||
                obj.evento.Equals(null) || obj.opcion.Equals(null) ||
                obj.valor.Equals(null) || obj.cantidad.Equals(null) || obj.total.Equals(null))
            {
                result.code = -1;
                result.message = "Los datos: nombre, pais, opcion, precio, cantidad y total no pueden ser datos vacios, favor de verificar";
                result.data = new recibo();
                return result;
            }

            try
            {
                //Se inicia la transaccion
                using (var trans = new System.Transactions.TransactionScope())
                {
                    using (var db = new EntitiesRecibo())
                    {
                        //se busca el registro a actualizar
                        SAG01_RECIBO regActual = (from c in db.SAG01_RECIBO
                                                  where c.RECIBO == obj.idrecibo
                                                  && c.ESTADO_REGISTRO == "A"
                                                  select c).FirstOrDefault();

                        if (regActual == null)
                        {
                            result.code = 1;
                            result.message = "No existe el recibo indicado";
                            return result;
                        }
                        regActual.NOMBRE = obj.nombre;
                        regActual.CANTIDAD = obj.cantidad;
                        regActual.VALOR = obj.valor;
                        regActual.DETALLE = obj.detalle;
                        regActual.EVENTO = obj.evento;
                        regActual.OPCION = obj.opcion;
                        regActual.PAIS = obj.pais;
                        regActual.USUARIO_MODIFICACION = usuario;
                        regActual.FECHA_MODIFICACION = fecha;

                        //se Actualiza la informacion del Recibo 
                        int res = db.SaveChanges();
                        if (res <= 0)
                        {
                            System.Transactions.Transaction.Current.Rollback();
                            result.code = 2;
                            result.message = "No fue posible Actualizar los datos del Recibo";
                            return result;
                        }
                    }
                    //Se hace commit de los cambios
                    trans.Complete();
                    result.code = 0;
                    result.message = "Se Actualizo el Recibo de forma Exitosa";
                    return result;                    
                }
            }
            catch (Exception ex)
            {
                result.code = -1;
                result.message = "Ocurrio una excepcion al tratar de Actualizar el Recibo";
                result.messageError = ex.ToString();
                return result;
            }
        }

        public Response<recibo> anularRecibo(decimal rec)
        {
            Response<recibo> result = new Response<recibo>();
            result.code = 1;
            result.message = "Ocurrio un error en Base de datos al tratar de Anular";
            result.data = new recibo();

            try
            {
                using (var trans = new System.Transactions.TransactionScope())
                {
                    using (var db = new EntitiesRecibo())
                    {
                        //se busca el registro a actualizar
                        SAG01_RECIBO regActual = (from c in db.SAG01_RECIBO
                                                  where c.RECIBO == rec
                                                  select c).FirstOrDefault();
                        if (regActual == null)
                        {
                            result.code = 1;
                            result.message = "No existe el recibo indicado";
                            return result;
                        }

                        if (regActual.ESTADO_REGISTRO != "A")
                        {
                            result.code = 1;
                            result.message = "El Recibo ya se encuentra Anulado";
                            return result;
                        }

                        regActual.ESTADO_REGISTRO = "N";
                        regActual.USUARIO_MODIFICACION = usuario;
                        regActual.FECHA_MODIFICACION = fecha;

                        //se Actualiza la informacion del Recibo 
                        int res = db.SaveChanges();
                        if (res <= 0)
                        {
                            System.Transactions.Transaction.Current.Rollback();
                            result.code = 2;
                            result.message = "No fue posible Anular el Recibo";
                            return result;
                        }
                    }
                    //Se hace commit de los cambios
                    trans.Complete();
                    result.code = 0;
                    result.message = "Se Anulo el Recibo de forma Exitosa";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.code = -1;
                result.message = "Ocurrio una excepcion al tratar de Anular el Recibo";
                result.messageError = ex.ToString();
                return result;
            }
        }

        public List<reciboconsulta> obtenerDatosRecibo()
        {
            List<reciboconsulta> result = new List<reciboconsulta>();

            try
            {
                using (var db = new EntitiesRecibo())
                {
                    StringBuilder sqlstr = new StringBuilder();
                    sqlstr.Append(_sqlDatosRecibo);

                    var list = db.Database.SqlQuery<reciboconsulta>(sqlstr.ToString(), new object[] { this.numerorecibo }).ToList<reciboconsulta>();
                                        
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