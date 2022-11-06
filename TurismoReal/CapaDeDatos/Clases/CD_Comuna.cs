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
    public class CD_Comuna
    {
        readonly CD_Conexion con = new CD_Conexion();
        readonly CE_Comuna ce = new CE_Comuna();


        #region nombre formato

        public CE_Comuna NombreComuna(int IdComuna)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.SP_P_Comuna", con.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@idComuna", SqlDbType.Int).Value = IdComuna;
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt = ds.Tables[0];
            DataRow row = dt.Rows[0];
            ce.IdComuna = Convert.ToInt32(row[0]);
            ce.Comuna = Convert.ToString(row[1]);
            ce.IdRegion = Convert.ToInt32(row[2]);

            return ce;

        }

        #endregion

        #region IdTipoUsuario
        public int IdComuna(string comuna)
        {
            SqlCommand com = new SqlCommand()
            {
                Connection = con.AbrirConexion(),
                CommandText = "dbo.SP_C_IdComuna",
                CommandType = CommandType.StoredProcedure,
            };

            com.Parameters.AddWithValue("@comuna", comuna);
            object valor = com.ExecuteScalar();
            int idcomuna = (int)valor;
            con.CerrarConexion();

            return idcomuna;
        }

        #endregion


        #region SELECT ANIDADO
        public DataTable dt;
        public DataTable listarComunas(int idRegion)
        {
            dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("dbo.SP_C_Comuna", con.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@idRegion", SqlDbType.Int).Value = idRegion;
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
        public DataTable MostrarComuna(int idComuna)
        {
            dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("dbo.SP_P_Comuna", con.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@idComuna", SqlDbType.Int).Value = idComuna;
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

        public DataTable MostrarRegion(int idComuna)
        {
            dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("dbo.SP_P_CargarRegion", con.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@idComuna", SqlDbType.Int).Value = idComuna;
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