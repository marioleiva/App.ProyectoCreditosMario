using App.ProyectoCreditos.Datos.Connection;
using App.Service.Implementacion.Handlers;
using App.Service.Implementacion.Request;
using App.Service.Implementacion.Responses;
using Dapper;
using Dapper.Oracle;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ProyectoCreditos.Datos.Repository
{
    public class DCreditoHandlers : ICreditoHandler
    {
        private readonly IDbConnection DbConnection;
        Conexiones con = new Conexiones();
        public DCreditoHandlers()
        {
            DbConnection = con.ConstruirConexion();
        }

        public CreditoResponses CargarDatosCreditosJson(CreditoRequest c)
        {
            var Consulta = DbConnection.Query<CreditoResponses>("dbo.InsertarCredito", new
            {
                IdPersona = c.idPersona,
                IdTipoCredito = c.idTipoCredito,
                Importe = c.importe,
                Interes = c.interes,
                NumCuotas = c.numCuotas,
                Motivo = c.motivo
            }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return Consulta;
        }

        public CreditoEvidenciaResponses CargarDatosEvidenciaJson(CreditoEvidenciaRequest c)
        {
            var Consulta = DbConnection.Query<CreditoEvidenciaResponses>("dbo.InsertarEvidencia", new
            {
                idCredito = c.idCredito,
                idUsuario = c.idUsuario,
                titulo = c.titulo,
                ubicacionArchivo = c.ubicacionArchivo
            }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return Consulta;
        }

        public Task<GeneralResponseDTO> CargarDatosCreditosEvidenciaJson(CreditoDetalleEvidenciaRequest cliente)
        {
            throw new NotImplementedException();
        }

        
    }
}
