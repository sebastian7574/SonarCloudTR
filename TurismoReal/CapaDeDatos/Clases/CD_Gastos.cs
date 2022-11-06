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
    public class CD_Gastos
    {
        readonly CD_Conexion con = new CD_Conexion();
        readonly CE_Gastos ce = new CE_Gastos();

        //CRUD Artefactos
        #region Insertar
        public void CD_Insertar(CE_Gastos Gastos)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_GA_Insertar",
                CommandType = CommandType.StoredProcedure,
            };
            com.Parameters.AddWithValue("@descripcion", Gastos.Descripcion);
            com.Parameters.AddWithValue("@monto", Gastos.Monto); 
            com.Parameters.Add("@fechaGastos", SqlDbType.Date).Value = Gastos.FechaGastos;
            com.Parameters.AddWithValue("@idDepartamento", Gastos.IdDepartamento);
            com.Parameters.AddWithValue("@idTipoGastos", Gastos.IdTipoGastos);


            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }
        #endregion

        #region Consultar

        public CE_Gastos CD_Consulta(int idGastos)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_GA_Consultar", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@idGastos", SqlDbType.Int).Value = idGastos;


            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            ce.Descripcion = Convert.ToString(row[1]);
            ce.Monto = Convert.ToInt32(row[2]);
            ce.FechaGastos = Convert.ToDateTime(row[3]);
            ce.IdDepartamento = Convert.ToInt32(row[4]);
            ce.IdTipoGastos = Convert.ToInt32(row[5]);

            return ce;
        }

        #endregion
        #region CARGAR GASTOS A LA VISTA

        public DataTable CargarGastos(int idDepartamento)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_GA_CargarGasto", con.AbrirConexion());
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

        #region Actualizar Datos

        public void CD_ActualizarDatos(CE_Gastos Gastos)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_GA_Actualizar",
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.AddWithValue("@idGastos", Gastos.IdGastos);
            com.Parameters.AddWithValue("@descripcion", Gastos.Descripcion);
            com.Parameters.AddWithValue("@monto", Gastos.Monto);
            com.Parameters.Add("@fechaGastos", SqlDbType.Date).Value = Gastos.FechaGastos;
            com.Parameters.AddWithValue("@idDepartamento", Gastos.IdDepartamento);
            com.Parameters.AddWithValue("@idTipoGastos", Gastos.IdTipoGastos);

            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }

        #endregion

        #region Eliminar
        public void CD_Eliminar(CE_Gastos Gastos)
        {
            SqlCommand com = new SqlCommand();
            com.Connection = con.AbrirConexion();
            com.CommandText = "dbo.SP_GA_Eliminar";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@idGastos", Gastos.IdGastos);
            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }

        #endregion




    }
}
