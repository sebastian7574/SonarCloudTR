using CapaDeDatos.Clases;
using CapaDeEntidad.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeNegocio.Clases
{
    public class CN_Boletas
    {
        CD_Boletas CD_Boletas = new CD_Boletas();
        private readonly CD_Boletas objDatos = new CD_Boletas();

        #region Insertar   

        public void Insertar(CE_Boletas Boletas)
        {
            objDatos.CD_Insertar(Boletas);
        }

        public void InsertarDS(CE_Boletas Boletas)
        {
            objDatos.CD_InsertarDS(Boletas);
        }

        #endregion

        #region Consultar
        public CE_Boletas Consulta(int idBoleta)
        {
            return objDatos.CD_Consulta(idBoleta);
        }
        #endregion

        #region Ver
        public CE_Boletas Ver(int idReserva)
        {
            return objDatos.CD_Ver(idReserva);
        }
        #endregion

        #region CARGAR BOLETAS POR RESERVA

        public DataTable CargarPorReserva(int idReserva)
        {
            return objDatos.CargarPorReserva(idReserva);
        }

        #endregion

        #region CARGAR BOLETAS A LA VISTA GENERAL

        public DataTable CargarBoletas()
        {
            return objDatos.CargarBoletas();
        }

        #endregion

        #region BUSCAR BOLETAS

        public DataTable Buscar(string buscar)
        {
            return objDatos.Buscar(buscar);
        }

        #endregion

        #region Consultar
        public CE_Boletas Detalle(int idBoleta)
        {
            return objDatos.CD_Detalle(idBoleta);
        }
        #endregion
    }
}
