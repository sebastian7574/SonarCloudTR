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
    public class CD_Reservas
    {
        private readonly CD_Conexion con = new CD_Conexion();
        private CE_Reservas ce = new CE_Reservas();

        #region CARGAR RESERVAS A LA VISTA
        public DataTable CargarReservas()
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_R_CargarReservas", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            con.CerrarConexion();

            return dt;
        }

        #endregion 

        #region CARGAR ACOMPAÑANTES

        public DataTable CargarA(int idReserva)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_R_Acompañantes", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@idReserva", SqlDbType.Int).Value = idReserva;

            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            con.CerrarConexion();

            return dt;
        }
        #endregion


        #region Consultar

        public CE_Reservas CD_Consulta(int idReserva)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_R_Consultar", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@idReserva", SqlDbType.Int).Value = idReserva;


            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            ce.FechaDesde = Convert.ToDateTime(row[1]); 
            ce.FechaHasta = Convert.ToDateTime(row[2]);
            ce.EstadoRerserva = Convert.ToString(row[3]);
            ce.Abono = Convert.ToInt32(row[4]);
            //Fecha actual del campo check in
            ce.CheckIN = DateTime.Now;
            //En caso de que este este completo, mostrar la fecha
            if (!row.IsNull("checkIn")) 
            { 
                ce.CheckIN = Convert.ToDateTime(row[5]);
            }

            //Fecha actual para el campo checkout
            ce.CheckOUT = DateTime.Now;
            //Si esta guardado en la BD, se muestra el ingresado, no el actual
            if (!row.IsNull("chekOut"))
            {
                ce.CheckOUT = Convert.ToDateTime(row[6]);
            }

            ce.PrecioNocheReserva = Convert.ToInt32(row[8]);
            ce.Saldo = Convert.ToInt32(row[9]);
            ce.IdDepartamento = Convert.ToInt32(row[10]);
            ce.IdUsuario = Convert.ToInt32(row[11]);

            return ce;
        }

        #endregion

        #region Ingresar IN

        public void CD_ActualizarIN(CE_Reservas Reservas)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_R_IngresarIN",
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.AddWithValue("@idReserva", Reservas.IdReserva);
            com.Parameters.Add("@checkIn", SqlDbType.DateTime).Value = Reservas.CheckIN;

            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }

        #endregion

        #region Actualizar OUT

        public void CD_ActualizarOUT(CE_Reservas Reservas)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_R_IngresarOUT",
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.AddWithValue("@idReserva", Reservas.IdReserva);
            com.Parameters.Add("@checkOut", SqlDbType.DateTime).Value = Reservas.CheckOUT;

            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }

        #endregion

        #region BUSCAR 
        #region BUSCAR RESERVAS POR RUT

        public DataTable BuscarR(string buscarrut)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_RC_BuscarReserva", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@buscarrut", SqlDbType.VarChar).Value = buscarrut;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            con.CerrarConexion();

            return dt;
        }

        #endregion

        #region BUSCAR RESERVAS POR NOMBRE O APELLIDO

        public DataTable BuscarN(string buscar)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_RC_BuscarNC", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@buscar", SqlDbType.VarChar).Value = buscar;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            con.CerrarConexion();

            return dt;
        }

        #endregion
        #endregion
    }
}
