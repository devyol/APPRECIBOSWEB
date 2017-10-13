using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RSA01.Clases;
using RSA01.Models.Recibo;
using RSA01.Models.Estadistica;
using Microsoft.Reporting.WebForms;

namespace RSA01.Controllers
{
    public class ImpresionReciboController : Controller
    {
        //
        // GET: /ImpresionRecibo/

        public ActionResult Index()
        {
            return View();
        }

        public FileResult imprimir(decimal idRecibo)
        {
            List<reciboconsulta> Servicio = new List<reciboconsulta>();
            GestionRecibo objServicio = new GestionRecibo() { numerorecibo = idRecibo };
            Servicio = objServicio.obtenerDatosRecibo();

            ReportViewer rv = new ReportViewer();
            rv.ProcessingMode = ProcessingMode.Local;
            rv.LocalReport.ReportPath = Server.MapPath("~/Reportes/RptReciboSAG.rdlc");
            rv.LocalReport.DataSources.Clear();
            ReportDataSource dsEncabezado = new ReportDataSource("dtsReciboSAG", Servicio);
            rv.LocalReport.DataSources.Add(dsEncabezado);
            rv.LocalReport.Refresh();

            byte[] streamBytes = null;
            string mimeType = "";
            string enconding = "";
            string filenameExtension = "";
            string[] streamids = null;
            Warning[] warnings = null;

            streamBytes = rv.LocalReport.Render("PDF", null, out mimeType, out enconding, out filenameExtension, out streamids, out warnings);
            return File(streamBytes, mimeType);
        }

        public FileResult reporteDetallado(decimal idevento)
        {
            List<reciboconsulta> Servicio = new List<reciboconsulta>();
            GeneracionEstadisticas objServicio = new GeneracionEstadisticas() { idEvento = idevento };
            Servicio = objServicio.obtenerInfoDetallada();

            ReportViewer rv = new ReportViewer();
            rv.ProcessingMode = ProcessingMode.Local;
            rv.LocalReport.ReportPath = Server.MapPath("~/Reportes/RptRecporteDetallado.rdlc");
            rv.LocalReport.DataSources.Clear();
            ReportDataSource dsEncabezado = new ReportDataSource("dtsInfoDetallada", Servicio);
            rv.LocalReport.DataSources.Add(dsEncabezado);
            rv.LocalReport.Refresh();

            byte[] streamBytes = null;
            string mimeType = "";
            string enconding = "";
            string filenameExtension = "";
            string[] streamids = null;
            Warning[] warnings = null;

            streamBytes = rv.LocalReport.Render("PDF", null, out mimeType, out enconding, out filenameExtension, out streamids, out warnings);
            return File(streamBytes, mimeType);
        }

    }
}
