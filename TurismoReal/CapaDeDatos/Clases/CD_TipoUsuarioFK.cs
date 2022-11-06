using CapaDeEntidad.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeDatos.Clases
{
    public class CD_TipoUsuarioFK
    {
        readonly CD_Conexion con = new CD_Conexion();
        readonly CE_TipoUsuarioFK ce = new CE_TipoUsuarioFK();

        #region IdTipoUsuario
        public int IdTipoUsuario(string TipoUsuario)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_P_IdTipoUsuario",
                CommandType = CommandType.StoredProcedure,
            };

            com.Parameters.AddWithValue("@tipoUsuario", TipoUsuario);
            object valor = com.ExecuteScalar();
            int idtipousuario = (int)valor;
            con.CerrarConexion();

            return idtipousuario;
        }

        #endregion
        #region NombreTipoUsuario

        public CE_TipoUsuarioFK NombreTipoUsuario(int IdTipoUsuario)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_P_NombreTipoUsuario", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@idTipoUsuario", SqlDbType.Int).Value = IdTipoUsuario;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            ce.TipoUsuario = Convert.ToString(row[0]);

            return ce;
        
        }

        #endregion

        #region Listar

        public List<string> ObtenerTiposUsuario()
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText= "dbo.SP_P_CargarTipoUsuarioFK",
                CommandType = CommandType.StoredProcedure
            };
            SqlDataReader reader = com.ExecuteReader();
            List<string> lista = new List<string>();
            while (reader.Read())
            {
                lista.Add(Convert.ToString(reader["tipoUsuario"]));
            }
            con.CerrarConexion();

            return lista;
        }

        #endregion



    }
}
