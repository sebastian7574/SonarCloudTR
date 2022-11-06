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
    public class CD_DetalleInconveniente
    {
        readonly CD_Conexion con = new CD_Conexion();
        readonly CE_DetalleInconveniente ce = new CE_DetalleInconveniente();

        #region Insertar
        public void CD_Insertar(CE_DetalleInconveniente Multas)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_DI_IngresarMulta",
                CommandType = CommandType.StoredProcedure,
            };
            com.Parameters.AddWithValue("@detalle", Multas.Detalle);
            com.Parameters.AddWithValue("@medioDePago", Multas.MedioDePago);
            com.Parameters.AddWithValue("@banco", Multas.Banco);
            com.Parameters.AddWithValue("@comprobante", Multas.Comprobante);
            com.Parameters.AddWithValue("@monto", Multas.Monto);
            com.Parameters.AddWithValue("@efectivo", Multas.Efectivo);
            com.Parameters.AddWithValue("@vuelto", Multas.Vuelto);
            com.Parameters.AddWithValue("@idReserva", Multas.IdReserva);

            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }
        #endregion
    }
}
