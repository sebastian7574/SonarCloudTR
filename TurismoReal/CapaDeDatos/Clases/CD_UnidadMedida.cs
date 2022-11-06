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
    public class CD_UnidadMedida
    {
        readonly CD_Conexion con = new CD_Conexion();
        private CE_UnidadMedida ce = new CE_UnidadMedida();

        #region IdUnidad
        public int IdUnidad(string TipoUnidad)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_UM_IdUnidad",
                CommandType = CommandType.StoredProcedure,
            };

            com.Parameters.AddWithValue("@tipoUnidad", TipoUnidad);
            object resultado = com.ExecuteScalar();
            int idunidad = (int)resultado;
            con.CerrarConexion();

            return idunidad;
        }

        #endregion
        #region nombre unidad

        public CE_UnidadMedida NombreUnidad(int IdUnidadMedida)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_UM_Nombre", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@idUnidadMedida", SqlDbType.Int).Value = IdUnidadMedida;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            ce.TipoUnidad = Convert.ToString(row[0]);

            return ce;

        }

        #endregion

        #region Listar

        public List<string> ObtenerUnidad()
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_UM_CargarUnidad",
                CommandType = CommandType.StoredProcedure
            };
            SqlDataReader reader = com.ExecuteReader();
            List<string> lista = new List<string>();
            while (reader.Read())
            {
                lista.Add(Convert.ToString(reader["tipoUnidad"]));
            }
            con.CerrarConexion();

            return lista;
        }

        #endregion

    }
}