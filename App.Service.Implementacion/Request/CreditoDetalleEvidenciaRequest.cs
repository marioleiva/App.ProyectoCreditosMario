using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.Implementacion.Request
{
    public class CreditoDetalleEvidenciaRequest
    {
        public List<CreditoRequest> CreditoEvidencias { get; set; } = new List<CreditoRequest>();

    }
}
