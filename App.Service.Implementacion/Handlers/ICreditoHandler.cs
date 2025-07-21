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

        Task<GeneralResponseDTO> CargarDatosCreditosEvidenciaJson(CreditoDetalleEvidenciaRequest cliente);
    }
}
