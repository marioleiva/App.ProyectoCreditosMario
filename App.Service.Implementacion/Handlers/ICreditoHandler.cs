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
        Task<GeneralResponseDTO> CargarDatosCreditosJson(CreditoRequest cliente);
        Task<GeneralResponseDTO> CargarDatosCreditosEvidenciaJson(CreditoDetalleEvidenciaRequest cliente);
    }
}
