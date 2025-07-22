using App.ProyectoCreditos.Datos.Connection;
using App.Service.Implementacion.Handlers;
using App.Service.Implementacion.Request;
using App.Service.Implementacion.Responses;
using Dapper;
using Dapper.Oracle;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Document = iTextSharp.text.Document;

namespace App.ProyectoCreditos.Datos.Repository
{
    public class DCreditoHandlers : ICreditoHandler
    {
        private readonly IDbConnection DbConnection;
        Conexiones con = new Conexiones();
        public DCreditoHandlers()
        {
            DbConnection = con.ConstruirConexion();
        }

        public CreditoResponses CargarDatosCreditosJson(CreditoRequest c)
        {
            var Consulta = DbConnection.Query<CreditoResponses>("dbo.InsertarCredito", new
            {
                IdPersona = c.idPersona,
                IdTipoCredito = c.idTipoCredito,
                Importe = c.importe,
                Interes = c.interes,
                NumCuotas = c.numCuotas,
                Motivo = c.motivo
            }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return Consulta;
        }

        public CreditoEvidenciaResponses CargarDatosEvidenciaJson(CreditoEvidenciaRequest c)
        {
            var Consulta = DbConnection.Query<CreditoEvidenciaResponses>("dbo.InsertarEvidencia", new
            {
                idCredito = c.idCredito,
                idUsuario = c.idUsuario,
                titulo = c.titulo,
                ubicacionArchivo = c.ubicacionArchivo
            }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return Consulta;
        }

        public async Task<GeneralResponse<string>> GenerarConstanciaPdfCreditos(int idCredito, string estado)
        {
            GeneralResponse<string> result = new();
            Document doc = new Document(PageSize.A4, 30, 30, 30, 30);
            string pathFile = @"J:\Trash\Constancia_creditos.pdf";
            if (File.Exists(pathFile))
            {
                File.Delete(pathFile);
            }
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(pathFile, FileMode.Create));
            doc.Open();
            var titulo = new Paragraph("Constancia de Crédito")
            {
                Alignment = Element.ALIGN_CENTER,
                Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16)
            };
            doc.Add(titulo);
            doc.Add(new Paragraph(" "));
            PdfPTable tablaCreditos = new PdfPTable(9);
            tablaCreditos.AddCell("ID");
            tablaCreditos.AddCell("idPersona");
            tablaCreditos.AddCell("idTipoCredito");
            tablaCreditos.AddCell("Importe");
            tablaCreditos.AddCell("Interes");
            tablaCreditos.AddCell("NumCuotas");
            tablaCreditos.AddCell("Estado");
            tablaCreditos.AddCell("Motivo");
            tablaCreditos.AddCell("FechaSolicitud");
            CreditoVistaExcelResponses x = await ObtenerVistaCreditoConstancia(idCredito, estado);
            if (x.id == 0 )
            {
                result.IsSuccess = false;
                result.Title = "INFORMACIÓN";
                result.Message = "No se encontraron cpreditos en el rango de fechas especificada";
                result.TipoAlerta = TipoAlerta.Restrictivo;
                return result;
            }
           
                tablaCreditos.AddCell(x.id.ToString());
                tablaCreditos.AddCell(x.persona.ToString());
                tablaCreditos.AddCell(x.tipoCredito.ToString());
                tablaCreditos.AddCell(x.importe.ToString());
                tablaCreditos.AddCell(x.interes.ToString());
                tablaCreditos.AddCell(x.numCuotas.ToString());
                tablaCreditos.AddCell(x.estado);
                tablaCreditos.AddCell(x.motivo);
                tablaCreditos.AddCell(x.fechaSolicitud.ToString());
           
            doc.Add(tablaCreditos);
            doc.Add(new Paragraph($" Crédito { estado} - {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} "));
            doc.Close();
            writer.Close();

            result.IsSuccess = true;
            result.Title = "SATISFACTORIO";
            result.Message = "Resporte generado de manera correcta";
            result.TipoAlerta = TipoAlerta.Informativo;
            result.Data = pathFile;
            return result;
        }

