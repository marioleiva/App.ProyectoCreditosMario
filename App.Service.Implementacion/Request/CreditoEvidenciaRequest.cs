﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.Implementacion.Request
{
    public class CreditoEvidenciaRequest
    {
        public int id { get; set; }
        public int idCredito { get; set; }
        public int idUsuario { get; set; }
        public string titulo { get; set; }
        public string ubicacionArchivo { get; set; }
    }
}
