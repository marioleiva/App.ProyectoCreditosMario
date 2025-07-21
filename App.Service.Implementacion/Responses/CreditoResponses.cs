using App.Service.Implementacion.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.Implementacion.Responses
{
    public class CreditoResponses
    {
        public int id { get; set; }
        public int idPersona { get; set; }
        public int idTipoCredito { get; set; }
        public double importe { get; set; }
        public double interes { get; set; }
        public int numCuotas { get; set; }
        public string motivo { get; set; }
        public string estado { get; set; }
        public List<CreditoEvidenciaResponses> evidencias { get; set; }
       
    }
}
