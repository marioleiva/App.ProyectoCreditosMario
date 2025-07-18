using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.Implementacion.Request
{
    public class EvidenciasRequest
    {
        public int Id { get; set; }
        public string idCredito { get; set; }
        public int idUsuario { get; set; }
        public string titulo { get; set; }

    }
}
