using CapaDeEntidad.Clases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeDatos.Clases
{
    public class CD_DetalleServicio
    {
        readonly CD_Conexion con = new CD_Conexion();

        #region Insertar
        public void CD_Insertar(CE_DetalleServicio Servicios)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_DS_Insertar",
                CommandType = CommandType.StoredProcedure,
            };
            com.Parameters.Add("@fecha", SqlDbType.Date).Value = Servicios.Fecha;
            com.Parameters.AddWithValue("@montoTotal", Servicios.MontoTotal);
            com.Parameters.AddWithValue("@idServicio", Servicios.IdServicio);
            com.Parameters.AddWithValue("@idUsuario", Servicios.IdUsuario);

            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }
        #endregion
    }
}