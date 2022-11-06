using CapaDeDatos.Clases;
using CapaDeEntidad.Clases;
using System.Data;

namespace CapaDeNegocio.Clases
{
    public class CN_Reservas
    {
        private readonly CD_Reservas objDatos = new CD_Reservas();

        #region CARGAR BOLETAS A LA VISTA

        public DataTable CargarReservas()
        {
            return objDatos.CargarReservas();
        }

        #endregion

        #region CARGAR ACOMPAÑANTES

        public DataTable CargarA(int idReserva)
        {
            return objDatos.CargarA(idReserva);
        }

        #endregion

        #region Consultar
        public CE_Reservas Consulta(int idReserva)
        {
            return objDatos.CD_Consulta(idReserva);
        }
        #endregion

        #region Ingresar IN

        public void ActualizarIN(CE_Reservas Reservas)
        {
            objDatos.CD_ActualizarIN(Reservas);
        }

        #endregion

        #region Ingresar OUT

        public void ActualizarOUT(CE_Reservas Reservas)
        {
            objDatos.CD_ActualizarOUT(Reservas);
        }

        #endregion

        #region BUSCAR USUARIOS

        public DataTable BuscarR(string buscarrut)
        {
            return objDatos.BuscarR(buscarrut);
        }

        public DataTable BuscarN(string buscar)
        {
            return objDatos.BuscarN(buscar);
        }

        #endregion
    }
}