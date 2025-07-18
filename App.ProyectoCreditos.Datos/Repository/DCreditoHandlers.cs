using App.Service.Implementacion.Handlers;
using App.Service.Implementacion.Request;
using App.Service.Implementacion.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ProyectoCreditos.Datos.Repository
{
    public class DCreditoHandlers : ICreditoHandler
    {
        public Task<CreditoRequest> AdmClientes(GeneralResponseDTO cliente)
        {
            throw new NotImplementedException();
        }
    }
}
