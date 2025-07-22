using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.Implementacion.Responses
{
    public class CreditoVistaExcelResponses
    {
        public int id { get; set; }
        public int idPersona { get; set; }
        public string persona { get; set; }
        public int idTipoCredito { get; set; }
        public string tipoCredito { get; set; }
        public double importe { get; set; }
        public double interes { get; set; }
        public int numCuotas { get; set; }
        public string motivo { get; set; }
        public string estado { get; set; }
        public DateTime? fechaSolicitud { get; set; }
    }
}

