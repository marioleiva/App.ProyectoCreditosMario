using App.ProyectoCreditos.Datos.Repository;
using App.Service.Implementacion.Request;
using App.Service.Implementacion.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
    }
}
