using App.Service.Implementacion.Request;
using App.Service.Implementacion.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.Implementacion.Handlers
{
    public interface ICreditoHandler
    {
        CreditoResponses CargarDatosCreditosJson(CreditoRequest c);
        CreditoEvidenciaResponses CargarDatosEvidenciaJson(CreditoEvidenciaRequest c);
        Task<List<CreditoVistaExcelResponses>> ObtenerVistaCreditos(DateTime fechaInicio, DateTime fechaFin);
        Task<GeneralResponse<string>> GenerarReportePdfCreditos(DateTime fechaInicio, DateTime fechaFin);
        Task<GeneralResponse<string>> GenerarConstanciaPdfCreditos(int idCredito, string estado);

        Task<GeneralResponseDTO> CargarDatosCreditosEvidenciaJson(CreditoDetalleEvidenciaRequest cliente);
    }
}
