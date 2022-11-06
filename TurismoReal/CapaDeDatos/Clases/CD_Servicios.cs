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
    public class CD_Servicios
    {
        private readonly CD_Conexion con = new CD_Conexion();
        private CE_Servicios ce = new CE_Servicios();

        //CRUD Departamentos
        #region Insertar
        public void CD_Insertar(CE_Servicios Servicios)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_S_Insertar",
                CommandType = CommandType.StoredProcedure,
            };
            com.Parameters.AddWithValue("@descripcion", Servicios.Descripcion);
            com.Parameters.AddWithValue("@disponibilidad", Servicios.Disponibilidad);
            com.Parameters.AddWithValue("@precio", Servicios.Precio);
            com.Parameters.AddWithValue("@idTipoServicio", Servicios.IdTipoServicio);

            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }
        #endregion

        #region Consultar

        public CE_Servicios CD_Consulta(int idServicio)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_S_Consultar", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@idServicio", SqlDbType.Int).Value = idServicio;


            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            ce.Descripcion = Convert.ToString(row[1]);
            ce.Disponibilidad = Convert.ToString(row[2]);
            ce.Precio = Convert.ToInt32(row[3]);
            ce.IdTipoServicio = Convert.ToInt32(row[4]);

            return ce;
        }

        #endregion

        #region Eliminar
        public void CD_Eliminar(CE_Servicios Servicios)
        {
            SqlCommand com = new SqlCommand();
            com.Connection = con.AbrirConexion();
            com.CommandText = "dbo.SP_S_Eliminar";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@idServicio", Servicios.IdServicio);
            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }

        #endregion

        #region Actualizar Datos

        public void CD_ActualizarDatos(CE_Servicios Servicios)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_S_Actualizar",
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.AddWithValue("@idServicio", Servicios.IdServicio);
            com.Parameters.AddWithValue("@descripcion", Servicios.Descripcion);
            com.Parameters.AddWithValue("@disponibilidad", Servicios.Disponibilidad);
            com.Parameters.AddWithValue("@precio", Servicios.Precio);
            com.Parameters.AddWithValue("@idTipoServicio", Servicios.IdTipoServicio);

            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }

        #endregion


        #region CARGAR SERVICIOS A LA VISTA

        public DataTable CargarServicio()
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_S_CargarServicio", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            con.CerrarConexion();

            return dt;
        }

        #endregion

        #region CARGAR LISTADO SERVICIOS DISPONIBLES

        public DataTable CargarListado()
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_LS_ListarServ", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            con.CerrarConexion();

            return dt;
        }

        #endregion

        #region BUSCAR SERVICIOS

        public DataTable BuscarServ(string buscarServ)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_S_BuscarServicio", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@buscarServ", SqlDbType.VarChar).Value = buscarServ;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            con.CerrarConexion();

            return dt;
        }

        public DataTable BuscarServDispo(string servDispo)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_LS_Buscar", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@buscarServ", SqlDbType.VarChar).Value = servDispo;
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
