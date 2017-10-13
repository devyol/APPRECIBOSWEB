//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RSA01.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class SAG01_RECIBO
    {
        public decimal RECIBO { get; set; }
        public string NOMBRE { get; set; }
        public Nullable<decimal> CANTIDAD { get; set; }
        public Nullable<decimal> VALOR { get; set; }
        public Nullable<decimal> TOTAL { get; set; }
        public string DETALLE { get; set; }
        public Nullable<decimal> EVENTO { get; set; }
        public Nullable<decimal> OPCION { get; set; }
        public string PAIS { get; set; }
        public string ESTADO_REGISTRO { get; set; }
        public string USUARIO_CREACION { get; set; }
        public Nullable<System.DateTime> FECHA_CREACION { get; set; }
        public string USUARIO_MODIFICACION { get; set; }
        public Nullable<System.DateTime> FECHA_MODIFICACION { get; set; }
    
        public virtual SAG01_EVENTO SAG01_EVENTO { get; set; }
        public virtual SAG01_EVENTO_OPCION SAG01_EVENTO_OPCION { get; set; }
        public virtual SAG01_PAIS SAG01_PAIS { get; set; }
    }
}