using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.Implementacion.Request
{
    public class CreditoDetalleEvidenciaRequest : CreditoEvidenciaRequest
    {
        public List<CreditoEvidenciaRequest> CreditoEvidencias { get; set; } = new List<CreditoEvidenciaRequest>();

    }
}
