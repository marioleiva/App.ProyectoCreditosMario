using App.ProyectoCreditos.Datos.Repository;
using App.Service.Implementacion.Request;
using App.Service.Implementacion.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO.Compression;
using CompressionLevel = System.IO.Compression.CompressionLevel;



namespace App.ProyectoCreditos.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditoController : ControllerBase
    {
        //private readonly ILog _logger = LogManager.GetLogger("CARTA_GARANTIA");
        private DCreditoHandlers CC;
        public CreditoController()
        {
            CC = new DCreditoHandlers();
        }

        /// <summary>
        /// Cargar datos de Creditos
        /// </summary>
        /// <param name="creditosDB"></param>
        /// <returns></returns>
        [Route("InsertarCreditoJson")]
        [HttpPost]
        public GeneralResponse<List<CreditoResponses>> CargarDatosCreditosJson(CreditoDetalleEvidenciaRequest creditosDB)
        {
            //List<CreditoRequest> CreditoEvidenciasDB = new List<CreditoRequest>();
            GeneralResponse<List<CreditoResponses>> result = new GeneralResponse<List<CreditoResponses>>();
            result.Data = new List<CreditoResponses>();
            foreach (var item in creditosDB.CreditoEvidencias)
            {
                var credito =  CC.CargarDatosCreditosJson(item);
                credito.evidencias = new List<CreditoEvidenciaResponses>();
                foreach (var itemEvidencia in item.evidencias)
                {
                    itemEvidencia.idCredito = credito.id;
                    var evidencia = CC.CargarDatosEvidenciaJson(itemEvidencia);
                    credito.evidencias.Add(evidencia);
                }
                result.Data.Add(credito);
            }
            result.IsSuccess = true;
            result.Title = "Carga de datos exitosa";
            result.Message = "Los datos de créditos se han cargado correctamente";
            return result;
        }

        [Route("GenerarConstanciaPdfCreditos")]
        [HttpPost]
        public async Task<ActionResult<GeneralResponse<string>>> GenerarConstanciaPdfCreditos(int idCredito, string estado)
        {
            GeneralResponse<string> result = await CC.GenerarConstanciaPdfCreditos(idCredito, estado);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        /// <summary>
        /// Descargar excel
        /// </summary>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        [Route("GenerarReporteCreditos")]
        [HttpPost]
        public async Task<ActionResult<GeneralResponse<string>>> GenerarReporteCreditos(DateTime fechaInicio, DateTime fechaFin)
        {
            GeneralResponse<string> result = await CC.GenerarExcelCreditos(fechaInicio, fechaFin);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [Route("GenerarReportePdfCreditos")]
        [HttpPost]
        public async Task<ActionResult<GeneralResponse<string>>> GenerarReportePdfCreditos(DateTime fechaInicio, DateTime fechaFin)
        {
            GeneralResponse<string> result = await CC.GenerarReportePdfCreditos(fechaInicio, fechaFin);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        //[Route("GenerarDescargaZipCreditos")]
        [HttpGet("GenerarDescargaZipCreditos/{idCredito}")]
        public async Task<ActionResult> GenerarDescargaZipCreditos(int idCredito)
        {
            string carpetaOrigen = @"J:\Trash\creditosEnviar";
            var archivos = Directory.GetFiles(carpetaOrigen, $"*.xlsx*");


            if (!archivos.Any())
            {
                return NotFound("No se encontraron archivos para el crédito especificado");
            }
            var zipStream = new MemoryStream();
            using(var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, leaveOpen: true))
            {
                foreach(var archivo in archivos)
                {
                    var nombreArchivo = Path.GetFileName(archivo);
                    var zipEntry = archive.CreateEntry(nombreArchivo, CompressionLevel.Fastest);

                    using var entryStream = zipEntry.Open();
                    using var fileStream = System.IO.File.OpenRead(archivo);
                    fileStream.CopyTo(entryStream);
                }
            }
            zipStream.Position = 0;
            return File(zipStream, "application/zip", $"evidencias_Creditos{idCredito}.zip");
        }
            
    }
}
