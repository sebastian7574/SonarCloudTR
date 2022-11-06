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
    public class CD_TipoGasto
    {
        readonly CD_Conexion con = new CD_Conexion();
        readonly CE_TipoGasto ce = new CE_TipoGasto();

        //CRUD Artefactos
        #region Insertar
        public void CD_Insertar(CE_TipoGasto TipoGasto)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_TG_Insertar",
                CommandType = CommandType.StoredProcedure,
            };
            com.Parameters.AddWithValue("@tipoGasto", TipoGasto.TipoGasto);

            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }
        #endregion

        #region Consultar

        public CE_TipoGasto CD_Consulta(int idTipoGasto)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_TG_Consultar", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@idTipoGasto", SqlDbType.Int).Value = idTipoGasto;


            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            ce.TipoGasto = Convert.ToString(row[1]);

            return ce;
        }

        #endregion

        #region Actualizar Datos

        public void CD_ActualizarDatos(CE_TipoGasto TipoGasto)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_TG_Actualizar",
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.AddWithValue("@idTipoGasto", TipoGasto.IdTipoGasto);
            com.Parameters.AddWithValue("@tipoGasto", TipoGasto.TipoGasto);

            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }

        #endregion

        #region Eliminar
        public void CD_Eliminar(CE_TipoGasto TipoGasto)
        {
            SqlCommand com = new SqlCommand();
            com.Connection = con.AbrirConexion();
            com.CommandText = "dbo.SP_TG_Eliminar";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@idTipoGasto", TipoGasto.IdTipoGasto);
            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }

        #endregion


        #region IDTipoGasto
        public int IdTipoGasto(string TipoGasto)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_TG_IdGasto",
                CommandType = CommandType.StoredProcedure,
            };

            com.Parameters.AddWithValue("@tipoGasto", TipoGasto);
            object resultado = com.ExecuteScalar();
            int idtipogasto = (int)resultado;
            con.CerrarConexion();

            return idtipogasto;
        }

        #endregion
        #region nombre gasto

        public CE_TipoGasto NombreTipoGasto(int IdTipoGasto)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_TG_NombreGasto", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@idTipoGasto", SqlDbType.Int).Value = IdTipoGasto;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            ce.TipoGasto = Convert.ToString(row[0]);

            return ce;

        }

        #endregion
        #region Listar

        public List<string> ObtenerTipoGasto()
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_TG_CargarTipoGasto",
                CommandType = CommandType.StoredProcedure
            };
            SqlDataReader reader = com.ExecuteReader();
            List<string> lista = new List<string>();
            while (reader.Read())
            {
                lista.Add(Convert.ToString(reader["tipoGasto"]));
            }
            con.CerrarConexion();

            return lista;
        }

        #endregion

        #region CARGAR TipoGastos A LA VISTA

        public DataTable CargarTipoGastos()
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_TG_CargarTipoGasto", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            con.CerrarConexion();

            return dt;
        }

        #endregion 

    }
}