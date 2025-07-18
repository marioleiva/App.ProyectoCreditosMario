using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.Implementacion.Request
{
    public class CreditoRequest
    {
        public int Id { get; set; }
        public string TipoCredito { get; set; }
        public double Importe { get; set; }
        public int NumCuotas { get; set; }
        public double Interes { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Motivo { get; set; }

    }
}
