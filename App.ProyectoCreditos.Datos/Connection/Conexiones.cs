using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ProyectoCreditos.Datos.Connection
{
    public class Conexiones
    {

        public Conexiones()
        {
            
        }
        //string connString = "DATA SOURCE=(DESCRIPTION=(LOAD_BALANCE=yes)(ADDRESS=(PROTOCOL=TCP)(HOST=192.1.0.191)(PORT=1521))(CONNECT_DATA=(SERVER=dedicated)(SERVICE_NAME=chspsp))));USER ID = VDIAZO; Password=vdiazo123";
        //string connString = "DATA SOURCE=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.1.0.81)(PORT = 1521)) (ADDRESS = (PROTOCOL = TCP)(HOST = 192.1.0.82)(PORT = 1521)) (LOAD_BALANCE = yes)(CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = chsp)));USER ID = API_INTQUALAB; Password=qualab$54Sp";

        // public IDbConnection ConstruirConexion()
        // {
        //     return  new OracleConnection(this.connString);
        // }
    }
    
}
