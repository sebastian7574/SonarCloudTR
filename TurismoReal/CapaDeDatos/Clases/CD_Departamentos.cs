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
    public class CD_Departamentos
    {
        private readonly CD_Conexion con = new CD_Conexion();
        private CE_Departamentos ce = new CE_Departamentos();

        //CRUD Departamentos
        #region Insertar
        public void CD_Insertar(CE_Departamentos Departamentos)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_D_Insertar",
                CommandType = CommandType.StoredProcedure,
            };
            com.Parameters.AddWithValue("@descripcion", Departamentos.Descripcion);
            com.Parameters.AddWithValue("@direccion", Departamentos.Direccion);
            com.Parameters.AddWithValue("@cantHabitaciones", Departamentos.CantHabitaciones);
            com.Parameters.AddWithValue("@cantBanos", Departamentos.CantBanos);
            com.Parameters.AddWithValue("@precioNoche", Departamentos.PrecioNoche);
            com.Parameters.Add("@mantInicio", SqlDbType.Date).Value = Departamentos.MantInicio;
            com.Parameters.Add("@mantTermino", SqlDbType.Date).Value = Departamentos.MantTermino;
            com.Parameters.AddWithValue("@idComuna", Departamentos.IdComuna);
            com.Parameters.AddWithValue("@idEstadoDepto", Departamentos.IdEstadoDepto);

            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }
        #endregion

        #region Consultar

        public CE_Departamentos CD_Consulta(int idDepartamento)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_D_Consultar", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@idDepartamento", SqlDbType.Int).Value = idDepartamento;

            
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            ce.Descripcion = Convert.ToString(row[1]);
            ce.Direccion = Convert.ToString(row[2]);
            ce.CantHabitaciones = Convert.ToInt32(row[3]);
            ce.CantBanos = Convert.ToInt32(row[4]);
            ce.PrecioNoche = Convert.ToInt32(row[5]);
            ce.IdComuna = Convert.ToInt32(row[8]);
            ce.IdEstadoDepto = Convert.ToInt32(row[9]);

            return ce;
        }

        #endregion

        #region Actualizar Datos

        public void CD_ActualizarDatos(CE_Departamentos Departamentos)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_D_Actualizar",
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.AddWithValue("@idDepartamento", Departamentos.IdDepartamento);
            com.Parameters.AddWithValue("@descripcion", Departamentos.Descripcion);
            com.Parameters.AddWithValue("@direccion", Departamentos.Direccion);
            com.Parameters.AddWithValue("@cantHabitaciones", Departamentos.CantHabitaciones);
            com.Parameters.AddWithValue("@cantBanos", Departamentos.CantBanos);
            com.Parameters.AddWithValue("@precioNoche", Departamentos.PrecioNoche); 
            com.Parameters.AddWithValue("@idComuna", Departamentos.IdComuna);
            com.Parameters.AddWithValue("@idEstadoDepto", Departamentos.IdEstadoDepto);

            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }

        #endregion

        #region PROGRAMAR MANTENCIÓN

        public void CD_Mantencion(CE_Departamentos Departamentos)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_D_Mantencion",
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.AddWithValue("@idDepartamento", Departamentos.IdDepartamento);
            com.Parameters.Add("@mantInicio", SqlDbType.Date).Value = Departamentos.MantInicio;
            com.Parameters.Add("@mantTermino", SqlDbType.Date).Value = Departamentos.MantTermino;

            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }
        #region CARGAR MANTENCION

        public DataTable CargarMantencion(int idDepartamento)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_D_CargarMantencion", con.AbrirConexion());
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
        #endregion


        #region CARGAR DEPARTAMENTOS A LA VISTA

        public DataTable CargarDepartamentos()
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_D_CargarDeptos", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            con.CerrarConexion();

            return dt;
        }

        #endregion

        #region BUSCAR DEPTO.

        public DataTable BuscarDepto(string buscarDepto)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_D_BuscarDepto", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@buscarDepto", SqlDbType.VarChar).Value = buscarDepto;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            con.CerrarConexion();

            return dt;
        }

        #endregion

        #region FILTRO REGION
        public DataTable Filtro(string filtro)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_R_FiltroRegion", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@filtro", SqlDbType.VarChar).Value = filtro;
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
