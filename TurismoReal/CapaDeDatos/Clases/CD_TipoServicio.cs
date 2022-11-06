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
    public class CD_TipoServicio
    {
        readonly CD_Conexion con = new CD_Conexion();
        readonly CE_TipoServicio ce = new CE_TipoServicio();

        //CRUD Tipo servicio
        #region Insertar
        public void CD_Insertar(CE_TipoServicio TipoServicios)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_TS_Insertar",
                CommandType = CommandType.StoredProcedure,
            };
            com.Parameters.AddWithValue("@tipoServicio", TipoServicios.TipoServicio);

            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }
        #endregion

        #region Consultar

        public CE_TipoServicio CD_Consulta(int idTipoServicio)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_TS_Consultar", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@idTipoServicio", SqlDbType.Int).Value = idTipoServicio;


            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            ce.TipoServicio = Convert.ToString(row[1]);

            return ce;
        }

        #endregion

        #region Actualizar Datos

        public void CD_ActualizarDatos(CE_TipoServicio TipoServicios)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_TS_Actualizar",
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.AddWithValue("@idTipoServicio", TipoServicios.IdTipoServicio);
            com.Parameters.AddWithValue("@tipoServicio", TipoServicios.TipoServicio);

            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }

        #endregion

        #region Eliminar
        public void CD_Eliminar(CE_TipoServicio TipoServicios)
        {
            SqlCommand com = new SqlCommand();
            com.Connection = con.AbrirConexion();
            com.CommandText = "dbo.SP_TS_Eliminar";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@idTipoServicio", TipoServicios.IdTipoServicio);
            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }

        #endregion

        #region CARGAR TIPO SERVICIO A LA VISTA

        public DataTable CargarTipoServicio()
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_P_CargarTipoServicio", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            con.CerrarConexion();

            return dt;
        }

        #endregion 

        #region IdTipoServicio
        public int IdTipoServicio(string TipoServicio)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_P_IdTipoServicio",
                CommandType = CommandType.StoredProcedure,
            };

            com.Parameters.AddWithValue("@tipoServicio", TipoServicio);
            object resultado = com.ExecuteScalar();
            int idtiposerv = (int)resultado;
            con.CerrarConexion();

            return idtiposerv;
        }

        #endregion
        #region nombre formato

        public CE_TipoServicio NombreTipoServicio(int IdTipoServicio)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_P_NombreTipoServicio", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@idTipoServicio", SqlDbType.Int).Value = IdTipoServicio;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            ce.TipoServicio = Convert.ToString(row[0]);

            return ce;

        }

        #endregion

        #region Listar

        public List<string> ObtenerTipoServicio()
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_P_CargarTipoServicio",
                CommandType = CommandType.StoredProcedure
            };
            SqlDataReader reader = com.ExecuteReader();
            List<string> lista = new List<string>();
            while (reader.Read())
            {
                lista.Add(Convert.ToString(reader["tipoServicio"]));
            }
            con.CerrarConexion();

            return lista;
        }

        #endregion

    }
}