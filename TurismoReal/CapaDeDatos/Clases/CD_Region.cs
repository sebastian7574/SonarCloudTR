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
    public class CD_Region
    {
        readonly CD_Conexion con = new CD_Conexion();
        private CE_Region ce = new CE_Region();
        
        #region Consultar
        public CE_Region CD_Consulta(int idRegion)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_P_Region", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@idRegion", SqlDbType.Int).Value = idRegion;


            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            ce.Region = Convert.ToString(row[0]);

            return ce;
        }
        #endregion

        #region SELECT ANIDADO
        public DataTable dt;
        public DataTable listaRegion()
        {
            dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("dbo.SP_R_Regiones", con.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.CerrarConexion();
            }
        }

        #endregion  


    }
}