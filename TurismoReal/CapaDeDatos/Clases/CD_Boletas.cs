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
    public class CD_Boletas
    {
        private readonly CD_Conexion con = new CD_Conexion();
        private readonly CE_Boletas ce = new CE_Boletas();

        //CRUD Boletas
        #region Insertar
        public void CD_Insertar(CE_Boletas Boletas)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_B_IngresarCobro",
                CommandType = CommandType.StoredProcedure,
            };
            com.Parameters.AddWithValue("@medioDePago", Boletas.MedioDePago);
            com.Parameters.Add("@fecha", SqlDbType.Date).Value = Boletas.Fecha;
            com.Parameters.AddWithValue("@banco", Boletas.Banco);
            com.Parameters.AddWithValue("@comprobante", Boletas.Comprobante);
            com.Parameters.AddWithValue("@monto", Boletas.Monto);
            com.Parameters.AddWithValue("@efectivo", Boletas.Efectivo);
            com.Parameters.AddWithValue("@vuelto", Boletas.Vuelto);
            com.Parameters.AddWithValue("@descripcion", Boletas.Descripcion);
            com.Parameters.AddWithValue("@idReserva", Boletas.IdReserva);
            if (Boletas.IdDetalleServicio == 0)
            {
                com.Parameters.Add("@idDetalleServicio", SqlDbType.Int).Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                com.Parameters.AddWithValue("@idDetalleServicio", Boletas.IdDetalleServicio);
            }
            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }
        #endregion

        #region DETALLE SERVICIO
        public void CD_InsertarDS(CE_Boletas Boletas)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_B_IngresarDS",
                CommandType = CommandType.StoredProcedure,
            };
            com.Parameters.AddWithValue("@medioDePago", Boletas.MedioDePago);
            com.Parameters.Add("@fecha", SqlDbType.Date).Value = Boletas.Fecha;
            com.Parameters.AddWithValue("@banco", Boletas.Banco);
            com.Parameters.AddWithValue("@comprobante", Boletas.Comprobante);
            com.Parameters.AddWithValue("@monto", Boletas.Monto);
            com.Parameters.AddWithValue("@efectivo", Boletas.Efectivo);
            com.Parameters.AddWithValue("@vuelto", Boletas.Vuelto);
            com.Parameters.AddWithValue("@descripcion", Boletas.Descripcion);
            com.Parameters.AddWithValue("@idReserva", Boletas.IdReserva);
            com.Parameters.AddWithValue("@idDetalleServicio", Boletas.IdDetalleServicio);

            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }
        #endregion

        #region Consultar dentro de la interfaz

        public CE_Boletas CD_Consulta(int idBoleta)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_B_Consultar", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@idBoleta", SqlDbType.Int).Value = idBoleta;


            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            ce.MedioDePago = Convert.ToString(row[1]);
            ce.Fecha = Convert.ToDateTime(row[2]);
            ce.Banco = Convert.ToString(row[3]);
            ce.Comprobante = Convert.ToString(row[4]);
            ce.Monto = Convert.ToInt32(row[5]);
            ce.Efectivo = Convert.ToInt32(row[6]);
            ce.Vuelto = Convert.ToInt32(row[7]);
            ce.Descripcion = Convert.ToString(row[8]);
            ce.IdReserva = Convert.ToInt32(row[9]);

            //Fecha actual del campo check in
            ce.IdDetalleServicio = 0;
            //En caso de que este este completo, mostrar la fecha
            if (!row.IsNull("idDetalleServicio"))
            {
                ce.IdDetalleServicio = Convert.ToInt32(row[10]);
            }
            return ce;
        }

        #endregion 

        #region VER

        public CE_Boletas CD_Ver(int idReserva)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_B_Datos", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@idReserva", SqlDbType.Int).Value = idReserva;


            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            ce.MedioDePago = Convert.ToString(row[1]);
            ce.Fecha = DateTime.Now;
            ce.Banco = Convert.ToString(row[3]);
            ce.Monto = Convert.ToInt32(row[5]);
            ce.Efectivo = Convert.ToInt32(row[6]);
            ce.Vuelto = Convert.ToInt32(row[7]);
            ce.Descripcion = Convert.ToString(row[8]);
            ce.IdReserva = Convert.ToInt32(row[9]);

            return ce;
        }

        #endregion 


        #region CARGAR BOLETAS POR RESERVA

        public DataTable CargarPorReserva(int idReserva)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_B_PorReserva", con.AbrirConexion());
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

        #region CARGAR BOLETAS A LA VISTA GENERAL ADMIN
        public DataTable CargarBoletas()
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_B_CargarBoletas", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            con.CerrarConexion();

            return dt;
        }

        #endregion 

        #region BUSCAR BOLETAS POR COMPROBANTE

        public DataTable Buscar(string buscar)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_B_Buscar", con.AbrirConexion());
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

        #region Consultar dentro de la interfaz

        public CE_Boletas CD_Detalle(int idReserva)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_B_DetalleCheck", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@idReserva", SqlDbType.Int).Value = idReserva;


            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                ce.MedioDePago = Convert.ToString(row[1]);
                ce.Fecha = Convert.ToDateTime(row[2]);
                ce.Banco = Convert.ToString(row[3]);
                ce.Monto = Convert.ToInt32(row[5]);
                ce.Efectivo = Convert.ToInt32(row[6]);
                ce.Vuelto = Convert.ToInt32(row[7]);
                ce.Descripcion = Convert.ToString(row[8]);
                ce.IdReserva = Convert.ToInt32(row[9]);
            }
            return ce;

        }

        #endregion 
    }
}
