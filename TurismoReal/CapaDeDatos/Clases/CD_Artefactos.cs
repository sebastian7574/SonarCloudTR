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
    public class CD_Artefactos
    {
        readonly CD_Conexion con = new CD_Conexion();
        readonly CE_Artefactos ce = new CE_Artefactos();

        //CRUD Artefactos
        #region Insertar
        public void CD_Insertar(CE_Artefactos Artefactos)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_A_Insertar",
                CommandType = CommandType.StoredProcedure,
            };
            com.Parameters.AddWithValue("@descripcion", Artefactos.Descripcion);
            com.Parameters.AddWithValue("@tamano", Artefactos.Tamano);
            com.Parameters.AddWithValue("@color", Artefactos.Color);
            com.Parameters.AddWithValue("@valor", Artefactos.Valor);
            com.Parameters.AddWithValue("@idUnidadMedida", Artefactos.IdUnidadMedida);

            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }
        #endregion

        #region Consultar

        public CE_Artefactos CD_Consulta(int idArtefactos)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_A_Consultar", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@idArtefactos", SqlDbType.Int).Value = idArtefactos;


            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            ce.Descripcion = Convert.ToString(row[1]);
            ce.Tamano = Convert.ToInt32(row[2]);
            ce.Color = Convert.ToString(row[3]);
            ce.Valor = Convert.ToInt32(row[4]);
            ce.IdUnidadMedida = Convert.ToInt32(row[5]);

            return ce;
        }

        #endregion

        #region Actualizar Datos

        public void CD_ActualizarDatos(CE_Artefactos Artefactos)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_A_Actualizar",
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.AddWithValue("@idArtefactos", Artefactos.IdArtefactos);
            com.Parameters.AddWithValue("@descripcion", Artefactos.Descripcion);
            com.Parameters.AddWithValue("@tamano", Artefactos.Tamano);
            com.Parameters.AddWithValue("@color", Artefactos.Color);
            com.Parameters.AddWithValue("@valor", Artefactos.Valor);
            com.Parameters.AddWithValue("@idUnidadMedida", Artefactos.IdUnidadMedida);

            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }

        #endregion

        #region Eliminar
        public void CD_Eliminar(CE_Artefactos Artefactos)
        {
            SqlCommand com = new SqlCommand();
            com.Connection = con.AbrirConexion();
            com.CommandText = "dbo.SP_A_Eliminar";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@idArtefactos", Artefactos.IdArtefactos);
            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }

        #endregion


        #region IdArtefacto
        public int IdArtefacto(string Descripcion)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_P_IdArtefacto",
                CommandType = CommandType.StoredProcedure,
            };

            com.Parameters.AddWithValue("@descripcion", Descripcion);
            object resultado = com.ExecuteScalar();
            int idartefacto = (int)resultado;
            con.CerrarConexion();

            return idartefacto;
        }

        #endregion
        #region nombre formato

        public CE_Artefactos NombreArtefacto(int IdArtefactos)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_P_NombreArtefacto", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@idArtefactos", SqlDbType.Int).Value = IdArtefactos;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            ce.Descripcion = Convert.ToString(row[0]);

            return ce;

        }

        #endregion
        #region Listar

        public List<string> ObtenerArtefacto()
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_P_CargarArtefacto",
                CommandType = CommandType.StoredProcedure
            };
            SqlDataReader reader = com.ExecuteReader();
            List<string> lista = new List<string>();
            while (reader.Read())
            {
                lista.Add(Convert.ToString(reader["descripcion"]));
            }
            con.CerrarConexion();

            return lista;
        }

        #endregion

        #region CARGAR ARTEFACTOS A LA VISTA

        public DataTable CargarArtefactos()
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_A_CargarArtefactos", con.AbrirConexion());
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