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
    public class CD_EstadoDepto
    {
        readonly CD_Conexion con = new CD_Conexion();
        readonly CE_EstadoDepto ce = new CE_EstadoDepto();

        #region IdEstadoDepto
        public int IdEstadoDepto(string EstadoDepto)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_P_IdEstado",
                CommandType = CommandType.StoredProcedure,
            };

            com.Parameters.AddWithValue("@estadoDepto", EstadoDepto);
            object resultado = com.ExecuteScalar();
            int idestado = (int)resultado;
            con.CerrarConexion();

            return idestado;
        }

        #endregion
        #region nombre estado
        
        public CE_EstadoDepto NombreEstado(int IdEstadoDepto)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_P_Estado", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@idEstadoDepto", SqlDbType.Int).Value = IdEstadoDepto;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            ce.EstadoDepto = Convert.ToString(row[0]);

            return ce;

        }

        #endregion

        #region Listar

        public List<string> ObtenerEstado()
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_P_CargarEstado",
                CommandType = CommandType.StoredProcedure
            };
            SqlDataReader reader = com.ExecuteReader();
            List<string> lista = new List<string>();
            while (reader.Read())
            {
                lista.Add(Convert.ToString(reader["estadoDepto"]));
            }
            con.CerrarConexion();

            return lista;
        }

        #endregion

    }
}
