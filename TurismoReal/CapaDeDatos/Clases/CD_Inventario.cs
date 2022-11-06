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
    public class CD_Inventario
    {
        readonly CD_Conexion con = new CD_Conexion();
        readonly CE_Inventario ce = new CE_Inventario();

        //CRUD Artefactos
        #region Insertar
        public void CD_Insertar(CE_Inventario Inventario)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_IN_Insertar",
                CommandType = CommandType.StoredProcedure,
            };
            com.Parameters.AddWithValue("@cantidad", Inventario.Cantidad);
            com.Parameters.AddWithValue("@idDepartamento", Inventario.IdDepartamento);
            com.Parameters.AddWithValue("@idArtefactos", Inventario.IdArtefactos);

            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }
        #endregion

        #region Consultar

        public CE_Inventario CD_Consulta(int idInventario)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_IN_Consultar", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@idInventario", SqlDbType.Int).Value = idInventario;


            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            ce.Cantidad = Convert.ToInt32(row[1]);
            ce.IdArtefactos = Convert.ToInt32(row[3]);

            return ce;
        }
        #endregion

        #region CARGAR INVENTARIO A LA VISTA

        public DataTable CargarInventario(int idDepartamento)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_IN_CargarInventario", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@idDepartamento", SqlDbType.Int).Value = idDepartamento;

            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            con.CerrarConexion();

            return dt;
        }
        #endregion

        #region Consultar

        public CE_Inventario CargarInventarioIN(int idDepartamento)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_IN_CargarInventario", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@idDepartamento", SqlDbType.Int).Value = idDepartamento;


            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            ce.Cantidad = Convert.ToInt32(row[1]);
            ce.IdDepartamento = Convert.ToInt32(row[2]);
            ce.IdArtefactos = Convert.ToInt32(row[3]);

            return ce;
        }

        
        #endregion 

        #region Actualizar Datos

        public void CD_ActualizarDatos(CE_Inventario Inventario)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_IN_Actualizar",
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.AddWithValue("@idInventario", Inventario.IdInventario);
            com.Parameters.AddWithValue("@cantidad", Inventario.Cantidad);
            com.Parameters.AddWithValue("@idArtefactos", Inventario.IdArtefactos);

            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }

        #endregion

        #region Eliminar
        public void CD_Eliminar(CE_Inventario Inventario)
        {
            SqlCommand com = new SqlCommand();
            com.Connection = con.AbrirConexion();
            com.CommandText = "dbo.SP_IN_Eliminar";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@idInventario", Inventario.IdInventario);
            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }

        #endregion


        

    }
}