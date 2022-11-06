using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDeEntidad.Clases;

namespace CapaDeDatos.Clases
{
    public class CD_Acompanantes
    {
        private readonly CD_Conexion con = new CD_Conexion();

        #region BUSCAR RUT

        public DataTable BuscarRut(string buscarRut)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_A_BuscarRut", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@buscarrut", SqlDbType.VarChar).Value = buscarRut;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            con.CerrarConexion();

            return dt;
        }

        #endregion

        #region BUSCAR NOMBRE APELLIDO

        public DataTable Buscar(string buscar)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_A_Buscar", con.AbrirConexion());
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
    }
}
