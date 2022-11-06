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
    public class CD_Reportes
    {
        readonly CD_Conexion con = new CD_Conexion();
        readonly CE_Reportes ce = new CE_Reportes();

        //CRUD Artefactos
        #region Insertar
        public void CD_Insertar(CE_Reportes Reportes)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_RE_Insertar",
                CommandType = CommandType.StoredProcedure,
            };
            com.Parameters.AddWithValue("@total", Reportes.Total);
            com.Parameters.AddWithValue("@cantReservas", Reportes.CantReservas);
            com.Parameters.AddWithValue("@gastos", Reportes.Gastos);
            com.Parameters.AddWithValue("@fechadesde", Reportes.FechaDesde);
            com.Parameters.AddWithValue("@fechahasta", Reportes.FechaHasta);
            com.Parameters.AddWithValue("@idDepartamento", Reportes.IdDepartamento);
            com.Parameters.AddWithValue("@idRegion", Reportes.IdRegion);

            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }

        public void CD_InsertarR(CE_Reportes Reportes)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_RE_InsertarRegion",
                CommandType = CommandType.StoredProcedure,
            };
            com.Parameters.AddWithValue("@total", Reportes.Total);
            com.Parameters.AddWithValue("@cantReservas", Reportes.CantReservas);
            com.Parameters.AddWithValue("@gastos", Reportes.Gastos);
            com.Parameters.AddWithValue("@fechadesde", Reportes.FechaDesde);
            com.Parameters.AddWithValue("@fechahasta", Reportes.FechaHasta);
            com.Parameters.AddWithValue("@idDepartamento", Reportes.IdDepartamento);
            com.Parameters.AddWithValue("@idRegion", Reportes.IdRegion);

            com.ExecuteNonQuery();
            com.Parameters.Clear();
            con.CerrarConexion();
        }
        #endregion

        #region Consultar

        public CE_Reportes CD_Consulta()
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_RE_ReporteDepto", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            ce.Total = 0;
            if (!row.IsNull("total"))
            {
                ce.Total = Convert.ToInt32(row[0]);
            }
            ce.CantReservas = 0;
            if (!row.IsNull("cantReservas"))
            {
                ce.CantReservas = Convert.ToInt32(row[1]);
            }
            ce.Gastos = 0;
            if (!row.IsNull("gastos"))
            {
                ce.Gastos = Convert.ToInt32(row[2]);
            }
            return ce;
        }
        #endregion
    }
}