        public async Task<CreditoVistaExcelResponses> ObtenerVistaCreditoConstancia(int idCredito, string estado)
        {
            CreditoVistaExcelResponses Consulta = DbConnection.Query<CreditoVistaExcelResponses>("dbo.ActualizarCredito", new
            {
                Id = idCredito,
                estado = estado
            }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return Consulta;
        }


        public async Task<GeneralResponse<string>> GenerarReportePdfCreditos(DateTime fechaInicio, DateTime fechaFin)
        {
            GeneralResponse<string> result = new();
            Document doc = new Document(PageSize.A4, 30,30,30,30);
            string pathFile = @"J:\Trash\reporte_creditos.pdf";
            if (File.Exists(pathFile))
            {
                File.Delete(pathFile);
            }
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(pathFile, FileMode.Create));
            doc.Open();
            var titulo = new Paragraph("Reporte de Reclamos")
            {
                Alignment = Element.ALIGN_CENTER,
                Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16)
            };
            doc.Add(titulo);
            doc.Add(new Paragraph(" "));
            PdfPTable tablaCreditos = new PdfPTable(9);
            tablaCreditos.AddCell("ID");
            tablaCreditos.AddCell("idPersona");
            tablaCreditos.AddCell("idTipoCredito");
            tablaCreditos.AddCell("Importe");
            tablaCreditos.AddCell("Interes");
            tablaCreditos.AddCell("NumCuotas");
            tablaCreditos.AddCell("Estado");
            tablaCreditos.AddCell("Motivo");
            tablaCreditos.AddCell("FechaSolicitud");
            GeneralResponse<List<CreditoVistaExcelResponses>> creditosResponse = await ObtenerVistaCreditosF(fechaInicio, fechaFin);
            if(!creditosResponse.IsSuccess == true || creditosResponse.Data.Count == 0)
            {
                result.IsSuccess = false;
                result.Title = "INFORMACIÓN";
                result.Message = "No se encontraron cpreditos en el rango de fechas especificada";
                result.TipoAlerta = TipoAlerta.Restrictivo;
                return result;
            }
            creditosResponse.Data.ForEach(x =>
            {
                tablaCreditos.AddCell(x.id.ToString());
                tablaCreditos.AddCell(x.idPersona.ToString());
                tablaCreditos.AddCell(x.idTipoCredito.ToString());
                tablaCreditos.AddCell(x.importe.ToString());
                tablaCreditos.AddCell(x.interes.ToString());
                tablaCreditos.AddCell(x.numCuotas.ToString());
                tablaCreditos.AddCell(x.estado);
                tablaCreditos.AddCell(x.motivo);
                tablaCreditos.AddCell(x.fechaSolicitud.ToString());
            });
            doc.Add(tablaCreditos);
            doc.Add(new Paragraph($" Generado con ITextSharp { DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} "));
            doc.Close();
            writer.Close();

            result.IsSuccess = true;
            result.Title = "SATISFACTORIO";
            result.Message = "Resporte generado de manera correcta";
            result.TipoAlerta = TipoAlerta.Informativo;
            result.Data = pathFile;
            return result;
        }

        public async Task<List<CreditoVistaExcelResponses>> ObtenerVistaCreditos(DateTime fechaInicio, DateTime fechaFin)
        {
            List<CreditoVistaExcelResponses> Consulta = DbConnection.Query<CreditoVistaExcelResponses>("dbo.ListarCredito", new
            {
                fechaInicio = fechaInicio,
                fechaFin = fechaFin
            }, commandType: CommandType.StoredProcedure).ToList();
            return Consulta;
        }

        public async Task<GeneralResponse<string>> GenerarExcelCreditos(DateTime fechaInicio, DateTime fechaFin)
        {
            GeneralResponse<string> result = new();
            GeneralResponse<List<CreditoVistaExcelResponses>> responseCreditos = await ObtenerVistaCreditosF(fechaInicio, fechaFin);

            if (!responseCreditos.IsSuccess || responseCreditos.Data is null)
            {
                result.IsSuccess = false;
                result.Title = responseCreditos.Title;
                result.Message = responseCreditos.Message;
                result.TipoAlerta = TipoAlerta.Restrictivo;
                return result;
            }

            string urlPlantilla = @"J:\GITHUB\App.ProyectoCreditosMario\Plantilla\PlantillaDescargoCredito.xlsx";
            string fileName = ConstruirExcelReclamos(urlPlantilla, responseCreditos.Data);
            string urlArchivo = string.Concat("http://localhost/Creditos/", fileName);

            result.IsSuccess = true;
            result.Title = "SATISFACTORIO";
            result.Message = "Resporte generado de manera correctamente";
            result.TipoAlerta = TipoAlerta.Informativo;
            result.Data = urlArchivo;
            return result;
        }

        public async Task<GeneralResponse<List<CreditoVistaExcelResponses>>> ObtenerVistaCreditosF(DateTime fechaInicio, DateTime fechaFin)
        {
            //List<CreditoRequest> CreditoEvidenciasDB = new List<CreditoRequest>();
            GeneralResponse<List<CreditoVistaExcelResponses>> result = new();

            result.Data = await ObtenerVistaCreditos(fechaInicio, fechaFin);
            if (result.Data.Count == 0)
            {
                result.IsSuccess = false;
                result.Title = "INFORMACIÓN";
                result.Message = "No se encontraron créditos en el rango de fechas especificado";
                result.TipoAlerta = TipoAlerta.Restrictivo;
                return result;
            }

            result.IsSuccess = true;
            result.Title = "Carga de datos exitosa";
            result.Message = "Se han obtenido los créditos correctamente";
            result.TipoAlerta = TipoAlerta.Informativo;
            return result;
        }

        private static string ConstruirExcelReclamos(string plantillaRuta, List<CreditoVistaExcelResponses> creditos)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            FileInfo fileInfo = new FileInfo(plantillaRuta);
            string nuevaRuta = "";
            using (FileStream fs = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read))
            {
                using (ExcelPackage package = new ExcelPackage(fs))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int row = 10;

                    worksheet.Cells[row,1].LoadFromCollection(creditos,false);
                    string nuevoNombreArchivo = $"Creditos_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                    nuevaRuta = Path.Combine(@"J:\Trash", nuevoNombreArchivo);
                    package.SaveAs(new FileInfo(nuevaRuta));
                    return nuevoNombreArchivo;
                }
            }

        }

        public Task<GeneralResponseDTO> CargarDatosCreditosEvidenciaJson(CreditoDetalleEvidenciaRequest cliente)
        {
            throw new NotImplementedException();
        }

        
    }
}
